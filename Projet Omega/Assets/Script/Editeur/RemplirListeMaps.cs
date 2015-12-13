using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemplirListeMaps : MonoBehaviour {

	public GameObject Map_Prefab;

	RectTransform viewPort;
	GameObject ActualMap;
	string ActualFilename;
	string[] infos;
	public ArrayList imageBuffer = new ArrayList();

	//Use this for initialization
	void Start() {
		RefreshList();
	}

	public void RefreshList() {

		viewPort = gameObject.GetComponent<RectTransform>();

		foreach (Transform childTransform in transform)
			Destroy(childTransform.gameObject);
		viewPort.sizeDelta -= new Vector2(0, viewPort.sizeDelta.y);

		foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
		{
			ActualFilename = file.Substring(Application.persistentDataPath.Length + 1);

			if (ActualFilename.EndsWith(".omg"))
			{
				ActualMap = Instantiate(Map_Prefab) as GameObject;
				ActualMap.transform.SetParent(transform, false);
				ActualMap.GetComponent<RectTransform>().localPosition -= new Vector3(0, viewPort.sizeDelta.y, 0);

				infos = FileManager.GetInfos(ActualFilename);

				ActualMap.name = ActualFilename.Split('.')[0];
				//ActualMap.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>(infos[0]);

				StartCoroutine(LoadScreen(infos[1], ActualMap.GetComponentsInChildren<RawImage>()[1]));

				ActualMap.GetComponentInChildren<Text>().text = "< " + infos[1] + " >" + "\n\n" +
																"Auteur : " + infos[2] + '\n' +
																"Date : " + infos[3];


				viewPort.sizeDelta = new Vector2(viewPort.sizeDelta.x, viewPort.sizeDelta.y + 180);
			}
		}
	}

	IEnumerator LoadScreen(string FileName, RawImage Preview)
	{
		// Start a download of the given URL
		WWW www = new WWW("file://" + Application.persistentDataPath + '/' + FileName + ".png");

		// Wait for download to complete
		yield return www;
		
		Preview.texture = www.texture;
	}
}