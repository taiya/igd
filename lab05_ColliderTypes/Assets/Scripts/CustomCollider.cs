using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour {

	public bool overrideDetection;

	public float radius;
	public float cor = 0.5f; // Coeffecient of Restitution

	Rigidbody2D rigidbody;
	CircleCollider2D circleCollider;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		circleCollider = GetComponent<CircleCollider2D> ();

		circleCollider.enabled = true;
		circleCollider.isTrigger = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		var customCollider = other.GetComponent<CustomCollider> ();

		if (customCollider != null && !overrideDetection) {
			ResolveCollision (customCollider);
		}
	}

	void ResolveCollision(CustomCollider other) {

		// TODO: -------------
		// Teleport and apply impulses to the colliding objects 
		// to resolve the collision.



		// -------------------

	}

	void FixedUpdate () {

		var otherColliders = GameObject.FindObjectsOfType<CustomCollider> ();

		if (overrideDetection) {

			foreach (var other in otherColliders) {

				// TODO: (bonus) -----
				// for each object pair, detect and resolve any collisions manually



				// -------------------
			}

		}

		// TODO: -------------
		// Detect and resolve floor collisions



		// -------------------

	}
}
