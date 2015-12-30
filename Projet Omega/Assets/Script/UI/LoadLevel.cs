using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {
    private int save;
    public GameObject level;
    RectTransform viewPort;
    // Use this for initialization
    void Start () {
        save = 8;// PlayerPrefs.GetInt("Save");
        
        GameObject actualLevel;
        for (int i = 0; i <= save;  i++)
        {
            actualLevel = Instantiate(level) as GameObject;
            actualLevel.transform.SetParent(transform);
            Debug.Log(i);
            actualLevel.GetComponent<LoadLevelSpec>().level = "level" + i.ToString();
            actualLevel.GetComponentInChildren<Text>().text = "Level " + i.ToString();
            Vector2 translate = new Vector2(0, -actualLevel.GetComponent<RectTransform>().sizeDelta.y * i);
            actualLevel.GetComponent<RectTransform>().transform.Translate(translate);
            viewPort = gameObject.GetComponent<RectTransform>();
            viewPort.sizeDelta += new Vector2(0, actualLevel.GetComponent<RectTransform>().sizeDelta.y);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
