using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoixEvents : MonoBehaviour {
	
	RawImage Image;
	Controleur Editeur;
	UI_Palette Palette;

	void Start () {
		Image = GetComponent<RawImage>();
		Editeur = GetComponentInParent<Controleur>();
		Palette = GetComponentInParent<UI_Palette>();

		EventTrigger thisEventTrigger = GetComponent<EventTrigger>();
	}
	
	public void Begin()
	{
		Image.enabled = false;
		Palette.Shift();
        Editeur.setTile(gameObject.name);
    }

	public void Move()
	{
		Editeur.MovePreview();
	}

	public void End()
	{
		Image.enabled = true;
		Editeur.Placer();
	}
}
