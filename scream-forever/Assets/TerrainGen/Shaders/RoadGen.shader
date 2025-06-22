Shader "Scream2024/NoiseGen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("color", Color) = (.25, .5, .5, 1)
        [PerRendererData] _Amplitude("Amplitude", Float) = 8
        [PerRendererData] _Frequency("Frequency", Float) = .005
        [PerRendererData] _Octaves("Octaves", Integer) = 8
        [PerRendererData] _NoiseScale("NoiseScale", Float) = 1
        [PerRendererData] _BaseX("BaseX", Float) = 0
        [PerRendererData] _BaseY("BaseY", Float) = 0
        [PerRendererData] _BaseZ("BaseZ", Float) = 0

        [PerRendererData] _NoiseTypeIn("Noise Type In", Integer) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "FastNoiseLite.hlsl"

            float _Amplitude;
            float _Frequency;
            float _NoiseScale;
            int _Octaves;

            int _NoiseTypeIn;

            int _BaseX, _BaseY, _BaseZ;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float3 pos;
                pos.x = floor(_BaseX + IN.uv.x * 24.0) * _NoiseScale;
                pos.y = 0;
                pos.z = floor(_BaseZ + IN.uv.y * 24.0) * _NoiseScale;

                fnl_state noise = fnlCreateState();
                noise.noise_type = _NoiseTypeIn;
                noise.fractal_type = 2;
                noise.frequency = _Frequency;
                noise.octaves = _Octaves;

                float noiseVal = fnlGetNoise3D(noise, pos.x, 0, pos.z) * _Amplitude;
                return noiseVal;
            }
            ENDCG
        }
    }
}
