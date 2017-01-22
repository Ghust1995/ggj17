// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Sonar"
{
	Properties{
		
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Color("Tint", Color) = (1,1,1,1)
		_Decay("Decay", Float) = 0.2

	}
	SubShader {
		Tags {"Queue" = "Transparent"}
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct vertInput {
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
				float4 vertex   : POSITION;
			};
			struct vertOutput {
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed3 worldPos : TEXCOORD1;
				float4 color : COLOR;
			};

			fixed4 _Color;

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, input.pos);
				o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
				o.texcoord = input.texcoord;
				o.color = input.color * _Color;
				#ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(o.vertex);
				#endif
				return o;
			}			

			uniform int _Points_Length;		

			uniform float3 _Points[100];		
			uniform float _Radius[100];

			//uniform float _Thickness;
			uniform float _Decay;
			uniform float _NormalParam;
			uniform float _FadeTime;

			sampler2D _Tex;
			sampler2D _MainTex;

			half4 frag(vertOutput output) : COLOR {
				half h = 0;			
				for (int i = 0; i < _Points_Length; i++) {
					half di = distance(output.worldPos, _Points[i].xyz);
					
					half ri = _Radius[i];
					//if (di < ri - _Thickness || di > ri + _Thickness) continue;
					half hi = 0.8*saturate(1 - _Decay * abs(di - ri));
					
					h += hi;
				}

				h = saturate(h);
				half4 color = half4(0.0, 0.0, 0.0, 0.0);
				if (h > 0) {
					color += h * tex2D(_MainTex, output.texcoord);
				}
				return color;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
