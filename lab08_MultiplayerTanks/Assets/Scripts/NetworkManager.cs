using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {

	public string serverIP;

	public bool runAsClient = true;
	public bool webPlayerMode = false;

	public UnityEngine.UI.Text connectedText;

	public Tank playerTankPrefab;
	public GameObject bulletImposterPrefab;

	Player player;

	const int clientPort = 8888;
	const int serverPort = 8887;

	const int maxPeers = 20;
	int clientCount = 0;
	byte[] buffer = new byte[16384];

	int channel;
	int host;
	int serverConnection;

	Dictionary<int,Tank> clientTanks = new Dictionary<int,Tank>();
	Dictionary<int,Tank> peerTanks = new Dictionary<int,Tank>();
	Dictionary<int,Rigidbody2D> bulletImposters = new Dictionary<int,Rigidbody2D>();
	BinaryFormatter formatter = new BinaryFormatter();

	void Start () {

		player = GameObject.FindObjectOfType<Player> ();

		NetworkTransport.Init ();

		var config = new ConnectionConfig ();
		channel = config.AddChannel (QosType.ReliableFragmented);

		if (runAsClient) {

			var topology = new HostTopology (config, 1);
			host = NetworkTransport.AddHost (topology, clientPort);

			byte err;
			serverConnection = NetworkTransport.Connect (host, serverIP, serverPort, 0, out err);

		} else {

			var topology = new HostTopology (config, maxPeers);

			if (webPlayerMode) {
				host = NetworkTransport.AddWebsocketHost (topology, serverPort);
			} else {
				host = NetworkTransport.AddHost (topology, serverPort);
			}

		}

	}

	void HandleMessageClient(ServerToClientMessage msg) {

		foreach (var bulletID in msg.bullets.Keys) {
			
			var bulletInfo = msg.bullets [bulletID];

			if (!bulletImposters.ContainsKey (bulletID)) {

				// Create local copy of new bullet imposter
				bulletImposters [bulletID] = Instantiate (bulletImposterPrefab).GetComponent<Rigidbody2D>();

			}

			// Local bullet object
			Rigidbody2D imposter = bulletImposters [bulletID];

			// TODO: -------------------------
			// Update position and velocity of local bullet imposter



			// -------------------------------

		}

		// Destroy local bullet imposters which no longer exist on the server
		var bulletKeysList = new List<int> (bulletImposters.Keys);
		foreach (var bulletID in bulletKeysList) {
			if (!msg.bullets.ContainsKey (bulletID)) {
				
				Destroy (bulletImposters[bulletID].gameObject);
				bulletImposters.Remove (bulletID);

			}
		}

		foreach (var tankID in msg.enemyTanks.Keys) {

			var tankInfo = msg.enemyTanks [tankID];

			if (!peerTanks.ContainsKey (tankID)) {

				// Create local copy of new enemy tank
				peerTanks [tankID] = (Tank)Instantiate (playerTankPrefab);

			}

			// Local tank object
			Tank tank = peerTanks [tankID];

			// TODO: -------------------------
			// Update local enemy tank properties



			// -------------------------------

		}

		// Destroy local tanks which no longer exist on the server
		var tankKeysList = new List<int> (peerTanks.Keys);
		foreach (var tankID in tankKeysList) {
			if (!msg.enemyTanks.ContainsKey (tankID)) {
				
				Destroy (peerTanks[tankID].gameObject);
				peerTanks.Remove (tankID);

			}
		}

		// TODO: -------------------------
		// Update player tank (player.tank) properties



		// -------------------------------
	}

	void HandleMessageServer(int connection, ClientToServerMessage msg) {

		Tank tank = clientTanks [connection];

		tank.cannon.transform.up = msg.cannonDirection;

		if (msg.turned) {
			tank.TurnToward (msg.turnDirection);
		}
		if (msg.movedBackward) {
			tank.MoveBackward ();
		}
		if (msg.movedForward) {
			tank.MoveForward ();
		}
		if (msg.fired) {
			tank.cannon.Fire ();
		}
	}

	public void SendClientUpdate(ClientToServerMessage msg) {

		byte err;
		var stream = new MemoryStream ();
		formatter.Serialize (stream, msg);
		NetworkTransport.Send (host, serverConnection, channel, stream.GetBuffer(), (int)stream.Length, out err);

	}

	void ServerUpdate() {

		foreach (var client in clientTanks.Keys) {

			var msg = new ServerToClientMessage ();

			msg.bullets = new Dictionary<int,BulletInfo> ();
			foreach (var bulletObj in GameObject.FindObjectsOfType<Bullet>()) {
				if (bulletObj.owner.GetInstanceID() == clientTanks[client].GetInstanceID()) {
					continue;
				}
				msg.bullets[bulletObj.id] = new BulletInfo (bulletObj);
			}

			msg.enemyTanks = new Dictionary<int,TankInfo>();

			foreach (var otherClient in clientTanks.Keys) {
				if (otherClient == client) {
					continue;
				}
				msg.enemyTanks [otherClient] = new TankInfo (clientTanks[otherClient]);
			}

			msg.playerTank = new TankInfo (clientTanks [client]);

			byte err;
			var stream = new MemoryStream ();
			formatter.Serialize (stream, msg);
			NetworkTransport.Send (host, client, channel, stream.GetBuffer(), (int)stream.Length, out err);

		}

	}

	void FixedUpdate () {

		byte err;
		while (true) {

			int connection, channel, bytesRecieved;

			var evt = NetworkTransport.ReceiveFromHost (host, out connection, out channel, buffer, buffer.Length, out bytesRecieved, out err);

			if (evt == NetworkEventType.Nothing) {
				break;
			}

			if (bytesRecieved >= buffer.Length) {
				continue;
			}

			switch (evt) {

			case NetworkEventType.DataEvent:
				var msg = formatter.Deserialize (new MemoryStream (buffer));

				if (runAsClient) {
					HandleMessageClient ((ServerToClientMessage)msg);
				} else {
					HandleMessageServer (connection, (ClientToServerMessage)msg);
				}
				break;

			case NetworkEventType.ConnectEvent:

				if (!runAsClient) {

					clientCount++;
					connectedText.text = "Clients: " + clientCount.ToString();

					var newTank = (Tank)Instantiate (playerTankPrefab);
					newTank.transform.position = Random.insideUnitCircle * 10;

					clientTanks [connection] = newTank;
				} else {

					connectedText.text = "Connected";

				}

				break;

			case NetworkEventType.DisconnectEvent:

				if (!runAsClient) {

					clientCount--;
					connectedText.text = "Clients: " + clientCount.ToString();

					Destroy (clientTanks [connection].gameObject);
					clientTanks.Remove (connection);
				} else {

					connectedText.text = "Not Connected";

				}

				break;
			}

		}
		if (!runAsClient) {

			ServerUpdate ();

		}

	}

}
