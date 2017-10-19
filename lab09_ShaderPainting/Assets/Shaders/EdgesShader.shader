Shader "Hidden/EdgesShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "ShadersCommon.cginc"

			fixed4 frag (v2f input) : SV_Target {

				float kernel[9] = {
					1, 	0, 	-1,
					2,	0, 	-2,
					1, 	0, 	-1
				};

				// Dimensions of a pixel in screen space
				float2 pixelSize = 1 / screenSize;

				float blendFactor = brushSampleAA(input.uv) * 0.1;


				// TODO: -------------------------
				// Calculate the convolution of the kernel with the input image.

				float4 convolution = float4(0,0,0,1);


				// -------------------------------

				// Get the color of the old pixel at this position
				float4 oldPixelVal = tex2D(_MainTex, input.uv);

				return oldPixelVal + blendFactor * convolution;
			}
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "ShadersCommon.cginc"

			fixed4 frag (v2f input) : SV_Target {

				float kernel[9] = {
					1, 	2, 	1,
					0, 	0, 	0,
					-1,	-2,	-1
				};

				// Dimensions of a pixel in screen space
				float2 pixelSize = 1 / screenSize;

				float blendFactor = brushSampleAA(input.uv) * 0.01;


				// TODO: -------------------------
				// Calculate the convolution of the kernel with the input image.

				float4 convolution = float4(0, 0, 0, 1);

				// -------------------------------

				// Get the color of the old pixel at this position
				float4 oldPixelVal = tex2D(_MainTex, input.uv);

				return oldPixelVal + blendFactor * convolution;
			}
			ENDCG
		}
	}
}
