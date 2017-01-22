// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Test"
{
	Properties{
		_ColorMap("Height Map", 2D) = "white" {}
		_Tex("Texture", 2D) = "white" {}
		_Thickness("Thickness", Float) = 0.2
		_Decay("Decay", Float) = 0.2

	}
	SubShader {
		Tags {"Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct vertInput {
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};
			struct vertOutput {
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed3 worldPos : TEXCOORD1;
				float4 color : COLOR;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, input.pos);
				o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
				o.texcoord = input.texcoord;
				o.color = input.color;
				return o;
			}


			uniform int _Points_Length;		

			uniform float3 _Points[100];		
			uniform float _Radius[100];

			uniform float _Thickness;
			uniform float _Decay;
			uniform float _NormalParam;
			uniform float _FadeTime;

			sampler2D _Tex;
			sampler2D _ColorMap;

			half4 frag(vertOutput output) : COLOR {
				half h = 0;			
				for (int i = 0; i < _Points_Length; i++) {
					half di = distance(output.worldPos, _Points[i].xyz);
					
					half ri = _Radius[i];
					//if (di < ri - _Thickness || di > ri + _Thickness) continue;
					half hi = saturate(1 - _Decay * abs(di - ri));
					
					h += hi;
				}

				h = saturate(h);
				half4 color = half4(0.0, 0.0, 0.0, 1.0);
				if (h > 0) {
					color += h * tex2D(_Tex, output.texcoord) * tex2D(_ColorMap, output.texcoord);
				}
				return color;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
