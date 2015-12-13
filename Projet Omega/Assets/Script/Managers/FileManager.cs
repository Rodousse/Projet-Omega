using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Text;

public static class FileManager {
	
	static public void Exporter(string MapName)
	{
		tk2dTileMap TileMap = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();

		FileInfo file = new FileInfo(Application.persistentDataPath + '/' + MapName + ".omg");

		if (file.Exists)
			file.Delete();

		StreamWriter writer = file.CreateText();

		if (GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite == null)
			writer.Write("null");
		else
			writer.Write("Backgrounds/" + GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite.name);

		writer.Write(" " + MapName + " " + PlayerPrefs.GetString("Player_Name") + " " + DateTime.Now);

		for (int LayerID = 0; LayerID < TileMap.Layers.Length; LayerID++)
		{
			writer.Write("\n#");
			for (int X = 0; X < TileMap.width; X++)
			{
				writer.Write("\n");
				for (int Y = 0; Y < TileMap.height; Y++)
					writer.Write(TileMap.GetTile(X, Y, LayerID) + " ");
			}
		}
		writer.Close();
	}

	static public void Importer(string Filename)
	{
		tk2dTileMap TileMap = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();

		FileInfo file = new FileInfo(Application.persistentDataPath + '/' + Filename + ".omg");

		if (file.Exists)
		{
			StreamReader reader = file.OpenText();

			string BackgroundName = reader.ReadLine().Split(' ')[0];

			GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(BackgroundName);			

			string line;
			string tileID = "";
			int Y = -1, X = -1, LayerID = -1;
			TileMap.BeginEditMode();

			while ((line = reader.ReadLine()) != null)
			{
				if (line == "#")
				{
					LayerID++;
					X = -1;
				}
				else
					X++;

				Y = -1;

				foreach (char c in line)
					switch (c)
					{
						case ' ':
							Y++;
							TileMap.SetTile(X, Y, LayerID, int.Parse(tileID));
							tileID = "";
							break;
						case '#':
							break;
						default:
							tileID += c;
							break;
					}
			}

			TileMap.EndEditMode();
			reader.Close();
		}
	}

	static public string[] GetInfos(string Filename)
	{
		string[] infos = new string[4];
		/*
			1 : Background
			2 : Nom Map
			3 : Nom utilisateur
			4 : Date
		*/
		
		FileInfo file = new FileInfo(Application.persistentDataPath + '/' + Filename);

		if (file.Exists)
		{
			StreamReader reader = file.OpenText();
			infos = reader.ReadLine().Split(' ');

			reader.Close();
			return infos;
		}

		return null;
	}
}