using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoixEvents : MonoBehaviour {
	
	RawImage Image;
	Controleur Editeur;
	UI_Palette Palette;

	bool Fade = false;

	void Start ()
	{
		Image = GetComponent<RawImage>();
		Editeur = GetComponentInParent<Controleur>();
		Palette = GetComponentInParent<UI_Palette>();

		InvokeRepeating("UpdateAlpha", 0, Time.deltaTime);
	}
	
	public void Begin(bool Background)
	{
		if (Background)
			GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Backgrounds/" + GetComponent<RawImage>().name);
		else
		{
			Fade = true;
			Palette.Shift();
			Editeur.MovePreview();
			Editeur.setTile(gameObject.name);
		}
			
	}

	public void Move()
	{
		Editeur.MovePreview();
	}

	public void End(bool Background)
	{
		if (!Background)
		{
			Editeur.Placer();
			Fade = false;
		}
	}

	void UpdateAlpha()
	{
		if (Fade && Image.color.a > 0)
			Image.color = new Color(1, 1, 1, Image.color.a - 0.2f);
		else if (!Fade && Image.color.a < 1)
			Image.color = new Color(1, 1, 1, Image.color.a + 0.2f);
	}
}
