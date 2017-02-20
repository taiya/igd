using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour {

	public int numberOfTanks = 6;

	public GameObject tankPrefab;

	void SpawnTank () {
		
		var newTank = Instantiate (tankPrefab);

		newTank.transform.position = Random.insideUnitCircle * 10;

	}

	void Update () {

		var tanksList = GameObject.FindObjectsOfType<TankAI> ();

		if (tanksList.Length < numberOfTanks) {
			SpawnTank ();
		}
		
	}
}
