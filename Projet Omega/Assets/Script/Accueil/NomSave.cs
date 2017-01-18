using UnityEngine;
using System.Collections;

public class NomSave : MonoBehaviour {
    Animator anime;
    
	// Use this for initialization
	void Start () {
<<<<<<< HEAD
        anime = this.GetComponent<Animator>();
=======
		Debug.Log(PlayerPrefs.GetInt("Save"));

		anime = this.GetComponent<Animator>();
>>>>>>> parent of 24d010f... Correction mineures + video
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
