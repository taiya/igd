Shader "Hidden/PaintShader"
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

				float blendFactor = brushSampleAA(input.uv);
				blendFactor *= brushColor.a;

				// Get the color of the old pixel at this position
				float4 oldPixelVal = tex2D(_MainTex, input.uv);

				return (1 - blendFactor) * oldPixelVal + blendFactor * float4(brushColor.rgb,1);
			}
			ENDCG
		}
	}
}
