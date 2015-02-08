using UnityEngine;
using System.Collections.Generic;

public class EnemyGeneratorController : MonoBehaviour {


	public PlanetController Planet;

	Stage[] stages;
	int curStage = 0;

	void Awake()
	{
		stages = GetComponentsInChildren<Stage>();

		foreach(Stage s in stages)
		{
			s.gameObject.SetActive(false);
		}
	}


	public void OnStageComplete()
	{
		curStage++;
		ActivateCurrentStage();
	}

	void ActivateCurrentStage()
	{
		if(curStage>=stages.Length)
		{
			gameObject.SetActive(false);
		}
		else
		{
			stages[curStage].ActivateStage(this);
		}
	}
	// Use this for initialization
	void Start () {
		ActivateCurrentStage();
	}
	
	// Update is called once per frame
	void Update () {
		
		/*counter+=Time.deltaTime;
		elapsed+=Time.deltaTime;
		if(counter>nextSpawn && Planet.EnemyShips<MaxShips)
		{
			if(elapsed<BigShipsTime)
			{
				Spawn(SmallShip1, MinOrbit,SmallOrbitRange,true);
				counter = 0;
				nextSpawn = Random.Range(5,10);
			}
			else
			{
				if(Random.Range(0,1)>0.7f)
					Spawn(SmallShip1, MinOrbit,SmallOrbitRange,true);
				else
					Spawn(BigShip1, SmallOrbitRange,MaxOrbit,true);
				counter = 0;
				nextSpawn = Random.Range(5,10);
			}
		}*/
	}


	public void DoSpawnEnemy(EnemyShipController prefab, float low, float high, bool immobile)
	{

		Vector2 orbit =  new Vector2(0,Random.Range(low,high)).Rotate(Random.Range(0,359));
		Vector2 trace = new Vector2(orbit.y,-orbit.x).normalized*300;

		EnemyShipController ship = prefab.PrefabInstantiate(Planet, (Vector2)Planet.transform.position+orbit,(Vector2)Planet.transform.position+trace,immobile);

	}


}
