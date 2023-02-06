Shader "Custom/BCSimpleDiffuseShader"
{
       Properties
        {
            _myColor("Albedo Color", Color) = (1,1,1,1)

        }
            SubShader
        {
          Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert

           fixed4 _myColor; //Direct reference to properties 

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                 o.Albedo = _myColor.rgb;
            }
            ENDCG
        }
            Fallback "Diffuse"
}
