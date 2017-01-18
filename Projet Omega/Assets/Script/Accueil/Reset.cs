using UnityEngine;
using UnityEditor;

public class Reset : MonoBehaviour {
    Animator anime;
    // Use this for initialization
    void Start () {
        anime = GetComponent<Animator>();
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
        PlayerPrefs.SetString("Player_Name", ""); //reset du nom (TODO : supprimer les saves)
        anime.SetTrigger("End");
        GameObject.Find("InputField").GetComponent<Animator>().SetTrigger("Start");
		FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
		GameObject.Find("InputField").GetComponent<Animator>().SetTrigger("Start");
    }
}
