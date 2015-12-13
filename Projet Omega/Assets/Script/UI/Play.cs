using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void start()
    {
        Application.LoadLevel(0);
    }
    public void editeur()
    {
        Application.LoadLevel(1);
    }
}
