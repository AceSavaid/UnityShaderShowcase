Shader "Alannis/AToonShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _RampTex ("Toon Ramp Texture", 2D) = "white" {}
        _Contribution("Contribution", Range(0, 1)) = 0.5
    }
    SubShader
    {
        CGPROGRAM
        
        #pragma surface surf ToonRamp

        float4 _Color;
        sampler2D _RampTex;
        sampler2D _MainTex;
        float _Contribution;

        float4 LightingToonRamp (SurfaceOutput s, fixed3 lightDir, fixed atten){

            float diff = dot (s.Normal, lightDir);
            float h = diff * 0.5 + 0.5;
            float2 rh = h;
            float3 ramp = tex2D(_RampTex, rh).rgb;

            float4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
            c.a = s.Alpha;
            return c;
        }


        struct Input
        {
            float2 uv_MainTex;
        };

        
        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
