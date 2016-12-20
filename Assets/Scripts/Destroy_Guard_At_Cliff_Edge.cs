using UnityEngine;
using System.Collections;

public class Destroy_Guard_At_Cliff_Edge : MonoBehaviour {

	public AudioClip Collision_Sound;
    //public GameObject Guard;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Guard") {
            GetComponent<AudioSource>().PlayOneShot(Collision_Sound);
            Destroy(other.gameObject);
        }
	}
}
