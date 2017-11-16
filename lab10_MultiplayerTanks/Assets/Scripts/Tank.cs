using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

	public float moveSpeed = 2;
	public float turnSpeed = 200;

	public float health = 100;

	public Cannon cannon;
	public HealthBar healthBar;

	void Start () {}

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

		if (health <= 0) {
				
			health = 100;
			healthBar.SetHealth (100);

			transform.position = Random.insideUnitCircle * 10;

		}

	}

	void Update() {

		healthBar.SetHealth (health);

	}
}
