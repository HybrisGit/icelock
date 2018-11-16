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
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		ZWrite Off
		//Blend One DstColor
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert alpha

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

		void vert(inout appdata_full v)
		{
			float4 worldPos = mul(v.vertex, unity_ObjectToWorld);
			float3 dir = normalize(_Direction);
			float t = _Period * (dir.x * worldPos.x + dir.y * worldPos.z) + _Speed * _Time.y;
			float sint = sin(t);
			float cost = cos(t);
			float3 tangent = normalize(float3(dir.x, cost * _Amplitude, dir.y));
			float3 wavecrest = normalize(float3(dir.y, 0, dir.x));

			v.vertex.y += _Amplitude * sint;
			v.normal = normalize(cross(tangent, wavecrest));
			v.normal.y = abs(v.normal.y);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
