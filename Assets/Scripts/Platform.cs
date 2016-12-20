using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	void OnTriggerEnter2D( Collider2D StayOnPlatform)
	{
		StayOnPlatform.transform.parent = transform;
	}

	void OnTriggerExit2D (Collider2D StayOnPlatform)


	{
		StayOnPlatform.transform.parent = null;
	}


}
