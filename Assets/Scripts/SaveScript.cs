using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour {

	public static void Save(GameData data)
	{
		Debug.Log("Start Saving");
		try
		{
			if(File.Exists(Application.persistentDataPath + "/test.dat"))
			{
				File.Delete(Application.persistentDataPath + "/test.dat");
			}
		BinaryFormatter bf = new BinaryFormatter();
		using(FileStream fs = File.Create(Application.persistentDataPath + "/test.dat"))
			{
			bf.Serialize(fs, data);
			fs.Close();
			}
		}
		catch(Exception e)
		{
			File.WriteAllText(Application.persistentDataPath+"/SaveError.txt", e.Message);
            SceneManager.LoadScene("SplashScreen");
		}
		Debug.Log ("End Saving");
	}
	
	public static GameData Load()
	{	
		Debug.Log("Start Loading");
		if(File.Exists(Application.persistentDataPath + "/test.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(Application.persistentDataPath + "/test.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize(fs);
			return data;
		}
		else
		{
			try
			{
			BinaryFormatter bf = new BinaryFormatter();
			using(FileStream fs = File.Create(Application.persistentDataPath + "/test.dat"))
				{
				bf.Serialize(fs, new GameData(1));
				fs.Close();
				}
			return new GameData(1);
			}
			catch(Exception e)
			{
				File.WriteAllText(Application.persistentDataPath+"/LoadWriteError.txt", e.Message);
				SceneManager.LoadScene("SplashScreen");
				return null;
			}
		}
	}
}

