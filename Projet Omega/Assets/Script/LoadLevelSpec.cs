using UnityEngine;
using System.Collections;

public class LoadLevelSpec : MonoBehaviour {

    public string level;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        Application.LoadLevel(level);
    }
}
