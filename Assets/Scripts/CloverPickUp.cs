using UnityEngine;
using System.Collections;

public class CloverPickUp : MonoBehaviour {

	public AudioClip CollectSound;
	public bool CollectSoundplayed = false;
	
	public int CloverValue;

	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Clover") {
            GetComponent<Renderer>().material.color = Color.green;
            Destroy(col.gameObject);
            GetComponent<AudioSource>().PlayOneShot(CollectSound);
            CollectSoundplayed = true;
            Camera.main.GetComponent<CloverScript>().AddClover();
            StartCoroutine("Change_Colour");
        }
	}

    IEnumerator Change_Colour()
    {
        yield return new WaitForSeconds(.15f);
        GetComponent<Renderer>().material.color = Color.white;
    }
}
