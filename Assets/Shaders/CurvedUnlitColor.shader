Shader "Unlit/CurvedUnlitColor"
{ 
	Properties
	{
        _Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB

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
	float4 color : COLOR;
};

struct v2f
{
	float2 uv : TEXCOORD0;
	UNITY_FOG_COORDS(1)
	float4 color : TEXCOORD2;
	float4 vertex : SV_POSITION;
};

float4 _MainTex_ST;
fixed4 _Color;
float _CurveStrength;


v2f vert(appdata v)
{
	v2f o;

	float _Horizon = 100.0f;
	float _FadeDist = 50.0f;

	o.vertex = UnityObjectToClipPos(v.vertex);


	float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);

	o.vertex.y -= _CurveStrength * dist * dist * _ProjectionParams.x;

	o.uv = TRANSFORM_TEX(v.uv, _MainTex);

	o.color = v.color;

	UNITY_TRANSFER_FOG(o, o.vertex);
	return o;
}

fixed4 frag(v2f i) : SV_Target
{
return _Color;
}

			ENDCG
		}
	}
}
