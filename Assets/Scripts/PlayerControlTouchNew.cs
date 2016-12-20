using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControlTouchNew : Entity
{
    public float JumpForce = 300f;
    public bool Grounded = false;
    public Transform lineStart, lineEnd, groundedEnd, groundedLeft, groundedRight;
    public float Speed = 4f;
    public bool Interact = false;
    public float AttackStrength = 10;
    public float AttackDelay = 2;
    public float NextAttack = 0;
    public float PotDamage = 10;
    public AudioClip ElectroSound;
    public AudioClip HitSound;
    public bool JumpPressed = false;
    public bool AttackPressed = false;
    private float startCloudTime;

    public GameObject healthMasterGUI;
    public Texture fullHealthGUI;
    public Texture lowHealthGUI;


    //public AudioClip JumpSound;
    //private bool JumpsoundPlayed = false;

    Animator anim;

    float JumpTime, JumpDelay = .5f;
    public bool Jumped;
    bool Walked;


    private GUITexture LeftButton;
    private GUITexture RightButton;
    private GUITexture JumpButton;
    private GUITexture AttackButton;

    private GameMangerTouch Manager;

    RaycastHit2D WhatIHit;
    
    public bool invincible;
    public int invincibleTime = 2;
    public AudioClip Collision_Sound;
    public bool CollisionSoundplayed = false;

    void Start()
    {
        Manager = Camera.main.GetComponent<GameMangerTouch>();
        anim = GetComponentInChildren<Animator>();
        healthMasterGUI = GameObject.Find("Health_MASTER");
        healthMasterGUI.GetComponent<GUITexture>().texture = fullHealthGUI;

    }




    void Awake()
    {
        LeftButton = GameObject.Find("Left").GetComponent<GUITexture>();
        RightButton = GameObject.Find("Right").GetComponent<GUITexture>();
        JumpButton = GameObject.Find("Jump").GetComponent<GUITexture>();
        AttackButton = GameObject.Find("Attack").GetComponent<GUITexture>();
    }

    void Update()
    {

       
        if (health <= 0)
        {
            Manager.respawn();
        }
        Movement();
        Raycasting();

    }





    void Movement()
    {
        int inputCount = 0;
        anim.SetBool("Jump", !Grounded);

        foreach (Touch touch in Input.touches)
        {

            //Debug.Log(touch.position);
            inputCount = inputCount + 1;
            /*Testing new Controlls
			if(touch.position.x > Screen.width/2)
			{
				transform.Translate (Vector2.right * Speed * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, 0);
				anim.SetFloat("Speed", 1);

			}
			else if(touch.position.x < Screen.width/2)
			{
				transform.Translate( Vector2.right * Speed * Time.deltaTime);
				transform.eulerAngles = new Vector2(0, 180);
				anim.SetFloat("Speed", 1);

			}
			if(inputCount == 2)
			{
				if ( Grounded == true)
					
					//audio.PlayOneShot(JumpSound);
					//JumpsoundPlayed = true;
					rigidbody2D.AddForce(Vector2.up * JumpForce);
				JumpTime = JumpDelay;
				anim.SetTrigger("Jump");
				Jumped= true;
				
			}
			JumpTime -= Time.deltaTime;
			if (JumpTime <= 0 && Grounded && Jumped) {
				anim.SetTrigger ("Land");
				Jumped = false;
			}*/

            //Functional Animations
            //if (touch.phase == TouchPhase.Stationary  && RightButton.HitTest  (touch.position)) 
            if (RightButton.HitTest(touch.position))
            {
                transform.Translate(Vector2.right * Speed * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 0);
                anim.SetFloat("Speed", 1);
                //anim.SetTrigger("Walking");
                //Walked = true;
            }
            //if (touch.phase == TouchPhase.Stationary && LeftButton.HitTest (touch.position))
            if (LeftButton.HitTest(touch.position))
            {
                transform.Translate(Vector2.right * Speed * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 180);
                anim.SetFloat("Speed", 1);
                //anim.SetTrigger("Walking");
                //Walked = true;
            }
            if (touch.phase == TouchPhase.Began && JumpButton.HitTest(touch.position))
            {
                JumpPressed = true;
                if (Grounded == true)
                {
                    //audio.PlayOneShot(JumpSound);
                    //JumpsoundPlayed = true;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
                    JumpTime = JumpDelay;
                    anim.SetBool("Jump", true);
                    Jumped = true;
                }
            }

            if (!JumpButton.HitTest(touch.position))
            {
                JumpPressed = false;
            }

            JumpTime -= Time.deltaTime;
            /*if (JumpTime <= 0 && Grounded && Jumped) {
                anim.SetTrigger ("Land");
                Jumped = false;
            }*/
        }
        //Debug.Log(inputCount);
        if (inputCount == 0)
        {
            anim.SetFloat("Speed", 0);
        }
    }

    void Raycasting()
    {
        Debug.DrawLine(lineStart.position, lineEnd.position, Color.green);
        Debug.DrawLine(this.transform.position, groundedEnd.position, Color.green);
        Debug.DrawLine(this.transform.position, groundedLeft.position, Color.green);
        Debug.DrawLine(this.transform.position, groundedRight.position, Color.green);

        if (Physics2D.Linecast(this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer("Ground")) ||
               Physics2D.Linecast(this.transform.position, groundedLeft.position, 1 << LayerMask.NameToLayer("Ground")) ||
             Physics2D.Linecast(this.transform.position, groundedRight.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }


        if (Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Guard")))
        {
            WhatIHit = Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Guard"));
            Interact = true;
        }
        else
        {
            Interact = false;
        }
        foreach (Touch touch in Input.touches)
            if (touch.phase == TouchPhase.Began && AttackButton.HitTest(touch.position))
            {
                if (Grounded == true)
                {

                    if (NextAttack < Time.time)
                    {
                        this.gameObject.GetComponent<Animator>().SetTrigger("HurlAttack");
                        GetComponent<AudioSource>().PlayOneShot(HitSound);
                        NextAttack = Time.time + AttackDelay;
                        if (SceneManager.GetActiveScene().Equals("Level8"))
                        {
                            WhatIHit.collider.gameObject.GetComponent<GuardLogicLevel10>().RecieveDamage(AttackStrength);
                        }
                        if (SceneManager.GetActiveScene().Equals("Level10"))
                        {
                            WhatIHit.collider.gameObject.GetComponent<BossScript>().RecieveDamage(25);
                        }
                        else
                        {
                            WhatIHit.collider.gameObject.GetComponent<GuardLogicNormal>().RecieveDamage(AttackStrength);
                        }

                    }
                }
            }
        Physics2D.IgnoreLayerCollision(8, 9);
    }



    void OnTriggerEnter2D(Collider2D c)

    {
        var tempObj = c;


        BroadcastMessage("MushChecked", SendMessageOptions.DontRequireReceiver);
        if (tempObj.gameObject.tag.Equals("checkpoint"))
            {
                Manager.SetCheckpoint(tempObj.transform.position);
               


        }
        if (tempObj.gameObject.name.Equals("PotOfGoldStandIn(Clone)"))
            {
                RecieveDamage(10);
                Destroy(tempObj.gameObject);
            }

            if (tempObj.gameObject.tag.Equals("Cloud"))
            {
                startCloudTime = Time.time;
            }

            if (tempObj == gameObject.GetComponent<GuardLogicLevel10>().headBox)
            {

                tempObj.gameObject.GetComponent<GuardLogicLevel10>().RecieveDamage(25);
               // GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
               // GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                return;
            }
       

        if (!invincible)
        {

            if (c == c.gameObject.GetComponent<GuardLogicLevel10>().bodyBox)
            {
                RecieveDamage(10);
                
            }


        }

        

    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (startCloudTime + 2 <= Time.time)
        {
            if (c.gameObject.tag.Equals("Cloud"))
            {
                anim.SetTrigger("Electro");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ElectroSound);
                RecieveDamage(25);

            }
            startCloudTime = Time.time;
        }
    }




    public void RecieveDamage(int damage)
    {

        damage = 1;

        if (health <= 2)
        {
            healthMasterGUI.GetComponent<GUITexture>().texture = lowHealthGUI;

        }
        else if (health >= 2)
        {
            healthMasterGUI.GetComponent<GUITexture>().texture = fullHealthGUI;
        }
        else
        {
            healthMasterGUI.GetComponent<GUITexture>().texture = null;

        }


        if (invincible == false)
        {
            
            health = health - damage;
            Debug.Log("Took Damage");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
            GetComponent<Rigidbody2D>().AddForce(transform.right * 50);
            transform.eulerAngles = new Vector2(0, 0);
            if (!CollisionSoundplayed)
            {
                GetComponent<AudioSource>().PlayOneShot(Collision_Sound);
                CollisionSoundplayed = true;

            }
            StartCoroutine("Change_Colour", 5);
            StartCoroutine(SetInvicibility());
        }

       

    }

    public void addAttackPower(int powerUp)
    {
        AttackStrength = AttackStrength + powerUp;
    }

    public void addHealth(int healAmount)
    {
        health = health + healAmount;
    }




    IEnumerator SetInvicibility()
    {

        invincible = true;
        Debug.Log("We are invincible");
        yield return new WaitForSeconds(2);
        Debug.Log("No longer invincible");
        CollisionSoundplayed = false;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        gameObject.GetComponent<Renderer>().enabled = true;
        invincible = false;
       
    }

    IEnumerator Change_Colour(int flashAmount)
    {

        gameObject.GetComponent<Renderer>().material.color = Color.red;


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