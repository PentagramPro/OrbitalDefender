﻿using UnityEngine;
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
				ship.PrefabName = prefabName;
				//ship.transform.position = str.ReadLineVector3();
				//LoadObject(str,ship.gameObject);
				ObjectSerializer.LoadObject(str,ship.gameObject);
				//LoadClass<EnemyShipController>(str,ship);
				//EnemyShipController enemy = GameObject.Instantiate()
			}

			PlanetController planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();

			ObjectSerializer.LoadObject(str,planet.gameObject);

			UIController ui = GameObject.Find("UICanvas").GetComponent<UIController>();;
			
			ObjectSerializer.LoadComponent(str,ui.HpBar);
			ObjectSerializer.LoadComponent(str,ui.Score);
			ObjectSerializer.LoadComponent(str,planet.Multiplier.Indicator);

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
			ObjectSerializer.StoreObject(str,es.gameObject);

		}

		PlanetController planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();

		ObjectSerializer.StoreObject(str,planet.gameObject);

		UIController ui = GameObject.Find("UICanvas").GetComponent<UIController>();

		ObjectSerializer.StoreComponent(str,ui.HpBar);
		ObjectSerializer.StoreComponent(str,ui.Score);
		ObjectSerializer.StoreComponent(str,planet.Multiplier.Indicator);


/*
		StreamWriter log = new StreamWriter("g:/unity-dev/OrbitalDefender/log.txt",false);
		log.WriteLine(str.ToString());
		log.Close();*/


		PlayerPrefs.SetString(CheckpointName,str.ToString());
	}


}
