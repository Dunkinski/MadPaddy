#pragma strict

var paused : boolean = false;

var MyCheck : boolean = false;

function Update ()
{
	if (Input.GetKeyDown(KeyCode.P))
	{
	if (!paused) {
	
	Time.timeScale = 0;
	paused = true;
	}
	else
	{
	
	Time.timeScale = 1;
	paused = false;
	
			
	}
	}
}

function OnGUI ()
{
	if ( paused )
	{
		if (GUI.Button (Rect ( 10, 10, 100, 30), "Resume"))
		{
			Time.timeScale = 1.0f;
			paused = false;
		}
			
			if (GUI.Button (Rect( 10, 50, 100, 30), "Options")) 
			{
			
			}
			
			if (GUI.Button (Rect( 10, 100, 100, 30), "Quit")) 
			{
			Application.Quit();
		}
	}
}