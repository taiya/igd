using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Fireworks : MonoBehaviour {

	public AudioSource explosionSoundSourcePrefab;

	public ParticleSystem primaryExplosionParticles;

	AudioSource launchSound;
	ParticleSystem particles;

	void Start () {
		particles = GetComponent<ParticleSystem> ();
		launchSound = GetComponent<AudioSource> ();
	}

	IEnumerator PlaySecondaryExplosionSound(float delay) {

		yield return new WaitForSeconds (delay - 0.05f);

		var particleArray = new ParticleSystem.Particle[primaryExplosionParticles.particleCount];
		primaryExplosionParticles.GetParticles (particleArray);
		foreach (var particle in particleArray) {
			var soundEmitter = Instantiate (explosionSoundSourcePrefab.gameObject,particle.position,Quaternion.identity);
		}

	}

	IEnumerator PlayPrimaryExplosionSound(float delay) {

		yield return new WaitForSeconds (delay - 0.05f);

		var particleArray = new ParticleSystem.Particle[particles.particleCount];
		particles.GetParticles (particleArray);
		foreach (var particle in particleArray) {
			var soundEmitter = Instantiate (explosionSoundSourcePrefab.gameObject,particle.position,Quaternion.identity);
		}

		StartCoroutine (PlaySecondaryExplosionSound (primaryExplosionParticles.main.startLifetime.Evaluate (0)));

	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space) && particles.isStopped) {

			// TODO: -------------------------
			// Play the fireworks launch sound



			// -------------------------------

			particles.Play ();
			StartCoroutine (PlayPrimaryExplosionSound (particles.main.startLifetime.Evaluate (0)));

		}
		
	}
}
