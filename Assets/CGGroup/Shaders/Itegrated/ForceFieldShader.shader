Shader "Alannis/ForceField"
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
            float3 viewDir; //takes in view direction so that it will be centered on that
        };

        //takes in properties above as variables in the program
        sampler2D _MainTex;
        float4 _RimColor;
        float _RimPower;

        //surface shader
        void surf (Input IN, inout SurfaceOutput o)
        {

            half rim = 1.0 - saturate (dot(normalize(IN.viewDir),o.Normal)); //sets rim, calculated by normalizing the view direction and normal 
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _RimColor;
            o.Albedo = c.rgb;
            //pow function is to set how drastic the change is between the colours, this is then multiplied by 10 to increase the effect
            o.Emission = _RimColor.rgb *pow(rim, _RimPower) * 10; //gives a glow effect that scales on the level of rim lighting so the outer parts are the most emissive and the inner parts are the least emmissive
            o.Alpha = pow(rim, _RimPower); //transparency of material set to rim power as well (1 being opaque and 0 being completely transparent) so that it can be transparent the further in the shape it is instead of black
        }
        ENDCG
    }
    FallBack "Diffuse"
}