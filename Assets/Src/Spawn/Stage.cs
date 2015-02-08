using UnityEngine;
using System.Collections.Generic;

public class Stage : MonoBehaviour {
	enum Modes {Off,Spawning, Check, Done}
	public float MinNextSpawn = 5, MaxNextSpawn=10;
	public int MaxShips=5;
	public int TotalShipsToSpawn = 10;
	float nextSpawn = 1;
	float counter = 0;
	int curSpawner = 0;
	int alreadySpawned = 0;
	List<ISpawner> Spawners = new List<ISpawner>();
	Modes state = Modes.Off;

	public EnemyGeneratorController Generator {get;internal set;}
	public void ActivateStage(EnemyGeneratorController gen)
	{
		Generator = gen;

		gameObject.SetActive(true);

		Component[] comps = GetComponents<Component>();
		foreach(Component c in comps)
		{
			if(c is ISpawner)
				Spawners.Add(c as ISpawner);
		
		}
		state = Modes.Spawning;
		if(Spawners.Count==0)
		{
			gameObject.SetActive(false);
			state = Modes.Done;
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(state==Modes.Spawning)
		{
			counter+=Time.deltaTime;
			if(counter>nextSpawn && Generator.Planet.EnemyShips<MaxShips)
			{
				Spawners[curSpawner].NextSpawn();
				alreadySpawned++;

				if(alreadySpawned>=TotalShipsToSpawn)
				{
					state = Modes.Check;
				}
				else
				{
					counter = 0;
					nextSpawn = Random.Range(MinNextSpawn,MaxNextSpawn);
					curSpawner++;
					if(curSpawner>=Spawners.Count)
						curSpawner = 0;
				}
			}
		}
		else if(state==Modes.Check)
		{
			if(Generator.Planet.EnemyShips==0)
			{
				Generator.OnStageComplete();
				state = Modes.Done;
				gameObject.SetActive(false);
			}
		}
	}
}
