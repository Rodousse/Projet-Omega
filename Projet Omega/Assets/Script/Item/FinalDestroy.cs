using UnityEngine;
using System.Collections;

public class FinalDestroy : MonoBehaviour {
	public void Destroy()
	{
		Destroy(transform.parent.gameObject);
	}
}
