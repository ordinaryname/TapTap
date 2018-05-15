using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	public static GameControl instance;
	public bool gameOver = false;
	public GameObject gameOverPanel;
	public Text scoreText;
	public int score = 0;
	public int highScore = 0;
	public GameObject bar;
	public GameObject ballPrefab;
	public GameObject resetBall;
	private int poolSize = 4;
	public float gameSpeed = 1f;

	private GameObject[] balls;
	private Vector3 poolPosition = new Vector3 (10, 10, 10);
	private Vector3[] spawnPositions = new Vector3[4];
	public int lastSpawnPosition = 0;
	public int randomSpawnPosition = 0;
	private GameObject anObject = null;
	private Vector3 mouse0;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		spawnPositions [0] = new Vector3 (-2.25f, 6f, 0f); //Far Left
		spawnPositions [1] = new Vector3 (-0.75f, 6f, 0f); //Left
		spawnPositions [2] = new Vector3 (0.75f, 6f, 0f); //Right
		spawnPositions [3] = new Vector3 (2.25f, 6f, 0f); //Far Right

		balls = new GameObject[poolSize];
		for (int i = 0; i < poolSize; i++) {
			balls [i] = (GameObject)Instantiate (ballPrefab, poolPosition, Quaternion.identity);
			balls [i].name = "ball" + i.ToString ();
			balls [i].transform.position = spawnPositions [i];
			balls [i].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString ();
		if (!gameOver) {
			if (Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					Ray mouseRay = GenerateMouseRay (Input.GetTouch (0).position);
					RaycastHit hit;
					if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit)) {
						anObject = hit.transform.gameObject;
						if (anObject.name != "ResetBall") {
							PlayerScored ();
							RespawnBall (anObject);
						}
					}
				} else if (Input.GetTouch (0).phase == TouchPhase.Ended && anObject) {
					anObject = null;
				}
			}
		}
	}

	public void PlayerScored(){
		if (gameOver) {
			return;
		}

		score++;
	}

	public void PlayerLost(){
		gameOver = true;
		gameOverPanel.SetActive (true);
	}

	private void RespawnBall(GameObject theBall){
		
	}

	Ray GenerateMouseRay(Vector3 touchPosition){
		Vector3 mousePositionFar = new Vector3 (touchPosition.x, touchPosition.y, Camera.main.farClipPlane);
		Vector3 mousePositionNear = new Vector3 (touchPosition.x, touchPosition.y, Camera.main.nearClipPlane);
		Vector3 mousePositionF = Camera.main.ScreenToWorldPoint (mousePositionFar);
		Vector3 mousePositionN = Camera.main.ScreenToWorldPoint (mousePositionNear);
		Ray mouseRay = new Ray (mousePositionN, mousePositionF - mousePositionN);
		return mouseRay;
	}
}
