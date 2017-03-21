struct appdata
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
};

struct v2f
{
	float2 uv : TEXCOORD0;
	float4 vertex : SV_POSITION;
};

v2f vert (appdata v)
{
	v2f o;
	o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
	o.uv = v.uv;
	return o;
}

float2 brushStartPos;
float2 brushEndPos;
float2 screenSize;

float brushRadius;
float4 brushColor;

sampler2D _MainTex;

// Returns value of 1 or 0 where
// 1 is inside brush and 0 is outside
float brushSample(float2 samplePos) {

	// Calculate Distance from line 
	float lineLength = length(brushEndPos - brushStartPos);
	float2 lineDir = normalize(brushEndPos - brushStartPos);
	float2 startOffset = (samplePos - brushStartPos);

	float lineDist = length((startOffset - lineDir * dot(startOffset,lineDir)) * screenSize);
	lineDist += step(lineLength,dot(startOffset,lineDir)) * brushRadius * 2;
	lineDist += step(dot(startOffset,lineDir),0) * brushRadius * 2;

	// Distance from end circles
	float startCapDist = length((samplePos - brushStartPos) * screenSize);
	float endCapDist = length((samplePos - brushEndPos) * screenSize);

	float sampleDist = min(min(startCapDist,endCapDist),lineDist);

	return (1 + sign(brushRadius - sampleDist)) / 2.0;

}

// Returns antialiased brushSample value
float brushSampleAA(float2 samplePos) {

	// Dimensions of a pixel in screen space
	float2 pixelSize = 1 / screenSize;


	// TODO: -------------------------
	// Calculate the position of each sub-sample such that
	// the resulting average will approximate the percentage
	// of the pixel that is inside the brush outline

	float sample = 0;

	const int N = 4;

	for (int i = 0;i < N;i++) {
		for (int j = 0;j < N;j++) {

			float2 subsamplePos = samplePos; // <--- Change this

			sample += brushSample(subsamplePos) / (N * N);

		}
	}

	// -------------------------------

	return sample;

}