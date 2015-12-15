using UnityEngine;
using System.Collections;

public class Caisse_Bois : MonoBehaviour {
	
	tk2dUIItem UI_Component;

	void Start()
	{
		UI_Component = gameObject.AddComponent<tk2dUIItem>();

		// Ajout d'un listener
		UI_Component.OnClick += ClickManager.Instance.ClickOn_Desctructible;
    }

	public void Activate()
	{
		Destroy(GetComponent<SpriteRenderer>());
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<tk2dUIItem>());

		transform.GetChild(0).gameObject.SetActive(true);

		//Destroy(gameObject);
	}
}