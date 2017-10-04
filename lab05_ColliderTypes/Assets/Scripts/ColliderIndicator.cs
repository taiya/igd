using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderIndicator : MonoBehaviour {

	UnityEngine.UI.Text text;

	void Start () {
		text = GetComponent<UnityEngine.UI.Text> ();
		text.text = "Circles";
	}

	void Update () {

		if (Input.GetKey (KeyCode.Alpha1)) {
			text.text = "Circles";
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			text.text = "Boxes";
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			text.text = "Polygons";
		} else if (Input.GetKey (KeyCode.Alpha4)) {
			text.text = "Custom";
		}

	}
}
