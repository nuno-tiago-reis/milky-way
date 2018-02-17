Shader "Custom/Alpha Mask" 
{
	Properties 
	{
		_MainTexture ("Main Texture", 2D) = "white" {}
		_AlphaTexture ("Alpha Texture", 2D) = "white" {}
	}

	SubShader 
	{
		Tags 
		{
			"RenderType" = "Transparent"
			"Queue"="Transparent" 
			
			"IgnoreProjector"="True" 
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest Off
		Fog { Mode Off }

		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		CGPROGRAM
		#include "UnityCG.cginc"

		#pragma surface surfaceFunction NoLighting alpha
		
		struct Input
		{
			float2 uv_MainTexture;
			float2 uv_AlphaTexture;
		};

		sampler2D _MainTexture;
		sampler2D _AlphaTexture;

		// Custom lighting model [No Lighting]
		fixed4 LightingNoLighting(SurfaceOutput surface, fixed3 lightDirection, fixed attenuation)
		{
			fixed4 color;
			color.rgb = surface.Albedo;
			color.a = surface.Alpha;

			return color;
		}

		// Custom surface function
		void surfaceFunction(Input input, inout SurfaceOutput output) 
		{
			float4 mainTexture = tex2D (_MainTexture, input.uv_MainTexture);
			float4 alphaTexture = tex2D (_AlphaTexture, input.uv_AlphaTexture);

			output.Albedo = mainTexture.rgb;
			output.Alpha = alphaTexture.a;
		}

		ENDCG
	}
}