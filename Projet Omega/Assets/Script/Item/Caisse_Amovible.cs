using UnityEngine;
using System.Collections;

public class Caisse_Amovible : MonoBehaviour {
	
    tk2dUIItem UI_Component;
    Rigidbody2D rb2d;
    Vector2 destination;

    float currentSpeed;
    float t1, t2;
    void Start()
    {
        destination = new Vector2(transform.position.x, transform.position.y);
        rb2d = GetComponent<Rigidbody2D>();
        UI_Component = gameObject.AddComponent<tk2dUIItem>();

        // Ajout d'un listener
        UI_Component.OnClick += ClickManager.Instance.ClickOn_Desctructible;
    }

    void FixedUpdate()
    {
        if (rb2d.velocity == Vector2.zero && t2 - t1 > 1 )
            //rb2d.isKinematic = true;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX ;

        t2 = Time.deltaTime;
        if (Mathf.Abs(transform.position.x - destination.x) < 0.5f)
        {
            transform.position = new Vector3(destination.x, transform.position.y, transform.position.z);
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        }

    }

    public void Activate(int sens)
    {
        //currentSpeed = Mathf.Lerp(0.1f, 10, Mathf.Abs(transform.position.x - destination.x) / 1.25f);
        destination = new Vector2(transform.position.x + 2.5f*sens, transform.position.y);

        t1 = Time.deltaTime;
        Debug.Log("Activé");
        //rb2d.isKinematic = false
        rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb2d.MovePosition(Vector2.MoveTowards(transform.position, destination , 6f * Time.fixedDeltaTime));
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
