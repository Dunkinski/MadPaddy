using UnityEngine;
using System.Collections;

public class PickUpHealthPack : MonoBehaviour {

	public AudioClip CollectSound;
	public int healthAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "HealthPack") {
			GetComponent<PlayerControlTouchNew>().addHealth(healthAmount);
			GetComponent<AudioSource>().PlayOneShot(CollectSound);
			Destroy (col.gameObject);
		}
	}
}
