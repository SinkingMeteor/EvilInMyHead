Shader "Sheldier/PowerShield"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FieldOpacity("Field Opacity", Range(0, 1)) = 0.5
        _GlitchMap("Glitch Map", 2D) = "white" {}
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
                float4 color : COlOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            sampler2D _GlitchMap;
            float4 _MainTex_ST;
            float _FieldOpacity;

            float glitchWave(float x)
            {
                float y = 1.0 -(smoothstep(0.0,0.5, sin(_Time.y + x) ) * cos(_Time.y * 0.03));
                return 1.0 -(smoothstep(0.0, 0.9, sin(_Time.y * y * 0.1) )* (cos(_Time.y * 0.02)+0.1) * 0.2);
            }
            v2f vert (appdata v)
            {
                v2f o;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float4 clipPos = UnityObjectToClipPos(v.vertex);
                o.vertex = clipPos;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 restoredUv = i.uv / _MainTex_ST.xy;

                float glitchCol = sin(tex2D(_GlitchMap, restoredUv + _Time.y).r);
                
                float2 centeredUv = (restoredUv * 2) - 1;
                float roundOpacity = pow(length(centeredUv), 3);
                float multiplier = frac((_Time.y + 1) * 0.5);
                float lineAlpha = smoothstep(multiplier, multiplier + 0.2, restoredUv.y) - smoothstep(multiplier + 0.25, multiplier + 0.45, restoredUv.y);
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                float cellBorders = step(0.6, 1 - col.g);
                float isInLine = cellBorders * lineAlpha;
                roundOpacity += lineAlpha * glitchCol;
                col.b = max(col.b,  col.b* isInLine);
                col.a = col.a * roundOpacity * _FieldOpacity;
                float glitch = glitchWave(col.a);
                col.a *= glitch;
                col.b *= glitch;
                return col;
            }
            ENDCG
        }
    }
}
