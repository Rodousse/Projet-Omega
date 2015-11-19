using UnityEngine;

[System.Serializable]
public class SpriteCol : System.Object
{
	public Sprite Box, Cage, Dirt, Fire, Grass, IronBox, IronWall_Bright, IronWall_Dark, Light;
}

public class Controleur : MonoBehaviour
{
	public tk2dTileMap TileMap;

	public GameObject PreviewBloc;

	public Transform PreviewTile;

	public SpriteCol Previews;

	SpriteRenderer PreviewBlocSprite;

	Vector3 MousePosition, BlocPosition;

	int TileID = 0, LayerID = 0, ToolID = 1;

	void Start()
	{
		MousePosition.z = 0;
		PreviewBlocSprite = PreviewBloc.GetComponent<SpriteRenderer>();
	}

	public void MovePreview()
	{
		MousePosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 1.25f;
		MousePosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		PreviewBloc.transform.position = new Vector3(MousePosition.x - 1.25f, MousePosition.y, 0);

		MousePosition.x = MousePosition.x - MousePosition.x % 2.5f;
		MousePosition.y = MousePosition.y - MousePosition.y % 2.5f;

		PreviewTile.transform.position = new Vector3(MousePosition.x, MousePosition.y + 1.25f, 0);
	}

	public void Placer()
	{
		PreviewTile.transform.position = new Vector3(-2, 0, 0);
		PreviewBloc.transform.position = new Vector3(-2, 0, 0);

		switch (ToolID)
		{
			case 0: // Eraser
				TileMap.BeginEditMode();
				TileMap.SetTile((int)(MousePosition.x / 2.5f), (int)(MousePosition.y / 2.5f), LayerID, -1);
				TileMap.EndEditMode();
				break;
			case 1:
				TileMap.BeginEditMode();
				TileMap.SetTile((int)(MousePosition.x / 2.5f), (int)(MousePosition.y / 2.5f), LayerID, TileID);
				TileMap.EndEditMode();
				break;
			case 2:
				break;
			default:
				break;
		}
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
		switch (INPUT)
		{
			case "Box":
				TileID = 7;
				PreviewBlocSprite.sprite = Previews.Box;
				LayerID = 2;
				break;
			case "Dirt":
				TileID = 6;
				PreviewBlocSprite.sprite = Previews.Dirt;
				LayerID = 0;
				break;
			case "Grass":
				TileID = 0;
				PreviewBlocSprite.sprite = Previews.Grass;
				LayerID = 0;
				break;
			case "IronBox":
				TileID = 3;
				PreviewBlocSprite.sprite = Previews.IronBox;
				LayerID = 0;
				break;
			case "IronWall_Bright":
				TileID = 4;
				PreviewBlocSprite.sprite = Previews.IronWall_Bright;
				LayerID = 1;
				break;
			case "IronWall_Dark":
				TileID = 1;
				PreviewBlocSprite.sprite = Previews.IronWall_Dark;
				LayerID = 1;
				break;
			case "Cage":
				TileID = 2;
				PreviewBlocSprite.sprite = Previews.Cage;
				LayerID = 1;
				break;
			case "Fire":
				TileID = 5;
				PreviewBlocSprite.sprite = Previews.Fire;
				LayerID = 2;
				break;
			case "Light":
				TileID = 9;
				PreviewBlocSprite.sprite = Previews.Light;
				LayerID = 3;
				break;
			default:
				break;
		}
	}
}
