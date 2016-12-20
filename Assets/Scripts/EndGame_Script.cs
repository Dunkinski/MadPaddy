using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("endScene");
    }

    IEnumerator endScene()
    {


        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");

    }
}
