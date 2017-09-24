using UnityEngine;
using System.Collections.Generic;

public class Meteor : MonoBehaviour {

	void Start(){

        /* TODO: initalize the list of planet instances */

    }

	void FixedUpdate(){

        /* TODO: compute and apply gravitational forces */

	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	// https://docs.unity3d.com/ScriptReference/Collider2D.OnCollisionEnter2D.html
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "planet") {
			coll.gameObject.SetActive (false);
			GameObject.Find("GoalArea").SendMessage("decrease_score");
		}
	}
}
