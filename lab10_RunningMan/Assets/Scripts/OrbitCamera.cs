using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

	public Transform trackedObject;

	float longitude = 0;
	float latitude = 0;
	float zoom = 1;

	Vector3 prevMousePos;
	Vector3 cameraCenter;

	void Start () {
		prevMousePos = Input.mousePosition;
	}

	void Update () {

		var mouseDelta = Input.mousePosition - prevMousePos;
		prevMousePos = Input.mousePosition;

		if (Input.GetMouseButton (0)) {

			latitude += mouseDelta.y / Screen.height;
			latitude = Mathf.Max (Mathf.Min(latitude,Mathf.PI/2 - 0.05f), 0.05f - Mathf.PI / 2);
			longitude -= 2 * mouseDelta.x / Screen.width;

		}

		zoom -= 0.1f * Input.mouseScrollDelta.y;
		zoom = Mathf.Max(Mathf.Min(zoom,3),0.1f);

		var altitude = Mathf.Exp (zoom);

		var direction = new Vector3 (
			Mathf.Cos (latitude) * Mathf.Cos (longitude), 
			Mathf.Sin (latitude), 
			Mathf.Cos (latitude) * Mathf.Sin (longitude));

		cameraCenter += (trackedObject.position - cameraCenter) / 5;

		transform.position = cameraCenter + altitude * -direction;
		transform.up = Vector3.up;
		transform.forward = direction;

	}
}
