using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {
	
	public int Health = 100;
	public float Speed = 3f;
	public bool lookLeft = true;
	public bool PlayerInRange = false;
	public float AttackTime = 10;
	float HitTime = 0;

	
	// Use this for initialization
	void Start () 
	{
        this.GetComponent<Animator>().SetInteger("Health", Health);
    }
	
	// Update is called once per frame
	void Update () {
		
		if(Health <= 0)
		{

			Destroy(this.gameObject);
		}
		if(Time.time > AttackTime)
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			this.GetComponent<Animator>().SetTrigger("Attack");
			if(PlayerInRange)
			{
				GameObject.FindWithTag("Player").GetComponent<PlayerControlTouchNew>().RecieveDamage(25);
			}
			AttackTime = Time.time + 10;
			HitTime = Time.time + 10;
		}
		else
		{
			if(this.gameObject.transform.position.x > 0 && lookLeft == true)
			{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Speed*-1, -5));

			}
			else if(this.gameObject.transform.position.x < 31 && lookLeft == false)
			{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Speed, -5));
				if(lookLeft == true)
				{
				}
			}
			else if(this.gameObject.transform.position.x <= 2 && lookLeft == true)
			{
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				lookLeft = false;
			}
			else if(this.gameObject.transform.position.x >= 26 && lookLeft == false)
			{
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				lookLeft = true;
			}
		}

			
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			PlayerInRange = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			PlayerInRange = false;
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
