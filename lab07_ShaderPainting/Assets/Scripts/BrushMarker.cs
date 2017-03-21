using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushMarker : MonoBehaviour {

	public float radius = 10;

	RectTransform rectTransform;
	Image image;

	public void SetRadius(float radius) {
		this.radius = radius;
	}

	public void SetRed(float red) {
		var color = image.color;
		color.r = red;
		image.color = color;
	}

	public void SetGreen(float green) {
		var color = image.color;
		color.g = green;
		image.color = color;
	}

	public void SetBlue(float blue) {
		var color = image.color;
		color.b = blue;
		image.color = color;
	}

	public void SetAlpha(float alpha) {
		var color = image.color;
		color.a = alpha;
		image.color = color;
	}

	void Start () {
		rectTransform = GetComponent<RectTransform> ();
		image = GetComponent<Image> ();
	}

	void Update () {

		rectTransform.offsetMin = new Vector2(Input.mousePosition.x - radius,Input.mousePosition.y - radius);
		rectTransform.offsetMax = new Vector2(Input.mousePosition.x + radius,Input.mousePosition.y + radius);

	}
}
