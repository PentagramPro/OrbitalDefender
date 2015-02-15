using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {


	PlanetController planet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Enemy")
		{
			GameObject.Destroy(gameObject);
		}
		else if( coll.gameObject.tag=="Planet")
		{
			GameObject.Destroy(gameObject);
			PlanetController p = coll.gameObject.GetComponent<PlanetController>();
			p.Multiplier.ResetMultiplier();
		}
	}

	public void OnMissileCollision(MissileController missile)
	{
		planet.Multiplier.ResetMultiplier();
		GameObject.Destroy(gameObject);
		
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
