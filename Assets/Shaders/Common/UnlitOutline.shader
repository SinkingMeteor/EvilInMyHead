Shader "Sheldier/UnlitOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline color", Color) = (1.0, 1.0,1.0,1.0)
        _OutlineWidth("Outline width", Range(0.01, 3.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Zwrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineWidth;

            const static float D = 0.7;
            const static float2 _directions[8] = {
                float2(1.0,0.0), float2(-1.0, 0.0), float2(0.0, 1.0),float2(0.0, -1.0),
                float2(D,D), float2(D,-D), float2(-D,-D), float2(-D, D)
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }
            float GetMaxAlpha(float2 uv)
            {
                float result = 0.0;

                float2 offsetUV = uv + _directions[0] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[1] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[2] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[3] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[4] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[5] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[6] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                 offsetUV = uv + _directions[7] * float2(_MainTex_TexelSize.xy) * _OutlineWidth;
                result = max(result, tex2D(_MainTex, offsetUV).a);
                
                return result;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                col.rgb = lerp(_OutlineColor, col.rgb, col.a);
                col.a = GetMaxAlpha(i.uv);
                return col;
            }
            ENDCG
        }
    }
}
