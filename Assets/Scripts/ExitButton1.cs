using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitButton1 : MonoBehaviour
{
	public GUITexture ExitButtonMain;
	
	void Awake()
	{
		ExitButtonMain = GameObject.Find ("ExitToMain").GetComponent<GUITexture>();
	}
	
	void Update()
	{
		foreach (Touch touch in Input.touches)
			if (touch.phase == TouchPhase.Stationary && ExitButtonMain.HitTest (touch.position)) 
		{
                SceneManager.LoadScene("StartingPart");
			
		}
	}
	
}