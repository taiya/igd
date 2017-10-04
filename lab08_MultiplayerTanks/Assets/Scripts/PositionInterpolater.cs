using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInterpolater : MonoBehaviour {

	Vector3 remainingDelta;

	public void SetTargetPosition(Vector3 position) {
		remainingDelta = position - transform.position;
	}

	void Start () {
		remainingDelta = Vector3.zero;
	}

	void Update () {

		var stepDelta = remainingDelta / 10;
		remainingDelta -= stepDelta;

		transform.position += stepDelta;
	}
}
