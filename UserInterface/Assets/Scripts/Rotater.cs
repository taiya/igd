using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

	public void Rotate(float angle) {
		transform.Rotate(0.0f, 0.0f, -angle);
	}

}
