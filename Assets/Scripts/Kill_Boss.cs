using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Kill_Boss : MonoBehaviour {

    public AudioClip CollectSound;

    public int Health = 100;

    void Awake()
    {
        GameObject.Find("P1_Wins_Sprite").GetComponent<Renderer>().enabled = false;
        Health = 100;
    }


    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Saw") {
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
            Destroy(GameObject.Find("Boss_EO"));
            StartCoroutine("Player_Wins");
        }
    }


    IEnumerator Player_Wins()
    {
        GameObject.Find("P1_Wins_Sprite").GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Credits");
    }
}
