using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour 
{
	public float timer = 5f;
	public string levelToLoad = "MainMenu";

	// Use this for initialization
	void Start () 
	{
		Invoke ("nextLevel", timer);
	}


	void nextLevel()
	{
        SceneManager.LoadScene(levelToLoad);
	}
	

}
