using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDespawn : MonoBehaviour {

	float spawnTime;

	void Start () {
		spawnTime = Time.time;
	}

	void Update () {
		if (Time.time > spawnTime + 4) {
			Destroy (this.gameObject);
		}
	}
}
