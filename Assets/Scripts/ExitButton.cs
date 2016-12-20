using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour
{
	public GUITexture ExitButton1;
	
	void Awake()
	{
		ExitButton1 = GameObject.Find ("Exit").GetComponent<GUITexture>();
	}
	
	void Update()
	{
		foreach (Touch touch in Input.touches)
			if (touch.phase == TouchPhase.Stationary && ExitButton1.HitTest (touch.position)) 
		{
			Application.Quit();
			
		}
	}
	
}