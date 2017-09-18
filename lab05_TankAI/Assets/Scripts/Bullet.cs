using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float lifeTime = 5f;
	public float damage = 20;
	public Tank owner;

	float startTime;

	void Start () {
		startTime = Time.time;
	}

	void Update () {
		if (Time.time > startTime + lifeTime) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		Tank hitTank = other.GetComponent<Tank> ();

		if (hitTank != null && hitTank != owner) {
			hitTank.ApplyDamage (damage);
			Destroy (gameObject);
		}

	}
}
