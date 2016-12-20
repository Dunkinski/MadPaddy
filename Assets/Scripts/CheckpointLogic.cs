using UnityEngine;
using System.Collections;

public class CheckpointLogic : MonoBehaviour
{

   
    public bool isChecked = false;
    public GameObject checkpointCheckedOBJ;
    public GameObject checkpointFlacidOBJ;


    // Use this for initialization
    void Start ()
    {
        isChecked = false;
        checkpointCheckedOBJ.SetActive(false);
        checkpointFlacidOBJ.SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update ()
    {

	
	}

    public void MushChecked(bool b)
    {
        if (b)
        {
            isChecked = true;
            checkpointFlacidOBJ.SetActive(false);
            checkpointCheckedOBJ.SetActive(true);


        }


    }
}
