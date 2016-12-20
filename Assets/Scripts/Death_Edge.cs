using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Death_Edge : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col)
    {

      var sceneToLoad = SceneManager.GetActiveScene().buildIndex;

        if (col.gameObject.name.Equals("Player(Clone)"))
        {
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
