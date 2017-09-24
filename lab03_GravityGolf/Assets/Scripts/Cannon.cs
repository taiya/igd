using UnityEngine;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {
    public Meteor shot;
    public CannonUI powerui;
    public float powerMin = 5;
    public float powerMax = 100;
    public float powerSensitivity = 50;
    public float turnRate = 90;
	private float power = 100;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(0f, 0f, turnRate * Time.deltaTime);

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(0f, 0f, -turnRate * Time.deltaTime);
		
        if (Input.GetKey(KeyCode.W)) {
            power = Mathf.Clamp(power + powerSensitivity * Time.deltaTime, powerMin, powerMax);
            powerui.SetPower(power, powerMax);
        }
        
		if (Input.GetKey(KeyCode.S)) {
            power = Mathf.Clamp(power - powerSensitivity * Time.deltaTime, powerMin, powerMax);
            powerui.SetPower(power, powerMax);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
			if(GameObject.Find("Meteor(Clone)") == null) {
                Transform muzzle = gameObject.transform.Find("Muzzle").transform;
                
                /* TODO: fire a meteor */

            }
        }
    }
}
