Shader "Custom/Ocean" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Amplitude ("Wave Amplitude", Range(0, 1)) = 0.1
		_Period ("Wave Period", Range(0, 10)) = 1
		_Speed ("Wind Speed", Range(0, 10)) = 1
		_Direction ("Wind Direction", Vector) = (1, 0, 0, 0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		float _Amplitude;
		float _Period;
		float _Speed;
		float3 _Direction;

		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		float t(float4 pos)
		{
			float3 dir = normalize(_Direction);
			return dir.x * pos.x + dir.y * pos.z;
		}
		void vert(inout appdata_full v)
		{
			v.vertex.y += _Amplitude * sin(_Period * t(mul(v.vertex, unity_ObjectToWorld)) + _Speed * _Time.y);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
