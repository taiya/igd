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

			float umin;
			float umax;
			float vmin;
			float vmax;

			fixed4 frag (v2f input) : SV_Target {

				float2 uv = input.uv;

				float4 texVal = tex2D(_MainTex, (input.uv - float2(umin,vmin)) / float2(umax-umin,vmax-vmin));

				texVal *= step(umin,uv.x) * step(uv.x,umax) * step(vmin,uv.y) * step(uv.y,vmax);

				return texVal;
			}
			ENDCG
		}
	}
}
