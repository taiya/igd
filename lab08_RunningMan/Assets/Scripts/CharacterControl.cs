using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	Animator animator;

	void Start () {

		animator = GetComponent<Animator> ();

	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.A)) {
			animator.SetBool("TurningLeft",true);
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			animator.SetBool("TurningLeft",false);
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			animator.SetBool("TurningRight",true);
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			animator.SetBool("TurningRight",false);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			animator.SetBool("Running",true);
		}
		if (Input.GetKeyUp(KeyCode.W)) {
			animator.SetBool("Running",false);
		}

	}
}
