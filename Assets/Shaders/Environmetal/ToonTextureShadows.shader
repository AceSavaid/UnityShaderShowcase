Shader "Custom/ToonTextureShadows"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {} 

        _ShadowColour("Shadow Colour", Color) = (0,0.5,1,1)
         
        _ShadowTexture("Shadow Texture", 2D) = "white" {}
        _STexZoom("Shadow Texture Zoom", Range(0, 20)) = 1
        _SecondShadow("Second Shadow Level", Range(0,1)) = 0.5
        
        _RampTex("Toon Ramp Texture", 2D) = "white" {}

    }
    SubShader{
        Tags { "Geometry" = "Transparent" }
        //LOD 200

        ZWrite off
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert
        

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _OutlineWidth;
        fixed4 _Color;
        fixed4 _OutlineColor;

        void vert(inout appdata_full v)
        {
            v.vertex.xyz += v.normal * _OutlineWidth;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            o.Emission = _OutlineColor.rgb;
        }
        ENDCG
        ZWrite on
        Pass
        {
            Tags {"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
        
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR0;
                float4 pos : SV_POSITION;
                SHADOW_COORDS(1)
            //SHADOW COORDS
            };

            v2f vert(appdata v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;

                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0;
                TRANSFER_SHADOW(o);

                return o;
            }

            sampler2D _MainTex;
            float4 _Color;
            float4 _ShadowColour;
            sampler2D _ShadowTexture;
            float  _STexZoom;
            float _SecondShadow;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                fixed shadow = SHADOW_ATTENUATION(i);
                
                col.rgb *= i.diff;
                
                fixed4 sCol =  (( tex2D(_ShadowTexture, i.uv * _STexZoom) *col * _SecondShadow * _ShadowColour ) * (1-shadow)) + (col * shadow);
                col = sCol;

                return col;
            }
        ENDCG
        }

        
        Pass
        {
            Tags {"LightMode" = "ShadowCaster"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f{
                V2F_SHADOW_CASTER;
                
            };

            v2f vert(appdata v){
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target{

                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
