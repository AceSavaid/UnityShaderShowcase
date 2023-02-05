Shader "Alannis/ABumpMapShader"
{
    Properties {
        _myDiffuse ("Diffuse Texture", 2D) = "white" {}
        _BumpMap ("Bump Texture", 2D) = "bump" {}
        _BumpAmount ("Bump Amount", Range(0,10)) = 1
    }
    SubShader {
        CGPROGRAM
        #pragma surface surf Lambert
        sampler2D _myDiffuse;
        sampler2D _BumpMap;
        half _BumpAmount;
        struct Input {
            float2 uv_myDiffuse;
            float2 uv_BumpMap;
        };
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D(_myDiffuse, IN.uv_myDiffuse).rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Normal *= float3(_BumpAmount,_BumpAmount,1);
        }
    ENDCG 
    } 
    FallBack "Diffuse"
}
