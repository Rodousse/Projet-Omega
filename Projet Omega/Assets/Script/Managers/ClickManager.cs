using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClickManager : Singleton<ClickManager> {

	Move scriptPlayer;
	tk2dUIItem UI_Component;

	public static ClickManager Instance
	{
		get	{return ((ClickManager)mInstance);}
		set	{mInstance = value;}
	}

	void Start ()
	{
		scriptPlayer = GameObject.Find("Monkey").GetComponent<Move>();
		UI_Component = gameObject.AddComponent<tk2dUIItem>();

		// Ajout d'un listener
		UI_Component.OnClick += this.ClickOn_Background;
	}

	void ClickOn_Background()
	{
		scriptPlayer.SetfinalDestination();
    }

	public void ClickOn_Desctructible()
	{
		scriptPlayer.SetfinalDestination(isDestructible:true);
	}
}
