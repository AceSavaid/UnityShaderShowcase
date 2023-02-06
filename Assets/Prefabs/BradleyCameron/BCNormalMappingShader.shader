Shader "Custom/BCNormalMappingShader"
{
    Properties{
        _myDiffuse("Diffuse Texture", 2D) = "white" {}
        _NMap("NormalMap", 2D) = "normal" {}
    }
        SubShader{
            CGPROGRAM
            #pragma surface surf Lambert
            sampler2D _myDiffuse;
            sampler2D _NMap;
            struct Input {
                float2 uv_myDiffuse;
                float2 uv_NMap;
            };
            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_myDiffuse, IN.uv_myDiffuse).rgb;
                o.Normal = UnpackNormal(tex2D(_NMap, IN.uv_NMap));
            }
        ENDCG
    }
        FallBack "Diffuse"
}
