﻿using UnityEngine;
using System.Collections;

public class SpawnEnemy : SpawnBase, ISpawner {

	public EnemyShipController EnemyPrefab;
	public bool Easy = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}



	#region ISpawner implementation

	public void NextSpawn ()
	{
		stage.Generator.DoSpawnEnemy(EnemyPrefab,MinOrbit,MaxOrbit,true,Easy);
	}

	#endregion
}
