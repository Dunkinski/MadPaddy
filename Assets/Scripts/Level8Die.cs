using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level8Die : MonoBehaviour {

	public float speed = 300;

	void OnTriggerEnter2D()
	{
        SceneManager.LoadScene("Level8");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector3.forward * speed * Time.deltaTime,Space.World);
	}
}
