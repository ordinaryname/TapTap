using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

	public Rigidbody ballBody;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, -4f / GameControl.instance.gameSpeed, 0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GameControl.instance.gameOver == true) {
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}
}
