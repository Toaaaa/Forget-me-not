Shader "Unlit/UIGradientAlphShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay-1" }  // "Queue"를 Overlay-1로 설정하여 뒤쪽에 그려지게 합니다.
        LOD 100

        Pass
        {
            Cull Off
            ZWrite Off  // UI 요소는 일반적으로 ZWrite Off로 설정합니다.
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Always  // 항상 그리도록 설정

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                // Apply gradient alpha
                texColor.a *= i.uv.x;
                return texColor;
            }
            ENDCG
        }
    }//
    FallBack "Transparent/VertexLit"
}
