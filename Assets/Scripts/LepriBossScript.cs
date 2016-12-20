using UnityEngine;
using System.Collections;

public class LepriBossScript : MonoBehaviour {

	public int Health = 100;
	public int Speed = 3;
	public bool lookLeft = true;
	float AttackTime = 10;
	float HitTime = 0;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Animator>().SetInteger("Health",Health);
		if(Health <= 0)
		{
			Destroy(this.gameObject);
		}
		if(Time.time > AttackTime)
		{
			this.GetComponent<Animator>().SetTrigger("Attack");
			AttackTime = Time.time + 10;
			HitTime = Time.time + 3;
		}
		else
		{
			if(GameObject.FindWithTag("Player").transform.position.x < this.gameObject.transform.position.x)
			{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Speed*-1, -5));
				if(lookLeft == false)
				{
					Vector3 theScale = transform.localScale;
					theScale.x *= -1;
					transform.localScale = theScale;
					lookLeft = true;
				}
			}
			else if(GameObject.FindWithTag("Player").transform.position.x > this.gameObject.transform.position.x)
			{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Speed, -5));
				if(lookLeft == true)
				{
					Vector3 theScale = transform.localScale;
					theScale.x *= -1;
					transform.localScale = theScale;
					lookLeft = false;
				}
			}
		}

	}

	public void RecieveDamage(int damage)
	{
		if(Time.time < HitTime)
		{
			Health = Health - damage;
			this.GetComponent<Animator>().SetTrigger("GetHit");
		}
	}
}
