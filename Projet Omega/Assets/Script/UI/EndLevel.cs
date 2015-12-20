using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

  
    private Move scriptPlayer;
    public int nbLevel;
    public string nextLevel;

    // Use this for initialization
    void Start () {

		Debug.Log(PlayerPrefs.GetInt("Save"));


		if (PlayerPrefs.GetInt("Save") <= nbLevel)
            PlayerPrefs.SetInt("Save", nbLevel);
        scriptPlayer = GameObject.Find("Monkey").GetComponent<Move>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            
            if (scriptPlayer.banane)
                Application.LoadLevel(nextLevel);
        }

    }
}
