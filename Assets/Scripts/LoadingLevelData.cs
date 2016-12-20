using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingLevelData : MonoBehaviour {
	public LoadingLevelData LLD;
	public int Levels;
	// Use this for initialization

	void Start()
	{
		GameData Data = SaveScript.Load();
		Debug.Log("Levels "+Data.UnlockedLevels);
		for(int i = 0; i < Levels; i++)
		{
			if(i < Data.UnlockedLevels)
			{
				Debug.Log("Aktiv");
				GameObject.Find("Level"+(i+1)+"Load").SetActive(true);
				if(i == 1)
				{
				GameObject.Find ("Level"+(i+1)+"Load").GetComponent<LoadTouchNew>().enabled =true;
				}
			}
			else
			{
				Debug.Log("Passiv");
				GameObject.Find("Level"+(i+1)+"Load").SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
