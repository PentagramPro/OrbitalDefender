﻿using UnityEngine;
using System.Collections.Generic;

public class EnemyGeneratorController : MonoBehaviour {

	public MessageController MsgController;

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
		

	}
	public void DoSpawnAsteroid(AsteroidController prefab, float low, float high)
	{
		
		Vector2 orbit =  new Vector2(0,Random.Range(low,high)).Rotate(Random.Range(0,359));
		Vector2 trace = new Vector2(orbit.y,-orbit.x).normalized*300;
		
		AsteroidController a = prefab.PrefabInstantiate(Planet, (Vector2)Planet.transform.position+orbit,(Vector2)Planet.transform.position+trace);
		
	}

	public void DoSpawnEnemy(EnemyShipController prefab, float low, float high, bool immobile)
	{
		float angle = 0;
		if(Random.Range(0f,1f)<0.5f)
			angle = Random.Range(0f,150f);
		else
			angle = Random.Range(210f,359f);

		Vector2 orbit =  new Vector2(0,Random.Range(low,high)).Rotate(angle);
		Vector2 trace = new Vector2(orbit.y,-orbit.x).normalized*300;

		EnemyShipController ship = prefab.PrefabInstantiate(Planet, (Vector2)Planet.transform.position+orbit,(Vector2)Planet.transform.position+trace,immobile);

	}


}
