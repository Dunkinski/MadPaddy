using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GuardLogicLevel10 : MonoBehaviour 
{
	public Transform sightStart, SightEnd;
	public bool spotted = false;
	public bool lookLeft = true;
	public GameObject throwable;
	public float attackDelay;
	private float lastAttack = 0;
	public GameObject leftStop;
	public GameObject rightStop;
	public float Health = 2;
	public GameObject Clover;
	public Vector3 Startposition;
	public float PatrolRange;
	public int damage = 10;
    public int maxSpeed = 50;
	private int curSpeed = 80;
	public AudioClip hitAudio;
    public BoxCollider2D headBox;
    public BoxCollider2D bodyBox;
   

    public bool invincible;
    public int invincibleTime = 2;

    public bool isBoss = false;
    public GameObject endGameObject;
    public bool CollisionSoundplayed;
    
 

    void Start() 
	{
		Startposition = this.gameObject.transform.position;
		InvokeRepeating ("Patrol", 0f, Random.Range (2, 6));
        curSpeed = maxSpeed;
       
        
     
        if (isBoss)
        {

            damage = 50;
            
        }
	}


    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }

       

        Raycasting();
		Behaviours();
	}




    public void RecieveDamage(float damage)
    {
        

        if (invincible == false)
        {
            curSpeed = 0;
            Health = Health - damage;
            if (Health > 0)
            {
                this.GetComponent<Animator>().SetTrigger("Gethit");
                    if (!CollisionSoundplayed)
                        {
                            GetComponent<AudioSource>().PlayOneShot(hitAudio);
                            CollisionSoundplayed = true;
                            StartCoroutine("Change_Colour", 5);
                        }
                    else
                        {
                         CollisionSoundplayed = false;
                         gameObject.GetComponent<Renderer>().material.color = Color.white;
                         gameObject.GetComponent<Renderer>().enabled = true;
                     }
            }

            else
            {
                this.GetComponent<Animator>().SetTrigger("Die");
            }
            StartCoroutine(SetInvicibility());
        
        }

        
    }

    void Raycasting()
	{
		Debug.DrawLine (sightStart.position, SightEnd.position, Color.green);
		spotted = Physics2D.Linecast (sightStart.position, SightEnd.position, 1 << LayerMask.NameToLayer ("Player"));
	}
	void Die()
	{

        if (isBoss)
        {
            Vector3 x = this.transform.position;
            GameObject c1 = (GameObject)Instantiate(Clover, x, Quaternion.identity);
            gameObject.GetComponent<Animator>().SetTrigger("Die");
            GameObject c2 = (GameObject)Instantiate(endGameObject, x, Quaternion.identity);
        }

        Destroy(gameObject);
    }
	void Behaviours()
	{
		Patrol();
        if (spotted)
        {
            
                if ((lastAttack + attackDelay) < Time.time)
                {

                    Attack();
                }
            
        
		}
			
	}
    void Attack()
    {
        curSpeed = 0;
        lastAttack = Time.time;
        //GameObject newObject = (GameObject)GameObject.Instantiate(throwable, this.transform.position, Quaternion.identity);
        /*if(lookLeft)
		{
			newObject.rigidbody2D.velocity = new Vector2(-5,6);
		}
		else
		{
			newObject.rigidbody2D.velocity = new Vector2(5,6);
		}*/
        gameObject.GetComponent<Animator>().SetTrigger("Attack");

        if (GameObject.FindWithTag("Player").GetComponent<PlayerControlTouchNew>().Grounded)
             { 
                 GameObject.FindWithTag("Player").GetComponent<PlayerControlTouchNew>().RecieveDamage(damage);
             }

        curSpeed = maxSpeed;


    }
	void Patrol()
	{
		this.GetComponent<Animator>().SetFloat("Speed", 1);
		if(GameObject.FindWithTag("Player").transform.position.x < this.gameObject.transform.position.x)
		{
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(curSpeed*-1, -5));
			if(lookLeft == false)
			{
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				lookLeft = true;
			}
		}
		else
		{
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(curSpeed, -5));
			if(lookLeft == true)
			{
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				lookLeft = false;
			}
		}
		//lookLeft = !lookLeft;
		/*
		if (lookLeft == true) {
				this.rigidbody2D.AddForce(new Vector2(-5, 0));

		}
		else
		{
				this.rigidbody2D.AddForce(new Vector2(5, 0));

		}*/
	}

    IEnumerator SetInvicibility()
    {

        invincible = true;
        Debug.Log("We are invincible - Enemy");
        yield return new WaitForSeconds(invincibleTime);
        Debug.Log("No longer invincible - Enemy");
        invincible = false;
        curSpeed = maxSpeed;
    }

    IEnumerator Change_Colour(int flashAmount)
    {
       

        
        if (isBoss)
        {

            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;

        }


        while (flashAmount > 0)
        {

            gameObject.GetComponent<Renderer>().enabled = true;

            yield return new WaitForSeconds(.15f);

            gameObject.GetComponent<Renderer>().enabled = false;


            yield return new WaitForSeconds(.15f);

            flashAmount--;
        }

        if (flashAmount <= 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            gameObject.GetComponent<Renderer>().enabled = true;

        }

    }

   

}
