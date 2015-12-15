using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Caisse_Bois>() || other.gameObject.GetComponent<Caisse_Amovible>())
            target = other.gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Caisse_Bois>() || other.gameObject.GetComponent<Caisse_Amovible>())
            target = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Caisse_Bois>() || other.gameObject.GetComponent<Caisse_Amovible>())
            target = null;
    }
}