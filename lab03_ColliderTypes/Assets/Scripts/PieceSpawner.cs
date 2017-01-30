using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour {

	public UnityEngine.UI.Text piecesText;
	public UnityEngine.UI.Text fpsText;

	public GameObject[] prefabs;

	Camera camera;

	int piecesCount = 0;

	float timeOfLastSpawn = 0;

	Queue<float> frameTimes = new Queue<float>();

	void Start () {
		camera = GetComponent<Camera> ();
	}

	// Instantiate a randomly chosen prefab
	void SpawnPiece () {

		// Get mouse world position
		var mouseWorldPos = camera.ScreenToWorldPoint (Input.mousePosition);
		mouseWorldPos.z = 0;

		// Choose random prefab
		var prefab = prefabs [Random.Range (0, prefabs.Length)];

		// Instantiate with random rotation
		var newPiece = Instantiate (prefab, mouseWorldPos, Quaternion.AngleAxis (Random.Range (0, 360), Vector3.forward));

		// Apply random color
		newPiece.GetComponent<SpriteRenderer> ().color = Random.ColorHSV (0, 1, 1, 1, 0.7f, 0.7f);
	
		// Prevent objects from spawning on top of each other
		newPiece.transform.position += 6 * new Vector3(Random.value - 0.5f,Random.value - 0.5f);

		// Update counter
		piecesCount++;
		piecesText.text = piecesCount.ToString ();

	}

	void Update () {

		var now = Time.realtimeSinceStartup;

		frameTimes.Enqueue (now);

		if (frameTimes.Count > 30) {
			frameTimes.Dequeue ();
		}

		var fps = 1 / ((now - frameTimes.Peek ()) / (frameTimes.Count - 1));
		fpsText.text = Mathf.RoundToInt (fps).ToString ();

		if (Input.GetMouseButton (0) && Time.time - timeOfLastSpawn > 0.3f) {
			
			SpawnPiece ();

			timeOfLastSpawn = Time.time;

		}

		if (Input.GetMouseButton (1) && Time.time - timeOfLastSpawn > 0.01f) {

			SpawnPiece ();

			timeOfLastSpawn = Time.time;

		}

		camera.orthographicSize *= Mathf.Exp (1 * (Input.mouseScrollDelta.x + Input.mouseScrollDelta.y));
		camera.orthographicSize = Mathf.Min (camera.orthographicSize, 200);
		camera.orthographicSize = Mathf.Max (camera.orthographicSize, 1);

		var newCamPosition = camera.transform.position;
		newCamPosition.y = 0.8f * camera.orthographicSize;
		camera.transform.position = newCamPosition;
	}
}
