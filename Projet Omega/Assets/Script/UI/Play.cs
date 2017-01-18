using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Load");
    }
    public void editeur()
    {
		SceneManager.LoadScene("Editeur");
    }
}
