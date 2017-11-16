using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public Rigidbody2D bulletPrefab;

	public float bulletSpeed = 10;

	public float coolDownTime = 0.2f;

	float lastFireTime;

	void Start() {
		
	}

	public void AimToward(Vector3 direction) {
		transform.up = direction;
	}

	public void Fire() {

		if (lastFireTime + coolDownTime > Time.time) {
			return;
		}
		lastFireTime = Time.time;
		
		var newBullet = Instantiate (bulletPrefab,transform.position,transform.rotation);
		newBullet.velocity = transform.up * bulletSpeed;
		newBullet.GetComponent<Bullet> ().owner = GetComponentInParent<Tank> ();

	}

}
