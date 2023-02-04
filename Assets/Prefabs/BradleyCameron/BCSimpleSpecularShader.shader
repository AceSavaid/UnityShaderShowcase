Shader "Custom/BCSimpleSpecularShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MetallicTex("Metallic (R)", 2D) = "white" {}
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags {"Queue" = "Geometry"}
            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf StandardSpecular

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MetallicTex;
            half _Metallic;
            fixed4 _Color;

            struct Input
            {
                float2 uv_MetallicTex;
            };

            void surf(Input IN, inout SurfaceOutputStandardSpecular o)
            {
                o.Albedo = _Color.rgb;
                // Metallic and smoothness come from slider variables
                o.Specular = _Metallic;
                o.Smoothness = tex2D(_MetallicTex, IN.uv_MetallicTex).r;

            }
            ENDCG
        }
        FallBack "Diffuse"
}
