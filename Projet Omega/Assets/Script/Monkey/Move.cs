using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	public float maxSpeed;
	public float moveForce;
	public float jumpForce;
	public float distanceArret;
	public Transform groundCheck;
	public GameObject wallCheck;

	private Rigidbody2D rb2d;
	private Animator animator;
	private Camera cam;
	public bool grounded;
	public bool move;
	public bool wall;
	private float offset;
    private int faceRight;
	private Vector2 instantSpeed;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		cam = Camera.main;
		direction.x = transform.position.x;

	}
	//
	// Update is called once per frame
	void Update () {

		if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) || (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Wall"))))
			grounded = true;

		wall = wallCheck.GetComponent<BoxCheck>().value;

		if (Input.GetButtonDown ("Fire1")) // lors d'un clique donne la direction du singe
		{ 
			direction = cam.ScreenToWorldPoint (Input.mousePosition);

			offset = direction.x % 2.5f;

			if (offset <= 1.25f)
				direction.x -= offset;
			else
				direction.x += (2.5f - offset);

			move = true;
			if (transform.position.x < direction.x)
				faceRight = -1;
			else if (transform.position.x > direction.x)
				faceRight = 1;
			
			flip();
		}

	}

	void FixedUpdate()
	{
		if (grounded)
		{
			if (move)// si il y a un mur on arrete d'avancer
			{
				if (Mathf.Abs(rb2d.velocity.x) <= maxSpeed - 1)
				{
					rb2d.AddForce(Vector2.right * faceRight * moveForce, ForceMode2D.Impulse);
					if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
						rb2d.velocity.Set(faceRight * maxSpeed, rb2d.velocity.y);
				}
				else
					rb2d.velocity.Set(faceRight * maxSpeed, rb2d.velocity.y);

				if (grounded && wall) // Sauter sur la plateforme
				{
					rb2d.AddForce(new Vector2(faceRight * 4f, 12f) - rb2d.velocity, ForceMode2D.Impulse);
					instantSpeed = rb2d.velocity;
					move = false;
				}

				if (transform.position.x > direction.x - distanceArret && transform.position.x < direction.x + distanceArret)
				{
					move = false;
					instantSpeed = rb2d.velocity;
				}
			}
			else if (Mathf.Abs(transform.position.x - direction.x) < distanceArret) // si on atteint le seuil d'arret on ralentit
			{
				if (Mathf.Abs(transform.position.x - direction.x) < 0.1f && rb2d.velocity.x > 0.1f)
				{
					rb2d.velocity = new Vector2(0, rb2d.velocity.y);
					transform.position = new Vector2(direction.x, transform.position.y);
				}
				else
					rb2d.velocity = new Vector2( 0.1f + instantSpeed.x * Mathf.Cos((Mathf.Abs(transform.position.x - direction.x) - distanceArret) / distanceArret * Mathf.PI / 2), rb2d.velocity.y);
			}
		}
	}


	void flip()
	{
		if (faceRight == 1)
			GetComponent<Puppet2D_GlobalControl>().flip = true;
		else
			GetComponent<Puppet2D_GlobalControl>().flip = false;
		
		faceRight = -faceRight;
		rb2d.AddForce(new Vector2(-rb2d.velocity.x,0), ForceMode2D.Impulse);
	}
}
