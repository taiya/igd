using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

	public float moveSpeed = 2;
	public float turnSpeed = 200;

	public float health = 100;

	public Cannon cannon;
	public HealthBar healthBar;

	AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	public void MoveForward() {
		transform.position += transform.up * Time.deltaTime * moveSpeed;
	}

	public void MoveBackward() {
		transform.position -= transform.up * Time.deltaTime * moveSpeed;
	}

	public void TurnToward(Vector3 direction) {
		
		var axis = Vector3.Cross (direction, transform.up);

		if (axis.magnitude < 0.03) {
			axis = Vector3.zero;
		}
		var sign = -Vector3.Dot (Vector3.forward, axis.normalized);

		transform.RotateAround (transform.position, Vector3.forward, sign * Time.deltaTime * turnSpeed);

	}

	public void ApplyDamage(float damage) {
		
		health -= damage;

		healthBar.SetHealth (health);

		var ai = GetComponent<TankAI> ();
		if (ai != null) {

			// TODO: -------------------------
			// Make AI respond to attack



			// -------------------------------

		}

		if (audio != null) {

			// TODO: -------------------------
			// Play impact sound



			// -------------------------------

		}

		if (health <= 0) {

			var player = GetComponent<Player> ();

			if (player == null) {
				
				Destroy (gameObject);

			} else {
				
				health = 100;
				healthBar.SetHealth (100);

				player.ResetScore ();

				transform.position = Vector3.zero;

				var aiTanks = GameObject.FindObjectsOfType<TankAI> ();
				foreach (var tank in aiTanks) {
					Destroy (tank.gameObject);
				}

			}
		}

	}
}
