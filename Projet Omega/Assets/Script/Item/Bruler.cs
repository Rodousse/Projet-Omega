using UnityEngine;
using UnityEngine.SceneManagement;

public class Bruler : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
