using UnityEngine;
using System.Collections;

public class GuardLogic : MonoBehaviour 
{
	public Transform sightStart, SightEnd;
	public bool spotted = false;
	public bool lookLeft = true;
	public GameObject throwable;
	public float throwDelay;
	private float lastThrow = 0;
	public GameObject leftStop;
	public GameObject rightStop;
	public float Health = 100;
	public GameObject Clover;
	public Vector3 Startposition;
	public float PatrolRange;

	public void RecieveDamage(float damage)
	{
		Health = Health - damage;
	}
	void Start() 
	{
		Startposition = this.gameObject.transform.position;
		InvokeRepeating ("Patrol", 0f, Random.Range (2, 6));
	}


void Update()
{
		if(Health <= 0)
		{
			Die();
		}
	Raycasting();
	Behaviours();
}

void Raycasting()
{
		Debug.DrawLine (sightStart.position, SightEnd.position, Color.green);
		spotted = Physics2D.Linecast (sightStart.position, SightEnd.position, 1 << LayerMask.NameToLayer ("Player"));
}
	void Die()
	{
		Vector3 x = this.transform.position;
		GameObject c1 = (GameObject)Instantiate(Clover, x, Quaternion.identity);
		c1.GetComponent<Rigidbody2D>().AddForce(new Vector2(50,500));
		GameObject c2 = (GameObject)Instantiate(Clover, x, Quaternion.identity);
		c2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50,500));
		GameObject c3 = (GameObject)Instantiate(Clover, x, Quaternion.identity);
		c3.GetComponent<Rigidbody2D>().AddForce(new Vector2(100,500));
		GameObject c4 = (GameObject)Instantiate(Clover, x, Quaternion.identity);
		c4.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100,500));

		Destroy(this.gameObject);
	}
void Behaviours()
{
		if(spotted)
		{
			if((lastThrow+throwDelay)<Time.time)
			{
				Throw();
			}
		}
		else
		{
			Patrol();
		}
}
	void Throw()
	{
		lastThrow = Time.time;
		GameObject newObject = (GameObject)GameObject.Instantiate(throwable, this.transform.position, Quaternion.identity);
		if(lookLeft)
		{
			newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5,6);
		}
		else
		{
			//newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5,6);
		}
	}
	void Patrol()
	{
		//lookLeft = !lookLeft;
		if (lookLeft == true) {
			if(this.transform.position.x <(Startposition.x-PatrolRange))
			{
				lookLeft = false;
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
			else
			{
				transform.eulerAngles = new Vector2 (0, 0);
				//this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 0));
			}
				}
		else
		{
			if(this.transform.position.x > (Startposition.x+PatrolRange))
			{
				lookLeft = true;
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
			else
			{
				transform.eulerAngles = new Vector2(0, 180);
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 0));
			}
		}
	}

}
