using UnityEngine;
using UnityEngine.Advertisements;

public class AdScript : MonoBehaviour
{
	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}