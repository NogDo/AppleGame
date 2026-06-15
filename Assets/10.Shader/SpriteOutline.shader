Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineWidth ("Outline Width", Float) = 0.005
        _OutlineEnabled ("Outline Enabled", Float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _MainTex_TexelSize; // 텍스처 1픽셀 크기 자동 제공
                float4 _OutlineColor;
                float _OutlineWidth;
                float _OutlineEnabled;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * IN.color;

                if (_OutlineEnabled < 0.5)
                    return texColor;

                // 현재 픽셀이 투명하면 → 주변에 불투명 픽셀 있는지 검사
                float w = _OutlineWidth;
                float2 tx = _MainTex_TexelSize.xy; // (1/width, 1/height)

                float a = texColor.a;
                float aUp    = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(0,  w) * tx).a;
                float aDown  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(0, -w) * tx).a;
                float aRight = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( w, 0) * tx).a;
                float aLeft  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-w, 0) * tx).a;

                float outline = saturate(aUp + aDown + aRight + aLeft) * (1 - a);

                return lerp(texColor, _OutlineColor, outline);
            }
            ENDHLSL
        }
    }
}