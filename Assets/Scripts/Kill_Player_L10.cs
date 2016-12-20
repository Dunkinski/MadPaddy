using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Kill_Player_L10 : MonoBehaviour {

    public AudioClip CollectSound;

    public int Health = 100;

    void Awake()
    {
        GameObject.Find("Death_sprite").GetComponent<Renderer>().enabled = false;
        Health = 100;
    }


    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            Health -= 10;
            GetComponent<Renderer>().material.color = Color.green;
            GetComponent<AudioSource>().PlayOneShot(CollectSound);
            StartCoroutine("Change_Colour");
        }
    }

    IEnumerator Change_Colour()
    {
        yield return new WaitForSeconds(.15f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void Update()
    {
        if (Health <= 0)
        {
            GameObject.Find("Player(Clone)").GetComponent<Renderer>().enabled = false;
            StartCoroutine("Boss_Wins");
        }
    }


    IEnumerator Boss_Wins()
    {
        GameObject.Find("Death_sprite").GetComponent<Renderer>().enabled = true;
yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Level10");
    }
}
