using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //< Text

public class GoalArea : MonoBehaviour {
	int score = 0;
	private void set_score_in_ui(){ 
		GameObject.Find ("NumPoints").GetComponent<Text> ().text = ""+score;
	}
	public void increase_score(){
		score++;
		set_score_in_ui ();
	}
	public void decrease_score(){
		score--;
		set_score_in_ui ();
	}

	// https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html
	// https://unity3d.com/learn/tutorials/topics/physics/colliders-triggers
	void OnTriggerEnter2D(Collider2D other) {
		Destroy (other.gameObject, .5f);
		increase_score();
	}
}
