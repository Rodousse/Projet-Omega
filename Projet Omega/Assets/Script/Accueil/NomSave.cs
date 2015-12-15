using UnityEngine;
using System.Collections;

public class NomSave : MonoBehaviour {
    Animator anime;
    
	// Use this for initialization
	void Start () {
        anime = this.GetComponent<Animator>();
        if (PlayerPrefs.GetString("Player_Name") == "")
            anime.SetTrigger("Start");
    }

    public void SetName(string name)
    {
        if (PlayerPrefs.GetString("Player_Name") == "")
        {
            PlayerPrefs.SetString("Player_Name", name);
            anime.SetTrigger("End");
            GameObject.Find("Button").GetComponent<Animator>().SetTrigger("Start");
            
        }
    }
}
