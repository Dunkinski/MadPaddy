using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Level10EnemySpawnScript : MonoBehaviour {

	public GameObject leftSpawnPoint, rightSpawnPoint;
	public int Waves;
	public int LevelNumber = 10;
	public int EnemiesPerWave;
	public int waveNumber = 0;
	public UnityEngine.Object Enemy;
	public GUITexture LevelComplete;
  
	// Use this for initialization
	void Start () {

        LevelComplete.enabled = false;
        waveNumber = 1;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameObject.FindGameObjectsWithTag("Guard").Length == 0)
		{
			waveNumber ++;
			if(waveNumber <= Waves)
			{
				for(int i = 0; i < EnemiesPerWave; i++)
				{
					if(i%2 == 0)
					{
						GameObject leftSpawn = (GameObject)Instantiate(Enemy,leftSpawnPoint.transform.position,Quaternion.identity);
						leftSpawn.GetComponent<GuardLogicLevel10>().lookLeft = false;
						leftSpawn.transform.eulerAngles = new Vector2(0, 180);
					}
					else
					{
						GameObject rightSpawn = (GameObject)Instantiate(Enemy,rightSpawnPoint.transform.position,Quaternion.identity);
						rightSpawn.GetComponent<GuardLogicLevel10>().lookLeft = true;
					}
				}
			}
			else
			{
				LevelComplete.enabled = true;
				try{
					GameData Data = SaveScript.Load();
					if(Data.UnlockedLevels <= LevelNumber)
					{
						Data.UnlockedLevels = LevelNumber++;
						SaveScript.Save(Data);
					}
				}
				catch(Exception ex)
				{
                    SceneManager.LoadScene("SplashScreen");
				}
                LevelComplete.enabled = true;
                StartCoroutine(DelayedContinue());
                
			}
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect (150, 10, 500, 20),"Wave: " + waveNumber.ToString() + " / "+Waves.ToString());
		GUI.Label(new Rect(150, 60, 500, 50), "Enemies: "+ ((waveNumber*EnemiesPerWave)-EnemiesPerWave+(EnemiesPerWave-GameObject.FindGameObjectsWithTag("Guard").Length)).ToString() + "/" + (EnemiesPerWave*Waves).ToString());
	}

    IEnumerator DelayedContinue()
    {
        
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartingPart");
        Destroy(this);
    }

}
