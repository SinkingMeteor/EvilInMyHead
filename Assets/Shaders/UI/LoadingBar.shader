Shader "Sheldier/LoadingBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1.0,1.0,1.0,1.0)
        _Fill ("Fill Amount", Range(0.0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" "PreviewType" = "Plane"}
        LOD 100
        
        Cull Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _MainTex_ST;
            float _Fill;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                clip(step(i.uv.x, _Fill) - 0.1f);
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                return col;
            }
            ENDCG
        }
    }
}
