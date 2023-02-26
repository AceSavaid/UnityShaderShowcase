Shader "Alannis/UpgradedToonShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _RampTex ("Toon Ramp Texture", 2D) = "white" {}
        _Contribution("Contribution", Range(0, 1)) = 0.5
        _BumpMap ("Bump Texture", 2D) = "bump" {}
        _BumpAmount ("Bump Amount", Range(0,10)) = 1
    }
    SubShader
    {
        CGPROGRAM
        
        #pragma surface surf ToonRamp

        float4 _Color;
        sampler2D _MainTex;
        sampler2D _RampTex;
        float _Contribution;
        sampler2D _BumpMap;
        half _BumpAmount;

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
            float2 uv_BumpMap;
        };

        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Normal *= float3(_BumpAmount,_BumpAmount,1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
