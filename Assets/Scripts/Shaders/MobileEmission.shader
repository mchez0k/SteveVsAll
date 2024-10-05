// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/Emission" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    /*[Toggle]*/ _EmissionForce ("Emission Force", Range(0,1)) = 0
    [ColorPicker] [HDR] _EmissionColor("Color", Color) = (1,1,1,1)
    _EmissionMap("Emission", 2D) = "white" {}
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex;
sampler2D _EmissionMap;
fixed4 _EmissionColor;
fixed _EmissionForce;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    o.Albedo = c.rgb;
    o.Alpha = c.a;
    
    o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor*_EmissionForce;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}