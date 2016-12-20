using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer: MonoBehaviour {


	public float seconds = 300;
	public float miliseconds = 0;
	public GUITexture LevelFailed;
	private bool failed = false;
	//public int CloverCollected;
	//public int CloverTotal;

	public string LevelString = "StartingPart";

	void Start()
	{
		//CloverTotal = GameObject.FindGameObjectsWithTag("Clover").Length;

	}
	void Update()
		{
			Minutes ();
	}
	void OnGUI()
	{
		GUI.Label(new Rect (10, 10, 300, 100),"Time Left: " + seconds.ToString ());
		//GUI.Label(new Rect(30, 60, 200, 50), "Clovers: "+ CloverCollected + "/" + CloverTotal);
	}

	void Minutes()
	{
		
			if(seconds >= 0){
				if (seconds <= 0) {

                LevelFailed.gameObject.SetActive(true);
                if (failed == false)
                {
                    failed = true;
                    StartCoroutine(waiting());
                }

            } 
				if (miliseconds <= 0) {
					seconds--;
					miliseconds = 60;
				}
				miliseconds -= Time.deltaTime * 50;
                
                    

                
			}
		
	}
	IEnumerator waiting()
	{
		yield return new WaitForSeconds(2);
        SceneManager.LoadScene(LevelString);
	}

	public void LevelLoad()
	{
		Invoke("LoadLevelNow",5);
	}
	private void LoadLevelNow()
	{
		SceneManager.LoadScene("StartingPart");
	}

}