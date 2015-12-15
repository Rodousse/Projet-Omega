using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

  
    private Move scriptPlayer;
    public string nextLevel;

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
            if (scriptPlayer.banane)
                Application.LoadLevel(nextLevel);

    }
}
