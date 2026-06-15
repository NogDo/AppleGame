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
            #pragma multi_compile_instancing
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            // _MainTex_ST, _MainTex_TexelSize는 인스턴싱 대상이 아님
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;

            UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
                UNITY_DEFINE_INSTANCED_PROP(float4, _OutlineColor)
                UNITY_DEFINE_INSTANCED_PROP(float,  _OutlineWidth)
                UNITY_DEFINE_INSTANCED_PROP(float,  _OutlineEnabled)
            UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);

                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * IN.color;

                float outlineEnabled = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _OutlineEnabled);
                if (outlineEnabled < 0.5)
                    return texColor;

                float w  = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _OutlineWidth);
                float4 outlineColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _OutlineColor);
                float2 tx = _MainTex_TexelSize.xy;

                float a = texColor.a;

                // 원형 16방향 샘플링 (22.5° 간격)
                float outline = 0;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 1.000,  0.000) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.924,  0.383) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.707,  0.707) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.383,  0.924) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.000,  1.000) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-0.383,  0.924) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-0.707,  0.707) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-0.924,  0.383) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-1.000,  0.000) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-0.924, -0.383) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-0.707, -0.707) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2(-0.383, -0.924) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.000, -1.000) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.383, -0.924) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.707, -0.707) * w * tx).a;
                outline += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + float2( 0.924, -0.383) * w * tx).a;
                outline = saturate(outline) * (1 - a);

                return lerp(texColor, outlineColor, outline);
            }
            ENDHLSL
        }
    }
}
