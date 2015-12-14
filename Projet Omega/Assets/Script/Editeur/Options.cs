using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Options : MonoBehaviour {

	private bool State = false;
	private bool State_Explorer = false;
	private bool State_INPUT = false;
	private Animator anim, explorer_anim, INPUT_NomMap_anim;
	public GameObject Fond;
	public BlurOptimized effect;
	private GameObject child;

	bool Blur = false;

	void Start()
	{
		anim = GetComponent<Animator>();
		
		child = transform.FindChild("Safety").gameObject;
		
		explorer_anim = transform.FindChild("Explorer").gameObject.GetComponent<Animator>();
		INPUT_NomMap_anim = transform.FindChild("INPUT_NomMap").gameObject.GetComponent<Animator>();

		InvokeRepeating("UpdateBlur", 0, Time.deltaTime);
	}

	public void Switch()
	{
		Blur = State = !State;
		child.SetActive(State);
		anim.SetBool("State", State);

		State_Explorer = false;
		explorer_anim.SetBool("State", State_Explorer);
		Cancel_Export();
	}

	public void Cancel_Export()
	{
		State_INPUT = false;
		INPUT_NomMap_anim.SetBool("State", State_INPUT);
	}

	public void Effacer()
	{
		tk2dTileMap TileMap = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();

		TileMap.BeginEditMode();

		for (int layerID = 0; layerID < TileMap.Layers.Length; layerID++)
			for (int x = 0; x < TileMap.width; x++)
				for (int y = 0; y < TileMap.height; y++)
					TileMap.SetTile(x, y, layerID, -1);

		TileMap.EndEditMode();
	}

	public void Importer()
	{
		State_Explorer = !State_Explorer;
		explorer_anim.SetBool("State", State_Explorer);
	}

	public void Exporter()
	{
		State_INPUT = !State_INPUT;
		INPUT_NomMap_anim.SetBool("State", State_INPUT);
	}

	public void ValiderExporter()
	{
		string FileName = gameObject.GetComponentInChildren<InputField>().text;

		if(FileName.Length > 0)
		{
			State_INPUT = !State_INPUT;
			INPUT_NomMap_anim.SetBool("State", State_INPUT);

			FileManager.Exporter(FileName);

			StartCoroutine(ScreenShot(FileName));
		}
	}

	void UpdateBlur()
	{
		if(Blur)
			effect.enabled = true;

		if (!Blur && effect.blurSize > 0)
			effect.blurSize -= 0.2f;
		else if (Blur && effect.blurSize < 4)
			effect.blurSize += 0.2f;

		if (effect.blurSize < 0)
			effect.enabled = false;
	}

	public IEnumerator ScreenShot(string FileName)
	{
		// Wait till the last possible moment before screen rendering to hide the UI
		yield return null;
		GameObject.Find("Editeur").GetComponent<Canvas>().enabled = false;
		GameObject.Find("Camera").GetComponent<BlurOptimized>().enabled = false;

		// Wait for screen rendering to complete
		yield return new WaitForEndOfFrame();

		// Take screenshot
		//

# if UNITY_ANDROID
	Application.CaptureScreenshot(FileName + ".png");
#endif

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
	Application.CaptureScreenshot(Application.persistentDataPath + '/' + FileName + ".png");
#endif

		// Show UI after we're done
		GameObject.Find("Editeur").GetComponent<Canvas>().enabled = true;
		GameObject.Find("Camera").GetComponent<BlurOptimized>().enabled = true;

		// On attend que l'image soit finie d'etre ecrite
		yield return new WaitForSeconds(1);

		GetComponentInChildren<RemplirListeMaps>().RefreshList();
	}
}
