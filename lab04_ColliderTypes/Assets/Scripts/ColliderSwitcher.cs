using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSwitcher : MonoBehaviour {

	static int currentType = 1;

	CircleCollider2D circle;
	BoxCollider2D box;
	PolygonCollider2D poly;
	CustomCollider custom;

	void Start () {

		circle = GetComponent<CircleCollider2D> ();
		box = GetComponent<BoxCollider2D> ();
		poly = GetComponent<PolygonCollider2D> ();
		custom = GetComponent<CustomCollider> ();

		SwitchTypes ();
	}

	void SwitchTypes () {

		if (circle != null) {
			if (currentType == 1) {
				circle.enabled = true;
				circle.isTrigger = false;
			} else {
				circle.enabled = false;
			}
		}
		if (box != null) {
			if (currentType == 2) {
				box.enabled = true;
			} else {
				box.enabled = false;
			}
		}
		if (poly != null) {
			if (currentType == 3) {
				poly.enabled = true;
			} else {
				poly.enabled = false;
			}
		}
		if (custom != null && circle != null) {
			if (currentType == 4) {
				custom.enabled = true;
				circle.enabled = true;
				circle.isTrigger = true;
			} else {
				custom.enabled = false;
			}
		}
		
	}

	void Update () {

		if (Input.GetKey (KeyCode.Alpha1)) {
			currentType = 1;
			SwitchTypes ();
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			currentType = 2;
			SwitchTypes ();
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			currentType = 3;
			SwitchTypes ();
		} else if (Input.GetKey (KeyCode.Alpha4)) {
			currentType = 4;
			SwitchTypes ();
		}
		
	}
}
