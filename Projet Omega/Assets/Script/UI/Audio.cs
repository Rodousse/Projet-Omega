using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
    GameObject ambiance;
    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("Audio"))
        {
            ambiance = GameObject.FindGameObjectWithTag("Audio");
            if (ambiance != gameObject)
                Destroy(gameObject);
            else
                gameObject.tag = "Audio";
        }
        else
            gameObject.tag = "Audio";
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
