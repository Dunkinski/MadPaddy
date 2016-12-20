using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerControl : Entity
{
	public Transform lineStart, lineEnd, groundedEnd;
	public float  JumpForce = 200f;
	public bool Grounded = false;
	public float Speed = 4f;
	public bool Interact = false;
	public AudioClip JumpSound;
	public bool JumpsoundPlayed = false;
	public float AttackStrength = 10;
	public float AttackDelay = 2;
	public float NextAttack = 0;

	Animator anim;

	float JumpTime, JumpDelay = .5f;
	bool Jumped;



	private GameManager Manager;
//	private Transform CurrentPlatform = null;
//	private Vector3 LastPlatfromPosition = Vector3.zero;
//	private Vector3 CurrentPlatformDelta = Vector3.zero;
//	private Transform groundedEnds;


	RaycastHit2D WhatIHit;


	void Update ()
	{
		 Movement();
		Raycasting ();

	}

	void Start()
	{
		Manager = Camera.main.GetComponent<GameManager> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Movement()
	{
		anim.SetFloat ("Speed", Mathf.Abs (Input.GetAxis ("Horizontal")));
		if (Input.GetAxisRaw("Horizontal") > 0)
		    {
			transform.Translate(Vector2.right * Speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 0);
			}

		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			transform.Translate(Vector2.right * Speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 180);
		}
		if (Input.GetKeyDown(KeyCode.Space)&& Grounded == true) 
				{
			GetComponent<AudioSource>().PlayOneShot(JumpSound);
			JumpsoundPlayed = true;
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
			JumpTime = JumpDelay;
			anim.SetTrigger("Jump");
			Jumped= true;
		}
		JumpTime -= Time.deltaTime;
		if (JumpTime <= 0 && Grounded && Jumped)
				{
				anim.SetTrigger("Land");
				Jumped = false;
				}


	}
	void Raycasting()
	{
				Debug.DrawLine (lineStart.position, lineEnd.position, Color.green);
				Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);

				Grounded = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Ground")); 
	
				if (Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("Guard"))) {
						WhatIHit = Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("Guard"));
						Interact = true;
				} else {
						Interact = false;
				}
				if (Input.GetKeyDown (KeyCode.E) && Interact == true) {
						Destroy (WhatIHit.collider.gameObject);
				}

				Physics2D.IgnoreLayerCollision (8, 9);
		}





		void OnTriggerEnter2D(Collider2D c)

	{
		if (c.tag == "checkpoint") 
		{
			Manager.SetCheckpoint(c.transform.position);
		}

	//Moving platfrom logic
//	List<Transform> platforms = new List<Transform>();
//		bool onSamePlatform = false;
////		//foreach (Transform groundedEnds in groundedEnd) {
//					RaycastHit2D hit = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
//					if (hit.transform != null) {
//							platforms.Add (hit.transform);
//								if (CurrentPlatform == hit.transform) {
//										onSamePlatform = true;
//							}
//						}
//				//}
//
//		if(! onSamePlatform)
//		{
//		foreach(Transform platform in platforms)
//		
//		 
//			{
//					CurrentPlatform = platform;
//					LastPlatfromPosition = CurrentPlatform.position;
//				}
//			}
////						
//////			CurrentPlatform = hit.transform;
//////			CurrentPlatformDelta = Vector3.zero;
//////			if (CurrentPlatform != null)
//////			{
//////			LastPlatfromPosition = CurrentPlatform.position;
//////			}
//////			}
//////			}
////		
//		if (CurrentPlatform != null) {
//						CurrentPlatformDelta = CurrentPlatform.position - LastPlatfromPosition;
//
//						transform.position += CurrentPlatformDelta;
//
//			LastPlatfromPosition = CurrentPlatform.position;
//				}

	}



}
