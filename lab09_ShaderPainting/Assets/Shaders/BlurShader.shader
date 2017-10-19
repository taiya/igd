Shader "Hidden/BlurShader"
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

				float kernel[25] = {
					0.003765, 	0.015019, 	0.023792, 	0.015019, 	0.003765,
					0.015019, 	0.059912, 	0.094907, 	0.059912, 	0.015019,
					0.023792, 	0.094907, 	0.150342, 	0.094907, 	0.023792,
					0.015019, 	0.059912, 	0.094907, 	0.059912, 	0.015019,
					0.003765, 	0.015019, 	0.023792, 	0.015019, 	0.003765
				};

				// Dimensions of a pixel in screen space
				float2 pixelSize = 1 / screenSize;

				float blendFactor = brushSampleAA(input.uv);


				// TODO: -------------------------
				// Calculate the convolution of the kernel with the input image.

				float4 convolution = float4(0,0,0,1);

				for (int i = 0;i < 5;i++) {
					for (int j = 0;j < 5;j++) {

						float2 samplePos = input.uv; // <--- Change this
						int kernelIndex = i + 5 * j;

						convolution += 0; // <--- Change this

					}
				}

				// -------------------------------

				// Get the color of the old pixel at this position
				float4 oldPixelVal = tex2D(_MainTex, input.uv);

				return (1 - blendFactor) * oldPixelVal + blendFactor * convolution;
			}
			ENDCG
		}
	}
}
