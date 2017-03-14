using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float lifeTime = 5f;
	public float damage = 20;
	public Tank owner;
	public Rigidbody2D body;

	public int id;

	float startTime;

	static int nextID = 0;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
		startTime = Time.time;
		id = nextID;
		nextID++;
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
