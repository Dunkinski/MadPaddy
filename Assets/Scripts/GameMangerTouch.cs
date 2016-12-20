using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameMangerTouch : MonoBehaviour {

	public GameObject ResButton;
	public GameObject player;
	public GUITexture RespawnButton;
	public bool RespawnTrue = false;
	
	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;
	
	public static int levelCount = 2;
	public static int currentLevel = 1;

	public List<GameObject> Falling;
	
	void Awake()
	{
		//RespawnButton = GameObject.Find ("Respawn").GetComponent<GUITexture>();
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
		cam.transform.position = new Vector3 (currentPlayer.transform.position.x, currentPlayer.transform.position.y, cam.transform.position.z);
	}
	
	private void Update() {
		if (!currentPlayer) {
			foreach (Touch touch in Input.touches)
				if (touch.phase == TouchPhase.Stationary && RespawnButton.HitTest (touch.position))
					RespawnButton.GetComponent<GUITexture>().enabled = true;

			{
				SpawnPlayer(checkpoint );
			}

				
			}
		}

	
	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void respawn()
	{
		foreach (GameObject tempGO in Falling)
		{
			tempGO.SetActive(true);
		}
		GameObject.FindGameObjectWithTag("Player").transform.position = checkpoint;
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlTouchNew>().health = 100;
	}
	
	public void EndLevel() {
		if (currentLevel < levelCount) {
			currentLevel++;
            SceneManager.LoadScene("Level8" + currentLevel);
		}
		else {
			Debug.Log("Game Over");
		}
	}
	
}
