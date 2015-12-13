using UnityEngine;
using System.Collections;
using UnityEditor;

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
		scriptPlayer = GameObject.Find("Monkey").GetComponent<Move>();
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


                        else if (swipeValue < 0)//down swipe
                            ;


                    }

                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)

                    {

                        float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        if (swipeValue > 0)//right swipe
                            ;


                        else if (swipeValue < 0)//left swipe
                            ;


                    }
                    else if (!(swipeDistHorizontal > minSwipeDistX) && !(swipeDistVertical > minSwipeDistY))
                        scriptPlayer.SetfinalDestination();
                    break;
            }
        }
    }

    void ClickOn_Background()
	{
        if(!hold)
		    scriptPlayer.SetfinalDestination();
    }

	public void ClickOn_Desctructible()
	{
        if(!hold)
		    scriptPlayer.SetfinalDestination(isDestructible:true);
	}
}
