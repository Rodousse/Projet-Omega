using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    public AudioClip[] bruits;
    public AudioSource Source;
    public float maxSpeed;
	public LayerMask LayersConcerne;
    public bool banane = false;

	Collider2D groundCheck;
	Collider2D wallCheckR;
	Collider2D wallCheckL;
	Collider2D wallCheckTopR;
	Collider2D wallCheckTopL;

	Rigidbody2D rb2d;
	Animator animator;
	
	bool isGrounded;
	bool isWalled;
	bool isJumping;
	bool canJump;
	bool willJump;
	bool jump;
	bool isFacingRight;
	bool willActivateInteractible;
    bool recept = false;

    int sens;
	float currentSpeed;

	Vector3 destination;
	Vector3 finalDestination;

	void Start ()
	{
		groundCheck = GameObject.Find("Pied Collider").GetComponent<Collider2D>();
		wallCheckR = GameObject.Find("WallCheck R").GetComponent<Collider2D>();
		wallCheckL = GameObject.Find("WallCheck L").GetComponent<Collider2D>();
		wallCheckTopR = GameObject.Find("WallCheck Top R").GetComponent<Collider2D>();
		wallCheckTopL = GameObject.Find("WallCheck Top L").GetComponent<Collider2D>();

		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();

		// La position sur laquelle le joueur ne cessera de se deplacer,
		// qu'on initialise comme etant la position initiale
		// du joueur pour ne pas le faire se deplacer
		finalDestination = destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}

	public void SetfinalDestination(bool isDestructible = false)
	{
        Source.clip = bruits[0];
        Source.Play();
        willActivateInteractible = false;

		if (isGrounded)
		{
			finalDestination.x = destination.x = RoundAbout(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2.5f);
			UpdateFacing();

			if (isDestructible)
			{
				willActivateInteractible = true;

				if (isFacingRight)
					finalDestination.x -= 2.5f;
				else
					finalDestination.x += 2.5f;
			}

			destination.x = finalDestination.x;
		}
	}

    void Update()
    {
        UpdateSpeed();
        UpdateCheckers();
    }

    void FixedUpdate()
	{

        //UpdateSpeed();
        //UpdateCheckers();
        reception();
		if (jump) // Si on veux faire sauter le personnage
		{
			if (isGrounded) // et qu'il est sur le sol
			{
				if (willJump) // et qu'il est en face d'un mur
				{
					//Alors il saute 
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

				if (transform.position.x != destination.x && !jump) // tant que nous ne sommes pas arrivé à destination
				{
					if (isWalled)  // si on rencontre un mur
					{
						if (canJump) // et si il n'y a pas de caisse au dessus
						{
							if (destination.x > transform.position.x + 1.25f || destination.x < transform.position.x - 1.25f)
								willJump = true; // Si la destination est au prochain "cube" de distance On fait sauter le mur
						}
						else // si il y a un mur on arrete d'avancer
							finalDestination = new Vector3(RoundAbout(transform.position.x, 2.5f), RoundAbout(transform.position.y, 2.5f), finalDestination.z) ;

						// et on recentre sa position
						destination.x = RoundAbout(transform.position.x, 2.5f);
					}

					destination = new Vector3(destination.x, transform.position.y, transform.position.z);
					//On deplace le personnage
					rb2d.MovePosition(Vector2.MoveTowards(transform.position, destination, currentSpeed * Time.fixedDeltaTime));

					// On se réaligne lorqu'on arrive très près de l'objectif
					if (Mathf.Abs(transform.position.x - destination.x) < 0.01f)
					{
						transform.position = new Vector3(destination.x, transform.position.y, transform.position.z);
						if (willJump) // Sauter sur la plateforme
							jump = true;
                        if (!willActivateInteractible)
                            if (Source.clip == bruits[0])
                                Source.Stop();// Source.Stop();
                    }
				}
			}
			else if (!isJumping) // Fall
			{
				rb2d.velocity = new Vector2(rb2d.velocity.x * 0.9f, rb2d.velocity.y);
            }
        }

		if (transform.position.x != destination.x) // Si on a changé la destinaion poour aller sur le prochain cube,
			destination = finalDestination;
		if(willActivateInteractible && transform.position.x == finalDestination.x) // Sinon si on doit activer le cube et que l'on est à la finaldestination
		{
			willActivateInteractible = false;

			animator.SetTrigger("Punch");

			if (isFacingRight)
			{
                if (wallCheckR.GetComponent<Detector>().target.GetComponent<Caisse_Bois>())
                    wallCheckR.GetComponent<Detector>().target.GetComponent<Caisse_Bois>().Activate();
                if (wallCheckR.GetComponent<Detector>().target.GetComponent<Caisse_Amovible>())
                    wallCheckR.GetComponent<Detector>().target.GetComponent<Caisse_Amovible>().Activate(sens);
            }
			else
			{
                if (wallCheckL.GetComponent<Detector>().target.GetComponent<Caisse_Bois>())
                    wallCheckL.GetComponent<Detector>().target.GetComponent<Caisse_Bois>().Activate();
                if (wallCheckL.GetComponent<Detector>().target.GetComponent<Caisse_Amovible>())
                    wallCheckL.GetComponent<Detector>().target.GetComponent<Caisse_Amovible>().Activate(sens);
            }
            Source.Stop();
            Source.clip = bruits[1];
            Source.Play();
        }

		//On la reinitialise pour poursuivre 
	}

	void UpdateSpeed()
	{
		// Décélération si on arrive à 1.25 de l'objectif sinon currentSpeed = maxSpeed
		currentSpeed = Mathf.Lerp(0.1f, maxSpeed, Mathf.Abs(transform.position.x - destination.x) / 1.25f);

		animator.SetFloat("Vitesse", currentSpeed);
	}

	void UpdateCheckers()  // Mise à jour des boolens pour les checkers
	{
		isGrounded = groundCheck.IsTouchingLayers(LayerMask.GetMask("TileMap"));
        animator.SetBool("Grounded", isGrounded);

		Debug.Log("Maj check");

		if (isFacingRight)
		{
			isWalled = wallCheckR.IsTouchingLayers(LayerMask.GetMask("TileMap"));
            canJump = !wallCheckTopR.IsTouchingLayers(LayerMask.GetMask("TileMap"));

        }
        else
		{
			isWalled = wallCheckL.IsTouchingLayers(LayerMask.GetMask("TileMap"));
            canJump = !wallCheckTopL.IsTouchingLayers(LayerMask.GetMask("TileMap"));

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
			isWalled = wallCheckR.IsTouchingLayers(LayerMask.GetMask("TileMap"));
            canJump = !wallCheckTopR.IsTouchingLayers(LayerMask.GetMask("TileMap"));
        }
        else
		{
			isFacingRight = false;
			sens = -1;
			GetComponentInChildren<Puppet2D_GlobalControl>().flip = true;
			isWalled = wallCheckL.IsTouchingLayers(LayerMask.GetMask("TileMap"));
            canJump = !wallCheckTopL.IsTouchingLayers(LayerMask.GetMask("TileMap"));
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

    public void Jump()
    {

        jump = true;
        willJump = true;
        isJumping = true;
        recept = true;
        Vector2 velocity = new Vector2(Vector2.right.x * rb2d.velocity.x, Vector2.up.y);
        rb2d.AddForce(velocity, ForceMode2D.Impulse);
        animator.SetTrigger("Jump"); //faire un saut vers l'avant

    }

    void reception()
    {

        if (!groundCheck)
            recept = true;
        if (groundCheck && recept && !isJumping)
        {
            finalDestination = transform.position;
            recept = false;
        }
    }
}