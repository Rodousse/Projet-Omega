using UnityEngine;
using System.Collections;

public class BoxCheck : MonoBehaviour
{
	public LayerMask LayersConcerne;
	public bool value;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (((1 << other.gameObject.layer) & LayersConcerne.value) != 0)
			value = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		value = false;
    }
}
