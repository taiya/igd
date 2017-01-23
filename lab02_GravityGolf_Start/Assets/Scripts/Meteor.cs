using UnityEngine;
using System.Collections.Generic;

public class Meteor : MonoBehaviour {

    public Cannon cannon;
    public float lifeDuration = 5;
    private float curLife;

    void Awake()
    {
        curLife = lifeDuration;
    }
	
	// Update is called once per frame
	void Update ()
    {
        curLife -= Time.deltaTime;
        if (curLife < 0f)
        {
            OnDestroy();
        }
	}

    public void OnDestroy()
    {
        cannon.canFire = true;
        Destroy(gameObject);
    }
}
