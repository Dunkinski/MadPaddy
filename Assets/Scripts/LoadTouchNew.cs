using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadTouchNew : MonoBehaviour {

	public GUITexture LevelButton;
	public int LevelNumber;
	
	void Awake()
	{
		LevelButton = GameObject.Find ("Level"+LevelNumber+"Load").GetComponent<GUITexture>();
	}
	
	void Update()
	{
		foreach (Touch touch in Input.touches)
			if (touch.phase == TouchPhase.Stationary && LevelButton.HitTest (touch.position)) 
		{
                SceneManager.LoadScene("Level"+LevelNumber);
			
		}
	}
	
}
