using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Linq;

public class SaveLoad : MonoBehaviour {


	public static readonly string CheckpointName = "Checkpoint";
	static BindingFlags getFieldFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
	void Start()
	{

	}

	void Update()
	{
		try
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
				
				//ship.transform.position = str.ReadLineVector3();
				LoadObject(str,ship.gameObject);
				//LoadClass<EnemyShipController>(str,ship);
				//EnemyShipController enemy = GameObject.Instantiate()
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Exception while trying to load level: "+e.ToString());
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


			//str.WriteLine(es.transform.position);

			//StoreClass<EnemyShipController>(str,es);
			StoreObject(str,es.gameObject);
		}


		StreamWriter log = new StreamWriter("g:/unity-dev/OrbitalDefender/log.txt",false);

		log.WriteLine(str.ToString());
		log.Close();
		PlayerPrefs.SetString(CheckpointName,str.ToString());
	}

	static void StoreObject(StringWriterEx str, GameObject obj)
	{
		Component[] components = obj.GetComponents<Component>();

		str.WriteLine(obj.activeSelf);

		str.WriteLine(components.Length);



		foreach(Component c in components)
		{
			str.WriteLine(c.GetType().Name);
			if(c is MonoBehaviour)
				str.WriteLine((c as MonoBehaviour).enabled);
			else
				str.WriteLine("true");
			StoreClass<Component>(str,c);
		}
	}

	static void LoadObject(StringReaderEx str, GameObject obj)
	{
		Component[] components = obj.GetComponents<Component>();
		Dictionary<string,Component> byName = new Dictionary<string, Component>();

		foreach(Component c in components)
			byName[c.GetType().Name] = c;

		bool isActive = str.ReadLineBool();

		int len = str.ReadLineInt();

		for(int i=0;i<len;i++)
		{
			string name = str.ReadLine();
			bool enabled = str.ReadLineBool();
			if(byName.ContainsKey(name))
			{
				Component c = byName[name];
				if(c is MonoBehaviour)
					(c as MonoBehaviour).enabled = enabled;
				LoadClass<Component>(str,c);
			}
		}

		obj.SetActive(isActive);
	}

	static void StoreClass<T>(StringWriterEx str,T obj)
	{
		str.WriteLine("class");
		Type t = obj.GetType();
		Debug.Log("   > Storing class "+t.Name);
		if(t==typeof(Transform))
		{
			StoreClassTransform(str, obj as Transform);
		}
		else if(t==typeof(Rigidbody2D))
		{
			StoreClassRigidbody2D(str, obj as Rigidbody2D);
		}
		else
		{
			var fields= t.GetFields(getFieldFlags).Where(f=>f.GetCustomAttributes(typeof(StoreThis),true).Length>0).ToArray();
		
			str.WriteLine(fields.Length);
			Debug.Log("   > Class has "+fields.Length+" fields");
			foreach(FieldInfo f in fields)
			{
				str.WriteLine(f.Name);

				str.WriteLine(StoreBase.SaveField(f,obj));
			
			}
		}
	}

	static void StoreClassTransform(StringWriterEx str, Transform obj)
	{
		str.WriteLine(obj.transform.position);
		str.WriteLine(obj.transform.localScale);
		str.WriteLine(obj.transform.rotation.eulerAngles);
	}
	
	static void StoreClassRigidbody2D(StringWriterEx str, Rigidbody2D obj)
	{
		str.WriteLine(obj.isKinematic);
		str.WriteLine(obj.gravityScale);
	}

	static void LoadClass<T>(StringReaderEx str, T obj)
	{
		if(str.ReadLine()!="class")
		{
			Debug.LogError("Magic string not found in output! Next lines are: "+str.ReadLine()+";"+str.ReadLine()+";"+str.ReadLine());
			throw new UnityException("Load exception");
		}

		Type t = obj.GetType();
		Debug.Log("   > Loading class "+t.Name);
		if(t==typeof(Transform))
		{
			LoadClassTransform(str, obj as Transform);
		}
		else if(t==typeof(Rigidbody2D))
		{
			LoadClassRigidbody2D(str, obj as Rigidbody2D);
		}
		else
		{
			var fields= t.GetFields(getFieldFlags).Where(f=>f.GetCustomAttributes(typeof(StoreThis),true).Length>0).ToArray();

			int len = str.ReadLineInt();
			if(fields.Length!=len)
				throw new UnityException("Incorrect save file");

			for(int i=0;i<len;i++)
			{
				string fieldName = str.ReadLine();
				string fieldData = str.ReadLine();

				Debug.Log("    > Loading field "+fieldName);
				FieldInfo finfo = fields.Where(f=>f.Name==fieldName).First();
				if(finfo==null)
					continue;
				StoreBase.LoadField(finfo,obj,fieldData);
			}
		}
	}

	static void LoadClassTransform(StringReaderEx str, Transform obj)
	{
		Debug.Log("   > Loading class Transform");
		obj.position = str.ReadLineVector3();
		obj.localScale = str.ReadLineVector3();
		obj.rotation = Quaternion.Euler(str.ReadLineVector3());
	}

	static void LoadClassRigidbody2D(StringReaderEx str, Rigidbody2D obj)
	{
		Debug.Log("   > Loading class Rigidbody2D");
		obj.isKinematic = str.ReadLineBool();
		obj.gravityScale = str.ReadLineFloat();
	}
}
