using UnityEngine;
using System.Collections;

public class GameParameters : MonoBehaviour {


	private static GameParameters instance;
	public static GameParameters Instance {
		// si le component n'a pas été ajouté à un objet, ajout d'un GO avec ce component
		get { return instance ?? (instance = new GameObject("GameParameters_object").AddComponent<GameParameters>()); }
		// set non fixé : read only variable !
	}


	public float player_moveForce = 45f;
	public float player_maxSpeed = 20f;
	public float player_jumpForce = 1000f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
