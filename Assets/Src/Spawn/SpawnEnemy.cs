using UnityEngine;
using System.Collections;

public class SpawnEnemy : SpawnBase, ISpawner {

	public EnemyShipController EnemyPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}



	#region ISpawner implementation

	public void NextSpawn ()
	{
		stage.Generator.DoSpawnEnemy(EnemyPrefab,MinOrbit,MaxOrbit,true);
	}

	#endregion
}
