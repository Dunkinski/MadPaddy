using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GuardLogicBoss : MonoBehaviour 
{
	public Transform sightStart, SightEnd;
	public bool spotted = false;
	public bool lookLeft = true;
	public GameObject throwable;
	public float attackDelay;
	private float lastAttack = 0;
	public GameObject leftStop;
	public GameObject rightStop;
	public float Health = 100;
	public GameObject Clover;
	public Vector3 Startposition;
	public float PatrolRange;
	public int damage = 10;
	public int speed = 50;
	public bool chase = false;
	public AudioClip GettingHit;
	public string CreditsLoad = "";
	
	public void RecieveDamage(float damage)
	{
		Health = Health - damage;
		if(Health > 0)
		{
			this.GetComponent<Animator>().SetTrigger("GetHit");
			GetComponent<AudioSource>().PlayOneShot(GettingHit);
		}
		else
		{
			this.GetComponent<Animator>().SetTrigger("Die");
		}
	}
	void Start() 
	{
		Startposition = this.gameObject.transform.position;
		//InvokeRepeating ("Patrol", 0f, Random.Range (2, 6));
		speed = 80;
	}
	
	
	void Update()
	{
		if(Health <= 0)
		{
			Die();
		}
		SpotThePlayer ();
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if((lastAttack+attackDelay)<Time.time)
			{
				Attack();
			}
		}
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if((lastAttack+attackDelay)<Time.time)
			{
				Attack();
			}
		}
	}


	void SpotThePlayer(){
		if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, this.transform.position) < 15) {
			Patrol();
			//this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-2));
			Debug.Log("Walking to player");
		}
	}

	public void Die()
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

        SceneManager.LoadScene("Credits");
	}

	void Attack()
	{
		lastAttack = Time.time;

		this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
		GameObject.FindWithTag("Player").GetComponent<PlayerControlTouchNew>().RecieveDamage(damage);
	}
	void Patrol()
	{
				this.GetComponent<Animator> ().SetFloat ("Speed", 1);
				if (GameObject.FindWithTag ("Player").transform.position.x < this.gameObject.transform.position.x) {
						this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (speed * -1, -5));
						if (lookLeft == true) {
								Vector3 theScale = transform.localScale;
								theScale.x *= -1;
								transform.localScale = theScale;
								lookLeft = false;
						}
				} else if (GameObject.FindWithTag ("Player").transform.position.x > this.gameObject.transform.position.x) {
						this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (speed, -5));
						if (lookLeft == false) {
								Vector3 theScale = transform.localScale;
								theScale.x *= -1;
								transform.localScale = theScale;
								lookLeft = true;
						}
				}
		}
	
}
