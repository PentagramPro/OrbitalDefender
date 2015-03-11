using UnityEngine;
using System.Collections.Generic;

public class AsteroidController : MonoBehaviour {

	public List<PowerupController> Powerups;

	PlanetController planet;
	// Use this for initialization
	void Start () {
		if(planet==null)
			planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();

		planet.Asteroids++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Enemy")
		{
			StartDestruction();
		}
		else if( coll.gameObject.tag=="Planet")
		{
			StartDestruction();
			PlanetController p = coll.gameObject.GetComponent<PlanetController>();
			p.Multiplier.ResetMultiplier();
		}
	}

	void StartDestruction()
	{
		planet.Asteroids--;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Animator>().SetTrigger("Explode");
		if(Powerups!=null && Powerups.Count>0)
		{
			PowerupController prefab = Powerups[Random.Range(0,Powerups.Count-1)];
			GameObject o = (GameObject)GameObject.Instantiate(prefab.gameObject);
			o.transform.position = transform.position;
		}


	}

	public void OnExploded()
	{
		GameObject.Destroy(gameObject);
	}

	public void OnMissileCollision(MissileController missile)
	{
		planet.Multiplier.ResetMultiplier();
		StartDestruction();
		
	}

	public AsteroidController PrefabInstantiate(PlanetController planet, Vector2 orbit, Vector2 position)
	{
		AsteroidController a = ((GameObject)GameObject.Instantiate(gameObject)).GetComponent<AsteroidController>();
		FlyController fly = a.GetComponent<FlyController>();

		a.transform.position = position;
		a.planet = planet;
		fly.PrepareFly(orbit,planet);
		return a;
	}
}
