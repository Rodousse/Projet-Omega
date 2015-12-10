using UnityEngine;
using System.Collections;

public class PlatformSpecific : MonoBehaviour {
	
	void Start () {
		//Empeche l'écran de s'éteindre
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
}
