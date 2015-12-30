using UnityEngine;
using System.Collections;

public class Ramasser : MonoBehaviour {
    Move scriptPlayer;
    // Use this for initialization
    void Start () {
        scriptPlayer = FindObjectOfType<Move>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            scriptPlayer.banane = true;
            Destroy(gameObject);
        }

    }
}
