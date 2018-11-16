Shader "Custom/Ocean" {
Properties {
	_Color ("Color", Color) = (1,1,1,1)
	_MainTex ("Albedo (RGB)", 2D) = "white" {}
	_Amplitude ("Wave Amplitude", Range(0, 1)) = 0.1
	_Period ("Wave Period", Range(0, 10)) = 1
	_Speed ("Wind Speed", Range(0, 10)) = 1
	_Direction ("Wind Direction", Vector) = (1, 0, 0, 0)
	_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0
}
Category {
	Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
	Blend SrcAlpha OneMinusSrcAlpha
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off

	SubShader {
		Pass {

			CGPROGRAM
			//#pragma surface surf BlinnPhong vertex:vert
			//#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_particles
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc" // for _LightColor0

			//struct Input {
			//	float2 uv_MainTex;
			//};

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
				fixed4 color : COLOR0;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
	#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD2;
	#endif
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float _Amplitude;
			float _Period;
			float _Speed;
			float3 _Direction;

			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			v2f vert(appdata_t v)
			{
				// fog and unity transforms
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
#endif
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);

				// wave calculations
				float4 worldPos = mul(v.vertex, unity_ObjectToWorld);
				float3 dir = normalize(_Direction);
				float t = _Period * (dir.x * worldPos.x + dir.y * worldPos.z) + _Speed * _Time.y;
				float sint = sin(t);
				float cost = cos(t);
				float3 tangent = normalize(float3(dir.x, cost * _Amplitude, dir.y));
				float3 wavecrest = normalize(float3(dir.y, 0, dir.x));

				// wave height
				o.vertex.y += _Amplitude * sint;
				// wave normal
				o.normal = normalize(cross(tangent, wavecrest));
				o.normal.y = abs(o.normal.y);

				// dot product between normal and light direction for
				// standard diffuse (Lambert) lighting
				half nl = max(0, dot(o.normal, _WorldSpaceLightPos0.xyz));
				// factor in the light color
				o.color = nl * _LightColor0 * v.color;// *_Color;

				return o;
			}

			UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
			float _InvFade;

			fixed4 frag(v2f i) : SV_Target
			{
				#ifdef SOFTPARTICLES_ON
				float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
				float partZ = i.projPos.z;
				float fade = saturate(_InvFade * (sceneZ - partZ));
				i.color.a *= fade;
				#endif

				fixed4 col = 2.0f * i.color * tex2D(_MainTex, i.texcoord);
				col.a = col.a;
				col.a = saturate(col.a); // alpha should not have double-brightness applied to it, but we can't fix that legacy behaior without breaking everyone's effects, so instead clamp the output to get sensible HDR behavior (case 967476)

				UNITY_APPLY_FOG(i.fogCoord, col);
				//return i.color;
				return col;
			}

			//void surf (Input IN, inout SurfaceOutput o) {
			//	fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//	o.Albedo = c.rgb;
			//	o.Alpha = c.a;
			//}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
}