using UnityEngine;
using System.Collections;

public class Controleur : MonoBehaviour
{

	public tk2dTileMap TileMap;

	int CurrentPosition_X, CurrentPosition_Y;

	public Transform curseur;

	Vector3 CameraMaximums;

	void Start()
	{
		CurrentPosition_X = -1;
		CurrentPosition_Y = -1;

		CameraMaximums.x = Screen.width;
		CameraMaximums.y = Screen.height;
    }

	void Update()
	{
		CurrentPosition_X = (int)((Camera.main.ScreenToWorldPoint(Input.mousePosition).x) / (Camera.main.ScreenToWorldPoint(CameraMaximums).x) * 15);
		CurrentPosition_Y = (int)((Camera.main.ScreenToWorldPoint(Input.mousePosition).y) / (Camera.main.ScreenToWorldPoint(CameraMaximums).y) * 8);

		curseur.position = new Vector3(CurrentPosition_X * 2.5f, CurrentPosition_Y * 2.5f, curseur.position.z);

		Debug.Log("X = " + CurrentPosition_X + " , Y = " + CurrentPosition_Y);
	}

	void SelectPosition(int x, int y)
	{

	}

	public void Placer()
	{
		TileMap.BeginEditMode();
		TileMap.SetTile(CurrentPosition_X, CurrentPosition_Y, 1, 1);
		TileMap.EndEditMode();
	}
}
