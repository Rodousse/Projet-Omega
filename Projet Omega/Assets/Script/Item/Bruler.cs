using UnityEngine;
using System.Collections;

public class Bruler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Touché");
        if (coll.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }
}
