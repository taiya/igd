using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public Camera camera;

	public UnityEngine.UI.Text scoreText;

	int score = 0;

	Tank tank;

	public void IncrementScore() {
		score++;
		if (scoreText != null) {
			scoreText.text = score.ToString ();
		}
	}

	public void ResetScore() {
		score = 0;
		if (scoreText != null) {
			scoreText.text = score.ToString ();
		}
	}

	void Start () {

		tank = GetComponent<Tank> ();

	}

	void Update () {

		var direction = Vector3.zero;

		if (Input.GetKey (KeyCode.W)) {
			direction += Vector3.up;
		}
		if (Input.GetKey (KeyCode.A)) {
			direction -= Vector3.right;
		}
		if (Input.GetKey (KeyCode.S)) {
			direction -= Vector3.up;
		}
		if (Input.GetKey (KeyCode.D)) {
			direction += Vector3.right;
		}

		direction.Normalize ();

		var moveForward = Vector3.Dot (direction, transform.up) > 0;

		if (direction.magnitude > Mathf.Epsilon) {
			tank.TurnToward (direction);

			if (moveForward) {
				tank.MoveForward ();
			} else {
				tank.MoveBackward ();
			}
		}

		var mousePos = camera.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;

		tank.cannon.AimToward ((mousePos - transform.position).normalized);

		if (Input.GetMouseButtonDown (0)) {
			tank.cannon.Fire ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			tank.ApplyDamage (100);
		}

	}

}
