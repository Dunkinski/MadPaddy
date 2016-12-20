using UnityEngine;
using System.Collections;

public class PotGoldKillScript : MonoBehaviour {

	float killTime;
	// Use this for initialization
	void Start () 
	{
		killTime = Time.time + 10;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(killTime <= Time.time)
		{
			Destroy(this.gameObject);
		}
	}
}
