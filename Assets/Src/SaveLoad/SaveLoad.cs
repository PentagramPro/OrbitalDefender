using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Linq;

public class SaveLoad : MonoBehaviour {


	public static readonly string CheckpointName = "Checkpoint";

	void Start()
	{

	}

	void Update()
	{
		StringReaderEx str = new StringReaderEx(PlayerPrefs.GetString(CheckpointName));
		
		int enemiesCount = str.ReadLineInt();
		Debug.Log("Load: enemies = "+enemiesCount);
		for(int i=0;i<enemiesCount;i++)
		{
			string prefabName = str.ReadLine();
			string path = "Assets/Resources/Prefabs/Ships/"+prefabName+".prefab";
			
			GameObject prefab = (GameObject)Resources.LoadAssetAtPath(path,typeof(GameObject));
			Debug.Log("path: "+path+", val="+(prefab==null));
			EnemyShipController ship = ((GameObject)GameObject.Instantiate(prefab)).GetComponent<EnemyShipController>();
			
			ship.transform.position = str.ReadLineVector3();
			
			LoadClass<EnemyShipController>(str,ship);
			//EnemyShipController enemy = GameObject.Instantiate()
		}

		GameObject.Destroy(gameObject);
	}
	public static void Resume()
	{
		GameObject g = new GameObject();
		g.AddComponent<SaveLoad>();
		GameObject.DontDestroyOnLoad(g);

		Application.LoadLevel("game");


	}

	public static void Checkpoint()
	{
		StringWriterEx str = new StringWriterEx();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		str.WriteLine(enemies.Length);

		foreach(GameObject enemy in enemies)
		{
			EnemyShipController es = enemy.GetComponent<EnemyShipController>();

			Debug.Log("Save: storing "+es.PrefabName);
			str.WriteLine(es.PrefabName);


			str.WriteLine(es.transform.position);

			StoreClass<EnemyShipController>(str,es);
		}

		PlayerPrefs.SetString(CheckpointName,str.ToString());
	}

	static void StoreClass<T>(StringWriterEx str,T obj)
	{
		Type t = typeof(T);
		var fields= t.GetFields().Where(f=>f.GetCustomAttributes(typeof(StoreThis),true).Length>0).ToArray();
	
		str.WriteLine(fields.Length);
		foreach(FieldInfo f in fields)
		{
			str.WriteLine(f.Name);

			str.WriteLine(StoreBase.SaveField(f,obj));


		
		}

	}

	static void LoadClass<T>(StringReader str, T obj)
	{
		Type t = typeof(T);
		var fields= t.GetFields().Where(f=>f.GetCustomAttributes(typeof(StoreThis),true).Length>0).ToArray();

		int len = int.Parse(str.ReadLine());
		if(fields.Length!=len)
			throw new UnityException("Incorrect save file");

		for(int i=0;i<len;i++)
		{
			string fieldName = str.ReadLine();
			string fieldData = str.ReadLine();

			FieldInfo finfo = fields.Where(f=>f.Name==fieldName).First();
			if(finfo==null)
				continue;
			StoreBase.LoadField(finfo,obj,fieldData);
		}
	}
}
