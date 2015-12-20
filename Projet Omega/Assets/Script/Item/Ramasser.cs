using UnityEngine;
using System.Collections;

public class Ramasser : MonoBehaviour {
	tk2dUIItem UI_Component;
	Move scriptPlayer;

    void Start () {
        scriptPlayer = FindObjectOfType<Move>();

		UI_Component = gameObject.AddComponent<tk2dUIItem>();
		UI_Component.OnClick += ClickManager.Instance.ClickOn_Background;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            scriptPlayer.banane = true;
            Destroy(gameObject);
        }

    }
}
