using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(GameObject.FindGameObjectWithTag("Audio"));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void start()
    {
        PlayerPrefs.SetInt("Save", 0);
        Application.LoadLevel("Load");
    }
    public void editeur()
    {
        Application.LoadLevel("Editeur");
    }
}
