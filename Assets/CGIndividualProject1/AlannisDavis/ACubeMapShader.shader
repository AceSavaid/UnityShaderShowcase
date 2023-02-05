Shader "Custom/CubeMapShader"
{
    Properties
    {
        _myColor("Albedo Color", Color) = (1,1,1,1)
        _myRange("Range", Range(0,5)) = 1
        _myTex("Texture", 2D) = "white" {}
        _myCube("Cube", CUBE) = ""  {}
        _myFloat("Float", Float) = 0.5
        _myVector("Vector", Vector) = (0.5, 1, 1, 1)

    }
    SubShader{
        CGPROGRAM // START
        #pragma surface surf Lambert // Declare functions?

        fixed4 _myColor; //Direct reference to properties
        half _myRange;
        sampler2D _myTex;
        samplerCUBE _myCube;
        float _myFloat;
        float4 _myVector;

        struct Input
        {
            float2 uv_myTex;
            float3 worldRefl;
        };


        //Surface
        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = (tex2D(_myTex, IN.uv_myTex) * _myRange).rgb;

            o.Emission = texCUBE (_myCube, IN.worldRefl).rgb;
            
        }

        ENDCG // END
    }
    FallBack "Diffuse"
}
