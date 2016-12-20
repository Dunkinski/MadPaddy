using UnityEngine;
using System.Collections;

public class CloverScript : MonoBehaviour {

	public int CloversInLevel;
	public int collectedClovers = 0;
	private bool showGui = false;
	private float timerToShow;
	int fontSize;
    public GameObject end_blocker;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(collectedClovers== CloversInLevel)
        {
            Destroy(end_blocker.gameObject);
        }
	}

	public void AddClover()
	{
		collectedClovers = collectedClovers + 1;
		timerToShow = Time.time + 2;
		showGui = true;
	}

	void OnGUI()
	{
		if (showGui == true && timerToShow > Time.time)
		{
			fontSize = GUI.skin.label.fontSize;
			GUI.skin.label.fontSize = 20;
			GUI.Label(new Rect (Screen.width/2-100, Screen.height/2-25, 200, 50),collectedClovers + " of " + CloversInLevel);
			GUI.skin.label.fontSize = fontSize;
		}
		if (showGui == true && timerToShow < Time.time) {
			showGui = false;		
		}
	}


}
