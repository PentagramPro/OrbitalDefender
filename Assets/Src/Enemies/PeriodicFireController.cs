using UnityEngine;
using System.Collections;

public class PeriodicFireController : MonoBehaviour {

	enum Modes {Idle,Fire}

	[StoreThis]
	Modes state = Modes.Idle;

	[StoreThis]
	CountTime counter = new CountTime();

	public float FireTime = 5;
	public float FireDamage = 10;
	public float FireImpulse = 5;
	public float TimeVariation = 0.1f;
	public FireballController FireballPrefab;

	PlanetController planet;
	// Use this for initialization
	void Start () {
		planet = GameObject.FindObjectOfType<PlanetController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(state==Modes.Fire)
		{
			if(counter.Count(FireTime))
			{
				counter.Reset(Random.Range(0f,FireTime*TimeVariation));
				FireballPrefab.PrefabInstantiate(FireDamage,transform.position,
				                                 (planet.transform.position-transform.position).normalized*FireImpulse);

			}
		}
	}



	public void StartFire()
	{
		counter.Reset(Random.Range(0f,FireTime*TimeVariation));
		state = Modes.Fire;
	}

	public void StopFire()
	{
		state = Modes.Idle;
	}
}
