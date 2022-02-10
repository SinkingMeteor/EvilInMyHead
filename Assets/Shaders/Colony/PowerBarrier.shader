Shader "Sheldier/PowerBarrier"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FieldOpacity("Field Opacity", Range(0, 1)) = 0.5
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
                float3 objectPos : TEXCOORD1;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _FieldOpacity;

			float triWave(float t, float offset, float yOffset)
			{
				return saturate(abs(frac(offset + t) * 2 - 1) + yOffset);
			}

			fixed4 texColor(v2f i)
			{
				fixed4 mainTex = tex2D(_MainTex, i.uv);
				mainTex.r *= triWave(_Time.x * 5, abs(i.objectPos.y) * 2, -0.7) * 6;
                mainTex.g *= (sin(_Time.z + mainTex.b * 5) + 1);
                mainTex.b = max(mainTex.r,mainTex.g);
                return mainTex.r * i.color + mainTex.g * i.color;
			}
            
            v2f vert (appdata v)
            {
                v2f o;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float4 clipPos = UnityObjectToClipPos(v.vertex);
                o.vertex = clipPos;
			    o.objectPos = v.vertex;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 restoredUv = i.uv / _MainTex_ST.xy;
                float2 centeredUv = (restoredUv * 2) - 1;
                float roundOpacity = pow(length(centeredUv), 4);
                float multiplier = frac((_Time.y + 1) * 0.5);
                float lineAlpha = smoothstep(multiplier, multiplier + 0.05, restoredUv.y) - smoothstep(multiplier + 0.1, multiplier + 0.15, restoredUv.y);
                fixed4 col = texColor(i);
                roundOpacity += lineAlpha;
                col.a = col.a * roundOpacity * _FieldOpacity;
                return col;
            }
            ENDCG
        }
    }
}
