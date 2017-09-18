using UnityEngine;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {

    public Meteor shot;
    public bool canFire = true;

    public CannonUI powerui;

    public float powerMin = 5;
    public float powerMax = 100;
    public float powerSensitivity = 50;
    private float power = 100;

    public float turnRate = 90;

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, 0f, turnRate * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, 0f, -turnRate * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            power = Mathf.Clamp(power + powerSensitivity * Time.deltaTime, powerMin, powerMax);
            powerui.SetPower(power, powerMax);
        }
        if (Input.GetKey(KeyCode.S))
        {
            power = Mathf.Clamp(power - powerSensitivity * Time.deltaTime, powerMin, powerMax);
            powerui.SetPower(power, powerMax);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canFire)
            {
                Meteor shotTemp = Instantiate(shot, transform.position, transform.rotation) as Meteor;
                shotTemp.cannon = this;
                canFire = false;
            }
        }
    }
}
