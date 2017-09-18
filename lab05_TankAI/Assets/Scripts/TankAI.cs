using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour {

	public const int Patrolling = 0;
	public const int Attacking = 1;
	public const int Fleeing = 2;

	public float detectionDistance = 5;
	public float fleeThreshold = 40;
	public float patrolPeriod = 2.5f;
	public float cannonReloadTime = 2;
	public float followDistance = 3;

	public int state = Patrolling;

	Tank tank;

	Player player;

	Vector3 patrolDirection;
	float patrolTimer = 0;

	float cannonCooldown = 0;

	void Start () {

		tank = GetComponent<Tank> ();
		player = GameObject.FindObjectOfType<Player> ();

	}

	void Update () {

		var playerDirection = (player.transform.position - transform.position).normalized;

		switch (state) {

		case Patrolling:

			// TODO: -------------------------
			// Add patrol behaviour

			if (patrolTimer <= 0) {
				patrolTimer = patrolPeriod;

				// Change patrolDirection



			}

			patrolTimer -= Time.deltaTime;

			// Move tank



			// -------------------------------

			// TODO: -------------------------
			// Under what circumstances should state transitions occur?
			// Add logic to leave patrol state (Check for player within detection distance)



			// -------------------------------

			break;

		case Attacking:

			// TODO: -------------------------
			// Add attack behaviour

			if (cannonCooldown <= 0) {
				cannonCooldown = cannonReloadTime;

				// Fire cannon at player



			}

			cannonCooldown -= Time.deltaTime;

			// Move tank



			// -------------------------------

			// TODO: -------------------------
			// Add logic to leave attack state (Check for tank health below flee threshold)



			// -------------------------------
			
			break;

		case Fleeing:

			// TODO: -------------------------
			// Add flee behaviour



			// -------------------------------

			// TODO: -------------------------
			// Add logic to leave flee state (Check for minimum distance reached)



			// -------------------------------

			break;

		}
	}

	void OnDestroy() {
		player.IncrementScore ();
	}
}
