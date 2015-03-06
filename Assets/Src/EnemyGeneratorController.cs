using UnityEngine;
using System.Collections.Generic;

public class EnemyGeneratorController : MonoBehaviour {

	public UIController UI;

	public PlanetController Planet;

	StageController[] stages;

	[StoreThis]
	int curStage = 0;

	void Awake()
	{
		stages = GetComponentsInChildren<StageController>();

		foreach(StageController s in stages)
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
			stages[curStage].gameObject.SetActive(true);
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
		
		prefab.PrefabInstantiate(Planet, (Vector2)Planet.transform.position+orbit,(Vector2)Planet.transform.position+trace);
		
	}

	public void DoSpawnEnemy(EnemyShipController prefab, float low, float high, bool immobile, bool easy)
	{
		float angle = 0;
		if(easy==false)
		{
			if(Random.Range(0f,1f)<0.5f)
				angle = Random.Range(0f,150f);
			else
				angle = Random.Range(210f,359f);
		}
		else
		{
			if(Random.Range(0f,1f)<0.5f)
				angle = Random.Range(0f,90f);
			else
				angle = Random.Range(270,359f);
		}

		Vector2 orbit =  new Vector2(0,Random.Range(low,high)).Rotate(angle);
		Vector2 trace = new Vector2(orbit.y,-orbit.x).normalized*300;

		EnemyShipController ship = prefab.PrefabInstantiate(Planet, (Vector2)Planet.transform.position+orbit,(Vector2)Planet.transform.position+trace,immobile);

	}


}
