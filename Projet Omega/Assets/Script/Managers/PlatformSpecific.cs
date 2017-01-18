using UnityEngine;
using System.Collections;

//package com.company.product;

//import com.unity3d.player.UnityPlayerActivity;

//import android.os.Bundle;
//import android.util.Log;

public class PlatformSpecific : MonoBehaviour
{
	void Start()
	{
		//Empeche l'écran de s'éteindre
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}

	public void RetourMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
