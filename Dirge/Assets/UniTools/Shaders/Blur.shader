// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UIEffects/Blur"
{
	Properties
	{
		_BlurAmount("Blur Amount", Range(0,02)) = 0.0005
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		GrabPass{
			"_behind"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 scrPos : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			
			sampler2D _behind;
			float4 _behind_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _behind);

				float4 tmp = UnityObjectToClipPos(v.vertex);

				o.uv = ComputeGrabScreenPos(o.vertex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			float _BlurAmount;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 texcol = fixed4(0,0,0,0);

				float remaining = 1.0f;

				float coef = 1.0;

				float fI = 0;
				
				for (int j = 0; j < 3; j++) {
					fI++;
					coef *= 0.32;
					texcol += tex2D(_behind, float2(i.uv.x, i.uv.y - fI * _BlurAmount)) * coef;
					texcol += tex2D(_behind, float2(i.uv.x - fI * _BlurAmount, i.uv.y)) * coef;
					texcol += tex2D(_behind, float2(i.uv.x + fI * _BlurAmount, i.uv.y)) * coef;
					texcol += tex2D(_behind, float2(i.uv.x, i.uv.y + fI * _BlurAmount)) * coef;

					remaining -= 4 * coef;
				}
				texcol += tex2D(_behind, float2(i.uv.x, i.uv.y)) * remaining;

				return texcol;
			}
			ENDCG
		}
	}
}
