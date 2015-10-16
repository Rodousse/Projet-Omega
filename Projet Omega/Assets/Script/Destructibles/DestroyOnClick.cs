using UnityEngine;
using System.Collections;

public class DestroyOnClick : MonoBehaviour
{
	public bool nextToPlayer = false;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.parent.CompareTag("Player"))
			nextToPlayer = true;
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.parent.CompareTag("Player"))
			nextToPlayer = false;
	}

	void OnMouseDown()
	{
		if (nextToPlayer)
			Destroy(gameObject);
	}
}
