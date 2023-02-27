Shader "Alannis/Void"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimColor("Rim Color", Color) = (0.0,0.4,0.7,0.0) //colour of rim lighting
        _RimPower("Rim Power", Range (0.5,8.0)) = 3.0 // level of rim power
    }
    SubShader
    {
        Tags{"Queue" = "Transparent"} //sets base of material to be transparent so that you can see through it and have levels of transparency applied;

        Pass{
           ZWrite On
           ColorMask 0 
        }

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade //sets the shader to have an alpha variable for transparencey 
        struct Input{
            float2 uv_MainTex;
            float3 viewDir; 
        };

        
        sampler2D _MainTex;
        float4 _RimColor;
        float _RimPower;

        //surface shader
        void surf (Input IN, inout SurfaceOutput o)
        {

            half rim = saturate (dot(normalize(IN.viewDir),o.Normal));
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _RimColor;
            o.Albedo = c.rgb;
            
            o.Emission = _RimColor.rgb *pow(rim, _RimPower) * 10; 
            o.Alpha = pow(rim, _RimPower); 
        }
        ENDCG
    }
    FallBack "Diffuse"
}