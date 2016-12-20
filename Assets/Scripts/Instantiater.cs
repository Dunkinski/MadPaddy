using UnityEngine;
using System.Collections;

public class Instantiater : MonoBehaviour {
	public GameObject obstacle;
	// Use this for initialization
	void Start () {
		InvokeRepeating("Create", 2, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void Create(){
		Instantiate(obstacle,new Vector3(7f, 0.2561473f, 0.1f),Quaternion.identity);
	}
}
