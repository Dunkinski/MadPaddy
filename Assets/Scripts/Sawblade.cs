using UnityEngine;
using System.Collections;

public class Sawblade : MonoBehaviour {
	
	public float speed = 300;
	public GameObject Player;
	public int damage = 100;


	void Update () {
		transform.Rotate(Vector3.forward * speed * Time.deltaTime,Space.World);
	}
	
	
	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == "Player") {
			Vector3 temp = transform.position; // copy to an auxiliary variable...

			temp.x = 7.0f; // modify the component you want in the variable...
			c.GetComponent<PlayerControlTouchNew>().RecieveDamage(damage);

			Player.transform.position = temp; // and save the modified value 

		}
	}
}
