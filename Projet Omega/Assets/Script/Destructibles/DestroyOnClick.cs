using UnityEngine;
using System.Collections;

public class DestroyOnClick : MonoBehaviour
{
	public bool nextToPlayer = false;
	private BoxCheck Checker;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.parent.CompareTag("Player"))
		{
			nextToPlayer = true;
			Checker = other.GetComponent<BoxCheck>();
        }
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

	void OnDestroy()
	{
		if(Checker)
			Checker.value = false;
    }
}
