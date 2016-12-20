using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
	{
		[SerializeField]
		public int UnlockedLevels;

		public GameData(int count)
		{
		 	UnlockedLevels = count;
		}
	}
