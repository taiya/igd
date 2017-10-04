using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetExplosion : MonoBehaviour {

	public GameObject deathExplosion;

	float startAltitude;

	AudioSource audio;

	void Start () {
		startAltitude = transform.position.y;

		audio = GetComponent<AudioSource> ();
	}

	IEnumerator Respawn() {

		var parentBody = GetComponent<Rigidbody> ();

		transform.rotation = Quaternion.identity;
		parentBody.velocity = Vector3.zero;
		parentBody.angularVelocity = Vector3.zero;

		var startPos = transform.position;

		var endPos = new Vector3(transform.position.x,startAltitude,transform.position.z);
		var spawnOffset = Random.insideUnitCircle * 500;
		endPos += new Vector3 (spawnOffset.x,0,spawnOffset.y);

		foreach (var renderer in GetComponentsInChildren<Renderer>()) {
			renderer.enabled = false;
		}

		var startTime = Time.time;
		var endTime = startTime + 1;
		while (Time.time < endTime) {

			transform.position = startPos + (endPos - startPos) * (Time.time - startTime) / (endTime - startTime);

			yield return null;
		}

		foreach (var renderer in GetComponentsInChildren<Renderer>()) {
			renderer.enabled = true;
		}

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine(Respawn());
		}
	}

	void OnCollisionEnter (Collision collision) {

		var explosion = Instantiate (deathExplosion,this.transform.position,this.transform.rotation);

		StartCoroutine (Respawn());

	}
}
