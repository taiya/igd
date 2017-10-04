using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushType {
	Paint,
	Blur,
	Edges,
}

public class Painter : MonoBehaviour {

	public Material paintMaterial;
	public Material blurMaterial;
	public Material edgesMaterial;
	public Material texDrawMaterial;

	public BrushType brushType = BrushType.Paint;
	public Color brushColor = Color.red;
	public float brushRadius = 10;

	public bool painting = false;

	RenderTexture frontBuffer;
	RenderTexture backBuffer;

	Vector3 prevMousePos;
	float prevWidth;
	float prevHeight;

	public void SetPaintMode() {
		brushType = BrushType.Paint;
	}

	public void SetBlurMode() {
		brushType = BrushType.Blur;
	}

	public void SetEdgesMode() {
		brushType = BrushType.Edges;
	}

	public void SetRadius(float radius) {
		brushRadius = radius;
	}

	public void SetRed(float red) {
		brushColor.r = red;
	}

	public void SetGreen(float green) {
		brushColor.g = green;
	}

	public void SetBlue(float blue) {
		brushColor.b = blue;
	}

	public void SetAlpha(float alpha) {
		brushColor.a = alpha;
	}

	public void DrawTexture(Texture texture) {

		float umin, umax, vmin, vmax;

		if (texture.width / texture.height > Screen.width / Screen.height) {

			umin = 0;
			umax = 1;

			vmin = (1 - ((float)texture.height / ((float)texture.width / (float)Screen.width)) / (float)Screen.height) / 2;
			vmax = 1 - vmin;

		} else {

			vmin = 0;
			vmax = 1;

			umin = (1 - ((float)texture.width / ((float)texture.height / (float)Screen.height)) / (float)Screen.width) / 2;
			umax = 1 - umin;

		}

		texDrawMaterial.SetFloat ("umin", umin);
		texDrawMaterial.SetFloat ("umax", umax);
		texDrawMaterial.SetFloat ("vmin", vmin);
		texDrawMaterial.SetFloat ("vmax", vmax);

		Graphics.Blit (texture,frontBuffer,texDrawMaterial);
		Graphics.Blit (texture,backBuffer,texDrawMaterial);

	}

	void ResizeScreen() {
		var newBack = new RenderTexture (Screen.width, Screen.height, 24);
		var newFront = new RenderTexture (Screen.width, Screen.height, 24);

		Graphics.Blit (backBuffer, newBack);
		Graphics.Blit (frontBuffer, newFront);

		backBuffer.Release ();
		frontBuffer.Release ();

		frontBuffer = newFront;
		backBuffer = newBack;
	}

	void Start () {

		frontBuffer = new RenderTexture (Screen.width, Screen.height, 24);
		backBuffer = new RenderTexture (Screen.width, Screen.height, 24);

		prevMousePos = Input.mousePosition;

		prevWidth = Screen.width;
		prevHeight = Screen.height;
		
	}

	void Update() {

		if (prevWidth != Screen.width || prevHeight != Screen.height) {
			ResizeScreen ();
		}

		prevWidth = Screen.width;
		prevHeight = Screen.height;
	}

	void OnRenderImage (RenderTexture src, RenderTexture dest) {

		if (painting && Input.mousePosition != prevMousePos) {

			Material material = null;

			switch (brushType) {

			case BrushType.Paint:

				material = paintMaterial;

				break;

			case BrushType.Blur:

				material = blurMaterial;

				break;

			case BrushType.Edges:

				material = edgesMaterial;

				break;

			}

			var endPos = new Vector4 (Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0, 0);
			var startPos = new Vector4 (prevMousePos.x / Screen.width, prevMousePos.y / Screen.height, 0, 0);
			var screenSize = new Vector4 (Screen.width, Screen.height, 0, 0);

			material.SetFloat ("brushRadius", brushRadius);
			material.SetVector ("brushStartPos", startPos);
			material.SetVector ("brushEndPos", endPos);
			material.SetVector ("screenSize", screenSize);
			material.SetVector ("brushColor", brushColor);

			for (int i = 0; i < material.passCount; i++) {

				Graphics.Blit (frontBuffer, backBuffer, material, i);

				var temp = frontBuffer;
				frontBuffer = backBuffer;
				backBuffer = temp;

			}

		}

		Graphics.Blit (frontBuffer, dest);

		prevMousePos = Input.mousePosition;

	}
}
