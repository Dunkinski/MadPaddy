using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour 
{

	public float timeToDestroy = 2.0f;


    void OnCollisonEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            StartCoroutine(LateCall());
        }
    }



    IEnumerator LateCall()
{
        this.gameObject.GetComponent<Animator>().SetTrigger("Rumble");

        yield return new WaitForSeconds(timeToDestroy);

        this.gameObject.GetComponent<Animator>().SetTrigger("Fall");

       gameObject.SetActive(false);
        //Do Function here...
    }

}
