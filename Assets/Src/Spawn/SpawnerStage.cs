using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(StageController))]
public class SpawnerStage : MonoBehaviour {
	enum Modes {Off,Spawning, Check, Done}
	public float MinNextSpawn = 5, MaxNextSpawn=10;
	public int MaxShips=5;
	public int TotalShipsToSpawn = 10;
	public bool StopOnFull = false;

	[StoreThis]
	float nextSpawn = 1;

	[StoreThis]
	float counter = 0;

	[StoreThis]
	int curSpawner = 0;

	[StoreThis]
	int alreadySpawned = 0;

	List<ISpawner> Spawners = new List<ISpawner>();

	[StoreThis]
	Modes state = Modes.Off;

	public EnemyGeneratorController Generator {get;internal set;}
	public void ActivateStage()
	{
		//Generator = gen as EnemyGeneratorController;

		gameObject.SetActive(true);



		/*if(Spawners.Count==0)
		{
			gameObject.SetActive(false);
			state = Modes.Done;
		}*/

	}

	void Awake()
	{
		Generator = GetComponentInParent<EnemyGeneratorController>();
		Component[] comps = GetComponents<Component>();
		foreach(Component c in comps)
		{
			if(c is ISpawner)
				Spawners.Add(c as ISpawner);
			
		}
		state = Modes.Spawning;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(state==Modes.Spawning)
		{
			counter+=Time.deltaTime;
			if(counter>nextSpawn)
			{
				if(Spawners.Count==0)
				{
					state = Modes.Check;
				}
				else
				{
					if(Spawners[curSpawner].CanSpawn() || StopOnFull)
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
