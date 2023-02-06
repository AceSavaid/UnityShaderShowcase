Shader "Custom/BCRimLightShader"
{
    Properties
    {
        _RimColor("Rim Color", Color) = (0,0.5,0.5,0)
        _RimPower("Rim Power", Range(0.5,8)) = 3
    }
        SubShader
    {
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float3 viewDir;
        };

    float4 _RimColor;
    float _RimPower;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow(rim,_RimPower);
        }
        ENDCG
    }
        FallBack "Diffuse"
}
