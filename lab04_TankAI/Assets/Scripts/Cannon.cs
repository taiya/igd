using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public Rigidbody2D bulletPrefab;

	public float bulletSpeed = 10;

	AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource> ();
	}

	public void AimToward(Vector3 direction) {
		transform.up = direction;
	}

	public void Fire() {
		
		var newBullet = Instantiate (bulletPrefab,transform.position,transform.rotation);
		newBullet.velocity = transform.up * bulletSpeed;
		newBullet.GetComponent<Bullet> ().owner = GetComponentInParent<Tank> ();

		if (audio != null) {

			// TODO: -------------------------
			// Play cannon sound



			// -------------------------------
			
		}

	}

}
