using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Essayer_Button : MonoBehaviour {

	bool State = true;
	tk2dTileMap TileMap;
	RawImage image;

	public Texture2D Play_Sprite, Pause_Sprite;
	public RawImage[] DeActivables;
	public Image[] DeActivablesImages;

	void Start () {
		TileMap = FindObjectOfType<tk2dTileMap>();
		image = GetComponent<RawImage>();

		Switch();
	}

	public void Switch()
	{
		if (State)
			Pause();
		else
			Play();
	}

	void Play()
	{
		foreach (RawImage o in DeActivables)
			o.enabled = false;
		foreach (Image o in DeActivablesImages)
			o.enabled = false;
		
		FindObjectOfType<Move>().enabled = true;
		image.texture = Pause_Sprite;
		Physics2D.gravity = new Vector2(0, -9.81f);
		State = true;
		TileMap.BeginEditMode();
		TileMap.EndEditMode();
	}

	public void Pause()
	{
		foreach (RawImage o in DeActivables)
			o.enabled = true;
		foreach (Image o in DeActivablesImages)
			o.enabled = true;

		FindObjectOfType<Move>().enabled = false;
		image.texture = Play_Sprite;
		Physics2D.gravity = new Vector2(0, 0);
		State = false;
		TileMap.BeginEditMode();
		TileMap.EndEditMode();
	}
}
