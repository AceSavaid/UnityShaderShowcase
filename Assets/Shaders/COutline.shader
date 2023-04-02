Shader "Carlos/COutline"
{
    Properties
    {
        _OutlineWidth ("Outline Width", Range(0.0, 10)) = 0.005
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        
    }
    SubShader
    {
        // No culling or depth
        //Cull Off ZWrite Off ZTest Always

        Pass
        {
            Cull front  

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                //float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                //float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float3 norm = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                float2 offset = TransformViewToProjection(norm.xy);
                
                o.pos.xy += offset * o.pos.z * _OutlineWidth;
                o.color = _OutlineColor;
                //o.uv = v.uv;
                return o;
            }

            //sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return i.color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
