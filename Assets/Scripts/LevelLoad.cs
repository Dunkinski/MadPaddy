using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour {
	public int LevelNumber;
	public GUITexture LevelComplete;
	private float timeLoad;

	
	private void Update() {
			
		if (timeLoad != 0){
			if(timeLoad < Time.time) {
                SceneManager.LoadScene("StartingPart");		
			}
		}
			
		}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.name.Equals("Player(Clone)"))
		{
		LevelComplete.gameObject.SetActive(true);
		try{
				Debug.Log ("SAVE START");
		GameData Data = SaveScript.Load();
				Debug.Log ("SAVE END");
				Debug.Log (LevelNumber.ToString());
		if(Data.UnlockedLevels <= LevelNumber)
		{
			Data.UnlockedLevels = LevelNumber+1;
			SaveScript.Save(Data);
		}
				Debug.Log ("END OF SCRIPT");
		}
		catch(Exception ex)
		{
		
		}
			GameObject.Find("Player(Clone)").GetComponent<PlayerControlTouchNew>().enabled = false;
			GameObject.Find("Player(Clone)").GetComponent<Animator>().enabled = false;
			timeLoad = Time.time+5;

		}

	}
}
