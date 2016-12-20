using UnityEngine;
using System.Collections;

public class PickUpPowerUp : MonoBehaviour {

	public AudioClip CollectSound;
	public int AttackPlus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "PowerUp") {
			Debug.Log("POWER UP");
			GetComponent<PlayerControlTouchNew>().addAttackPower(AttackPlus);
			GetComponent<AudioSource>().PlayOneShot(CollectSound);
			Destroy (col.gameObject);
		}
	}
}
