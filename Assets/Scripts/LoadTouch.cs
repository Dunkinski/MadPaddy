using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadTouch : MonoBehaviour 
{
	public GUITexture Level1Button;

	void Awake()
	{
		Level1Button = GameObject.Find ("Level1Load").GetComponent<GUITexture>();
	}

	void Update()
	{
			foreach (Touch touch in Input.touches)
			if (touch.phase == TouchPhase.Stationary && Level1Button.HitTest (touch.position)) 
						{
                SceneManager.LoadScene("Level1");

						}
	}

}
