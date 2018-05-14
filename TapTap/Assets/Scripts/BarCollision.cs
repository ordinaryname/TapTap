using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCollision : MonoBehaviour {

	private GameObject resetBall;

	void OnCollisionEnter(Collision collision){
		if (!GameControl.instance.gameOver) {
			if (collision.collider.name != "ResetBall") {
				BallCollision ();
			} else if (collision.collider.name == "ResetBall") {
				ResetBallCollision ();
			}
		}
	}

	private void BallCollision(){
		GameControl.instance.PlayerLost ();
	}

	private void ResetBallCollision(){
		
	}
}
