using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour
{
	[HideInInspector]
	public GameObject target;

	void OnTriggerEnter2D(Collider2D other)
	{
		target = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		target = null;
	}
}
