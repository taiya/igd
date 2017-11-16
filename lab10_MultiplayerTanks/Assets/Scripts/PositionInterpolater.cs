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
        // TODO: use interpolation to smoothly update the local position ( transform.position )
        // to the position given by the server ( the position SetTargetPosition() was called with )
        // transform.position = ?
    }
}
