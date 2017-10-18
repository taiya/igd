using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour {
	private Tank tank;
	private Player player;

	public float detectionDistance = 5;
	public float fleeHealthThreshold = 40;

	public enum State {Patrolling, Attacking, Fleeing};  
	public State state = State.Patrolling;

	/// The directions that, over time, the tanks will target
	Vector2 patrolDirection;
	Vector2 attackDirection;
	Vector2 fleeDirection;


	void Start () {
		tank = GetComponent<Tank> ();
		player = GameObject.FindObjectOfType<Player> ();

		// Setup timers
		InvokeRepeating("EventSwitchDirection", 0.0f, 2.5f);
		InvokeRepeating("EventFireCannon", 0.0f, 2.0f);
	}

	Vector2 RandomDirectionTowards(Vector2 target){
		// TODO: generate a random direction towards the target direction (+/- 90 degrees)
		return Vector2.zero;
	}

	void EventSwitchDirection() {
        // This method is called periodically. See Start().

        // TODO: use RandomDirectionTowards() to set patrolDirection
        // TODO: use RandomDirectionTowards() to set attackDirection
        // TODO: use RandomDirectionTowards() to set fleeDirection
    }

    void EventFireCannon() {
        // This method is called periodically. See Start().

        switch (state) {
		case State.Attacking:
			// TODO: shoot towards the player
            // TODO(optional): make the aim of the tank a bit worse
            break;
		}
	}

	public void EventAttackDetected() {
        // TODO: when attacked a tank executes this function.. react!
	}

	void Update () {
		switch (state) {

		case State.Patrolling:
			{
				// TODO: move the tank forward

				// TODO: logic for Patrol->Attach state change

                // let the tanks heal when not in battle
				tank.heal ();

				break;
			}
				
		case State.Attacking:
			{
				// TODO: move the tank and aim towards the player

				// TODO: logic for Attack->{Patrol,Flee} state change

				break;
			}

		case State.Fleeing:
			{
				// TODO: move the tank away from player

				// TODO: logic for Flee->{Patrol} state change

				break;
			}
		}
	}

	void OnDestroy() {
		player.IncrementScore ();
	}


}
