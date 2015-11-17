using UnityEngine;

public class Controleur : MonoBehaviour
{
	public tk2dTileMap TileMap;

	public GameObject PreviewBloc;

	public Transform PreviewTile;

	Vector3 MousePosition, BlocPosition;

	int TileID = 0, LayerID = 0, ToolID;

	void Start()
	{
		MousePosition.z = 0;
	}

	void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			MousePosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 1.25f;
            MousePosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

			PreviewBloc.transform.position = new Vector3(MousePosition.x - 1.25f, MousePosition.y, 0);

			MousePosition.x = MousePosition.x - MousePosition.x % 2.5f;
            MousePosition.y = MousePosition.y - MousePosition.y % 2.5f;

			PreviewTile.transform.position = new Vector3(MousePosition.x, MousePosition.y + 1.25f, 0);
			
		}
		else if(Input.GetButtonUp("Fire1"))
		{
			PreviewTile.transform.position = new Vector3(-2, 0, 0);
			PreviewBloc.transform.position = new Vector3(-2,0, 0);
			Placer();
		}
	}

	public void Placer()
	{
		TileMap.BeginEditMode();
		TileMap.SetTile((int) (MousePosition.x / 2.5f), (int) (MousePosition.y / 2.5f), LayerID, TileID);
		TileMap.EndEditMode();
	}

	public void setTool(string INPUT)
	{
		switch (INPUT)
		{
			case "Eraser":
				ToolID = 0;
				break;
			case "Pen":
				ToolID = 1;
				break;
			case "Brush":
				ToolID = 2;
				break;
			default:
				break;
		}
	}

	public void setTile(string INPUT)
	{
		print("set tile");

		switch (INPUT)
		{
			case "Box":
				TileID = 7;
				LayerID = 2;
                break;
			case "Dirt":
				TileID = 6;
				LayerID = 0;
				break;
			case "Grass":
				TileID = 0;
				LayerID = 0;
				break;
			case "IronBox":
				TileID = 3;
				LayerID = 0;
				break;
			case "IronWall1":
				TileID = 4;
				LayerID = 1;
				break;
			case "IronWall2":
				TileID = 1;
				LayerID = 1;
				break;
			case "Fire":
				TileID = 5;
				LayerID = 2;
				break;
			case "Light":
				TileID = 9;
				LayerID = 3;
				break;
			default:
				break;
		}
	}

	public void ChangePreviewSprite(Sprite INPUT)
	{
		PreviewBloc.GetComponent<SpriteRenderer>().sprite = INPUT;
    }
}
