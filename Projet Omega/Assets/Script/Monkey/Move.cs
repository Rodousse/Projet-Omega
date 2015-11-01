using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	public float maxSpeed;

	Collider2D groundCheck;
	BoxCheck wallCheckR;
	BoxCheck wallCheckL;
	BoxCheck wallCheckTopR;
	BoxCheck wallCheckTopL;

	Rigidbody2D rb2d;
	Animator animator;
	
	bool isGrounded;
	bool isWalled;
	bool isJumping;
	bool canJump;
	bool willJump;
	bool jump;
	bool isFacingRight;

	int sens;
	float currentSpeed;

	Vector3 destination;
	Vector2 finalDestination;

	void Start ()
	{
		groundCheck = GameObject.Find("Pied Collider").GetComponent<Collider2D>();
		wallCheckR = GameObject.Find("WallCheck R").GetComponent<BoxCheck>();
		wallCheckL = GameObject.Find("WallCheck L").GetComponent<BoxCheck>();
		wallCheckTopR = GameObject.Find("WallCheck Top R").GetComponent<BoxCheck>();
		wallCheckTopL = GameObject.Find("WallCheck Top L").GetComponent<BoxCheck>();

		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
		
		destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}

	void Update ()
	{
		if (Input.GetButtonDown("Fire1") && isGrounded) // lors d'un clique donne la direction du singe
		{
			destination.x = RoundAbout(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2.5f);

			UpdateFacing();
		}
	}

	void FixedUpdate()
	{
		UpdateSpeed();
		UpdateCheckers();

		if (jump)
		{
			if (isGrounded)
			{
				if (willJump)
				{
					isJumping = true;
                    rb2d.AddForce(Vector2.up * 20 + sens * Vector2.right * 6, ForceMode2D.Impulse);
					animator.SetTrigger("Jump");
					destination.x = RoundAbout(transform.position.x + sens * 2.5f, 2.5f);
				}
				willJump = false;
			}
			else
				jump = false;
		}
		else
		{
			if (isGrounded) // Conditions préalables pour avancer
			{
				isJumping = false;

                if (transform.position.x != destination.x && !jump)
				{
					if (isWalled)
					{
						if(canJump)
							if (destination.x > transform.position.x + 1.25f || destination.x < transform.position.x - 1.25f)
								willJump = true;

						destination.x = RoundAbout(transform.position.x, 2.5f);
					}

					destination = new Vector3(destination.x, transform.position.y, transform.position.z);

					rb2d.MovePosition(Vector2.MoveTowards(transform.position, destination, currentSpeed * Time.fixedDeltaTime));

					// On se réaligne lorqu'on arrive très près de l'objectif
					if (Mathf.Abs(transform.position.x - destination.x) < 0.01f)
					{
						transform.position = new Vector3(destination.x, transform.position.y, transform.position.z);
						if (willJump)
							jump = true;
					}
				}
			}
			else if(!isJumping)
				rb2d.velocity = new Vector2(rb2d.velocity.x * 0.9f, rb2d.velocity.y);
        }
	}

	void UpdateSpeed()
	{
		// Décélération si on arrive à 1.25 de l'objectif sinon currentSpeed = maxSpeed
		currentSpeed = Mathf.Lerp(0.1f, maxSpeed, Mathf.Abs(transform.position.x - destination.x) / 1.25f);

		animator.SetFloat("Vitesse", currentSpeed);
	}

	void UpdateCheckers()
	{
		isGrounded = groundCheck.IsTouchingLayers();

		if (isFacingRight)
		{
			isWalled = wallCheckR.value;
			canJump = !wallCheckTopR.value;
		}
		else
		{
			isWalled = wallCheckL.value;
			canJump = !wallCheckTopL.value;
		}
	}

	void UpdateFacing()
	{
		// MAJ de isFacingRight, isWalled, canJump et du flip en fonction de la direction

		if (transform.position.x <= destination.x)
		{
			isFacingRight = true;
			sens = 1;
			GetComponentInChildren<Puppet2D_GlobalControl>().flip = false;
			isWalled = wallCheckL.value;
			canJump = !wallCheckTopL.value;
		}
		else
		{
			isFacingRight = false;
			sens = -1;
			GetComponentInChildren<Puppet2D_GlobalControl>().flip = true;
			isWalled = wallCheckR.value;
			canJump = !wallCheckTopR.value;
		}
	}

	float RoundAbout(float INPUT, float x)
	{
		// Arrondi a à x près

		float offset = INPUT % x;

		if (offset <= x / 2)
			INPUT -= offset;
		else
			INPUT += (x - offset);

		return INPUT;
	}
}