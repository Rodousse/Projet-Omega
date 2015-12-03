using UnityEngine;
using System.Collections;

public class Caisse_Bois : MonoBehaviour {

	Move scriptPlayer;
	tk2dUIItem UI_Component;

	void Start()
	{
		scriptPlayer = GameObject.Find("Monkey").GetComponent<Move>();
		UI_Component = gameObject.AddComponent<tk2dUIItem>();

		// Ajout d'un listener
		UI_Component.OnClick += ClickManager.Instance.ClickOn_Desctructible;
    }

	public void Activate()
	{
		Destroy(gameObject);
	}
}
