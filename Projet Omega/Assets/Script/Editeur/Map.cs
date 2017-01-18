using System.IO;
using UnityEngine;

public class Map : MonoBehaviour{

	public void Importer()
	{
		FileManager.Importer(gameObject.name);
		GetComponentInParent<Options>().Switch();
	}

	public void Supprimer()
	{
		File.Delete(Application.persistentDataPath + '/' + gameObject.name + ".png");
		File.Delete(Application.persistentDataPath + '/' + gameObject.name + ".omg");

		GetComponentInParent<RemplirListeMaps>().RefreshList();
	}
}
