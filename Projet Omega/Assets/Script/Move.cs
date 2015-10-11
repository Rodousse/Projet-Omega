using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public bool faceRight = true;
	public float maxSpeed = 30f;
	public float moveForce =  10f;
	public float jumpForce = 500f;
	public Transform groundCheck;
	public Transform wallCheck;
	public Transform climbCheck;
			
	private Rigidbody2D rb2d;
	private Camera cam;
	private bool grounded = false;
	private bool move = false;
	private bool climb;
	private bool jump;
	private bool wall;
	bool wallContinue = false;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		cam = Camera.main;
	
	}
	
	// Update is called once per frame
	void Update () {

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//Si il touche le sol
		wall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));
		//Si il touche un mur
		climb = Physics2D.Linecast(transform.position, climbCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//si il touche un rebord
		
		if (Input.GetButtonDown("Jump") && grounded) // Condition si il appuie sur espace et qu'il est sur le sol
		{
			jump = true;
		}


		if (Input.GetButtonDown ("Fire1")) { // lors d'un clique donne la direction du singe
						Debug.Log ("Go");
						direction = cam.ScreenToWorldPoint (Input.mousePosition);
						move = true;
				}

	}

	void FixedUpdate()
	{
		if(move)// si il y a un mur on arrete d'avancer
		{
			if( transform.position.x < direction.x )
			{
				Debug.Log("GoDroite");
				if(!faceRight)
					flip();
				if(rb2d.velocity.x < maxSpeed)
					rb2d.AddForce(new Vector2(moveForce, 0f));
				if(rb2d.velocity.x > maxSpeed)
					rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
				
			}
			else if (transform.position.x > direction.x)
			{
				Debug.Log("GoGauche");
				
				if(faceRight)
					flip();
				Debug.Log (rb2d.velocity);
				if(rb2d.velocity.x > -maxSpeed)
					rb2d.AddForce(new Vector2(-moveForce, 0f));
				if((int)rb2d.velocity.x < (int)-maxSpeed)
					rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
				
				
			}
			if((int)(transform.position.x) == (int)(direction.x))
			{
				move = false;
				rb2d.velocity = new Vector2(0,0); // On s'arrete des que le singe a atteint le lieu donné
			}
			
			
		}


		if (jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce)); // Saute
			jump = false;
		}


		if(wall)

		if (wall && !wallContinue) {
			// Grimper sur la plateforme
			rb2d.velocity = new Vector2(0,rb2d.velocity.y);
			jump = true;
			wallContinue = true;


		}

		if (!wall)
			wallContinue = false;

		if (climb) {
			rb2d.velocity = new Vector2(0,rb2d.velocity.y);
			rb2d.position = new Vector3(rb2d.position.x, rb2d.position.y + 0.5f);
		}

//		if (wall && climb) {
//			wallContinue = false;
//		}

	}


	void flip()
	{
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
