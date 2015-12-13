using UnityEngine;
using System.Collections;

public class Ramasser : MonoBehaviour {
    Move scriptPlayer;
    // Use this for initialization
    void Start () {
        scriptPlayer = GameObject.Find("Monkey").GetComponent<Move>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Touché");
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Ramassé");
            scriptPlayer.banane = true;
            Destroy(gameObject);
        }

    }
}
