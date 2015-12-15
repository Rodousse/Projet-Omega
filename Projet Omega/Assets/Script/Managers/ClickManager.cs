using UnityEngine;
using System.Collections;

public class ClickManager : Singleton<ClickManager> {

	Move scriptPlayer;
	tk2dUIItem UI_Component;
    public float minSwipeDistY = 10f;

    public float minSwipeDistX = 10f;
    private Vector2 startPos;
    bool hold;

    public static ClickManager Instance
	{
		get	{return ((ClickManager)mInstance);}
		set	{mInstance = value;}
	}

	void Start ()
	{
		scriptPlayer = FindObjectOfType<Move>();
		UI_Component = gameObject.AddComponent<tk2dUIItem>();

		// Ajout d'un listener
		UI_Component.OnUp += this.ClickOn_Background;
	}

    void Update()
    {
        hold = false;
        //#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                    if (swipeDistVertical > minSwipeDistY)
                    {
                        float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

						if (swipeValue > 0)//up swipe
						{
							scriptPlayer.Jump();
							hold = true;
							Debug.Log("Bien ouej");
						}

						//down swipe
						//else if (swipeValue < 0);
					}

					float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)
                    {
                        //float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

						//right swipe
						//if (swipeValue > 0);

						//left swipe
						//else if (swipeValue < 0);
                    }
                    else if (!(swipeDistHorizontal > minSwipeDistX) && !(swipeDistVertical > minSwipeDistY))
                        scriptPlayer.SetfinalDestination();
                    break;
            }
        }
    }

    void ClickOn_Background()
	{
        if(!hold && scriptPlayer != null)
		    scriptPlayer.SetfinalDestination();
    }

	public void ClickOn_Desctructible()
	{
        if(!hold)
		    scriptPlayer.SetfinalDestination(isDestructible:true);
	}
}
