using UnityEngine;
using System.Collections;

public class SpawnAsteroid : SpawnBase, ISpawner {

	public AsteroidController AsteroidPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ISpawner implementation

	public void NextSpawn ()
	{
		stage.Generator.DoSpawnAsteroid(AsteroidPrefab,MinOrbit,MaxOrbit);
	}




	public bool CanSpawn ()
	{
		return stage.MaxShips>stage.Generator.Planet.Asteroids;
	}


	#endregion
}
