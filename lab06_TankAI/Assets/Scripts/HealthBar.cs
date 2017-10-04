using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	SpriteRenderer sprite;

	Vector3 offset;

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		offset = transform.position - transform.parent.position;
	}

	void LateUpdate () {
		
		transform.up = Vector3.up;
		transform.position = transform.parent.position + offset;

	}

	public void SetHealth(float value) {

		var currentScale = transform.localScale;
		currentScale.x = 5 * value / 100;
		transform.localScale = currentScale;

		sprite.color = Color.HSVToRGB (0.3f * value / 100, 1, 1);

	}
}
