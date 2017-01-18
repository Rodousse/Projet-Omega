using UnityEngine;

public class RestartButton : MonoBehaviour {

	UnityStandardAssets.ImageEffects.Twirl vortex;
	bool State;

	void Start()
	{
		State = false;
		vortex = FindObjectOfType<UnityStandardAssets.ImageEffects.Twirl>();
		InvokeRepeating("UpdateVortex", 0, Time.deltaTime);
	}

	public void ReloadLevel()
	{
		if (!State && vortex.angle == 0)
			State = true;
	}

	void UpdateVortex()
	{
		if (!State && vortex.angle < 360 && vortex.angle >= 180)
			vortex.angle += 5;
		else if (State && vortex.angle >= 0 && vortex.angle < 180)
			vortex.angle += 5;

		if (!State && vortex.angle == 360)
			vortex.angle = 0;

		if (State && vortex.angle == 180)
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}
}
