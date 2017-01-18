using UnityEngine;
using System.Collections;

public class NoAudio : MonoBehaviour {
    GameObject ambiance;
	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("Audio"))
        {
            ambiance = GameObject.FindGameObjectWithTag("Audio");
            
            Destroy(ambiance);
           
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
