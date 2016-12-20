using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	public GUITexture RespawnButton;

	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;

	public static int levelCount = 2;
	public static int currentLevel = 1;

	 void Awake()
	{
		//RespawnButton = GameObject.Find ("Respawn").guiTexture;
	}

	void Start () {
		cam = GetComponent<GameCamera>();

		if (GameObject.FindGameObjectWithTag("Spawn")) {
			checkpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		}

		SpawnPlayer(checkpoint);
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) {
		currentPlayer = Instantiate(player,spawnPos,Quaternion.identity) as GameObject;
		cam.SetTarget(currentPlayer.transform);
	}

	private void Update() {
		if (!currentPlayer) {
			//foreach (Touch touch in Input.touches)
				if //(touch.phase == TouchPhase.Stationary && RespawnButton.HitTest (touch.position))
					(Input.GetButtonDown("Respawn")) 
			{
				SpawnPlayer(checkpoint);
			}
		}
	}

	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void EndLevel() {
		if (currentLevel < levelCount) {
			currentLevel++;
            SceneManager.LoadScene("Level " + currentLevel);
		}
		else {
			Debug.Log("Game Over");
		}
	}

}
