using UnityEngine;
using System;
using System.IO;

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
		
		foreach (string file in Directory.GetFiles(Application.persistentDataPath))
		{
			File.SetAttributes(file, FileAttributes.Normal);
			File.Delete(file);
		}

		Directory.Delete(Application.persistentDataPath);

		GameObject.Find("InputField").GetComponent<Animator>().SetTrigger("Start");
    }
}
