using UnityEngine;
using System.Collections;

public class UI_Palette : MonoBehaviour {

	private Animator anim;
	public RectTransform Button;

	private bool State = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shift()
	{
		State = !State;
        anim.SetBool("State", State);
    }
}
