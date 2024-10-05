Shader "Mobile/Metallic"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _AlbedoColor("Albedo Color", Color) = (1,1,1,1)
        _Metallic("Metallic", Range(0,1)) = 0.5
        _Glossiness("Smoothness", Range(0,1)) = 0.5
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 150

            CGPROGRAM
            #pragma surface surf Standard noforwardadd

            sampler2D _MainTex;
            fixed4 _AlbedoColor;
            fixed _Metallic;
            fixed _Glossiness;

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o) {
   
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb * _AlbedoColor.rgb;
                o.Alpha = c.a;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
            }
            ENDCG
    }
        Fallback "Mobile/Diffuse"
}
