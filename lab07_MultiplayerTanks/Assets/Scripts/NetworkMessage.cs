using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerialVector {

	public float x, y, z;

	public static implicit operator SerialVector(Vector3 v) {

		var sv = new SerialVector ();
		sv.x = v.x;
		sv.y = v.y;
		sv.z = v.z;

		return sv;
	}

	public static implicit operator SerialVector(Vector2 v) {

		var sv = new SerialVector ();
		sv.x = v.x;
		sv.y = v.y;
		sv.z = 0;

		return sv;
	}

	public static implicit operator Vector3(SerialVector sv) {

		var v = new Vector3 ();
		v.x = sv.x;
		v.y = sv.y;
		v.z = sv.z;

		return v;
	}

	public static implicit operator Vector2(SerialVector sv) {

		var v = new Vector2 ();
		v.x = sv.x;
		v.y = sv.y;

		return v;
	}

}

[System.Serializable]
public class TankInfo {

	public SerialVector position;
	public SerialVector heading;
	public SerialVector cannonDirection;

	public float health;

	public TankInfo(Tank tank) {
		position = tank.transform.position;
		heading = tank.transform.up;
		cannonDirection = tank.cannon.transform.up;

		health = tank.health;
	}

	public void ApplyTo(Tank tank) {

		// TODO (Optional): --------------
		// Update tank properties from TankInfo object



		// -------------------------------

	}

}

[System.Serializable]
public class BulletInfo {

	public SerialVector position;
	public SerialVector velocity;

	public BulletInfo(Bullet bullet) {
		velocity = bullet.body.velocity;
		position = bullet.transform.position;
	}

}

[System.Serializable]
public class ClientToServerMessage {

	public bool turned = false;
	public SerialVector turnDirection;

	public SerialVector cannonDirection;

	public bool movedForward = false;
	public bool movedBackward = false;

	public bool fired = false;

}

[System.Serializable]
public class ServerToClientMessage {

	public Dictionary<int,TankInfo> enemyTanks;
	public Dictionary<int,BulletInfo> bullets;

	public TankInfo playerTank;

}