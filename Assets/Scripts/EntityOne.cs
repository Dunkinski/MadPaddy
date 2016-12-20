using UnityEngine;
using System.Collections;

public class EntityOne : MonoBehaviour {
	//public string LevelString = " ";
	public float health;
	//public GameObject ragdoll;

	
	public void TakeDamage(float dmg) {
		health -= dmg;
		
		if (health <= 0) {
			Die();	
		}
	}
	
	public void Die() {

		Destroy(this.gameObject);
		//Application.LoadLevel("Level7");
	}
}
