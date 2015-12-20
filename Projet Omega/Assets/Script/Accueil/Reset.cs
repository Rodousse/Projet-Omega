using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class Reset : MonoBehaviour {
    Animator anime;
    // Use this for initialization
    void Start () {
        anime = this.GetComponent<Animator>();
        if (PlayerPrefs.GetString("Player_Name") != "")
        {
            anime.SetTrigger("Start");
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Clicked()
    {
		PlayerPrefs.SetInt("Save", 0);
		PlayerPrefs.SetString("Player_Name", ""); //reset du nom (TODO : supprimer les saves)
        anime.SetTrigger("End");
		FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
		GameObject.Find("InputField").GetComponent<Animator>().SetTrigger("Start");
    }
}
