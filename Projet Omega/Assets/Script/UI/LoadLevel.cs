using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {
    private int save;
    public GameObject level;
    RectTransform viewPort;
    // Use this for initialization
    void Start () {
		save = PlayerPrefs.GetInt("Save");
		viewPort = GetComponent<RectTransform>();

		Debug.Log(save);

		for (int i = 0; i <= save;  i++)
        {
			GameObject actualLevel = Instantiate(level) as GameObject;
			actualLevel.transform.SetParent(transform, false);
			actualLevel.GetComponent<RectTransform>().localPosition -= new Vector3(0, viewPort.sizeDelta.y, 0);
			
            actualLevel.GetComponent<LoadLevelSpec>().level = "level" + i.ToString();
            actualLevel.GetComponentInChildren<Text>().text = "Level " + i.ToString();

            viewPort.sizeDelta = new Vector2(viewPort.sizeDelta.x, viewPort.sizeDelta.y + 250);
		}

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
