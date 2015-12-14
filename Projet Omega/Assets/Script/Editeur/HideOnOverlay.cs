using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HideOnOverlay : MonoBehaviour
{
	RawImage Image;
	bool Fade;
	float MaxAlpha;

	void Start()
	{
		Fade = false;
		Image = GetComponent<RawImage>();
		InvokeRepeating("UpdateAlpha", 0, Time.deltaTime);
		MaxAlpha = Image.color.a;
	}

	void Update()
	{
		if (!Input.GetMouseButton(0))
			Fade = false;
	}

	public void UpdateFade(bool Fade_I)
	{
		Fade = Fade_I;
	}

	void UpdateAlpha()
	{
		if (Fade && Image.color.a > 0)
			Image.color -= new Color(0, 0, 0, MaxAlpha / 10);
		else if (!Fade && Image.color.a < MaxAlpha)
			Image.color += new Color(0, 0, 0, MaxAlpha / 10);
	}
}
