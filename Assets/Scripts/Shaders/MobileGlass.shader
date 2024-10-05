Shader "Mobile/glass"
{
    Properties
    {
       _MainTex("Texture", 2D) = "white" {}
       _Color("Color", Color) = (1, 1, 1, 1)
       _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
       _EdgeThickness("Silouette Dropoff Rate", float) = 1.0
    }
    
    SubShader
    {
       Tags
       {
          "Queue" = "Transparent"
       }

       LOD 150
       Pass
       {
          Cull Back
          ZWrite Off
          Blend SrcAlpha OneMinusSrcAlpha

          CGPROGRAM

          #pragma vertex vert
          #pragma fragment frag
          #pragma multi_compile_instancing // Добавляем поддержку инстансинга

          #include "UnityCG.cginc" // Для UNITY_SETUP_INSTANCE_ID и UNITY_INITIALIZE_OUTPUT

          sampler2D     _MainTex;
          uniform float4 _Color;
          uniform float4 _EdgeColor;
          uniform float   _EdgeThickness;

          struct vertexInput
          {
             float4 vertex : POSITION;
             float3 normal : NORMAL;
             float3 texCoord : TEXCOORD0;
             UNITY_VERTEX_INPUT_INSTANCE_ID // Для поддержки инстансинга
          };

          struct vertexOutput
          {
             float4 pos : SV_POSITION;
             float3 normal : NORMAL;
             float3 texCoord : TEXCOORD0;
             float3 viewDir : TEXCOORD1;
             UNITY_VERTEX_OUTPUT_STEREO // Для поддержки инстансинга
          };

          vertexOutput vert(vertexInput input)
          {
             vertexOutput output;
             UNITY_SETUP_INSTANCE_ID(input); // Для поддержки инстансинга
             UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output); // Для поддержки инстансинга

             output.pos = UnityObjectToClipPos(input.vertex);
             float4 worldNormal = float4(input.normal, 0.0);
             output.normal = normalize(mul(worldNormal, unity_WorldToObject).xyz);
             output.viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, input.vertex).xyz);

             output.texCoord = input.texCoord;

             return output;
          }

          float4 frag(vertexOutput input) : COLOR 
          {
             float4 texColor = tex2D(_MainTex, input.texCoord.xy);
             float edgeFactor = abs(dot(input.viewDir, input.normal));

             float oneMinusEdge = 1.0 - edgeFactor;
             float3 rgb = (_Color.rgb * edgeFactor) + (_EdgeColor * oneMinusEdge);
             rgb = min(float3(1, 1, 1), rgb); 
             rgb = rgb * texColor.rgb;
             float opacity = min(1.0, _Color.a / edgeFactor);
             opacity = pow(opacity, _EdgeThickness);
             opacity = opacity * texColor.a;

             float4 output = float4(rgb, opacity);
             return output;
          }

          ENDCG
       }
    }
}