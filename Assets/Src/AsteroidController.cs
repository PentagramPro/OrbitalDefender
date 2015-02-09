using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Enemy" || coll.gameObject.tag=="Planet")
		{
			GameObject.Destroy(gameObject);
		}
	}

	public void OnMissileCollision(MissileController missile)
	{
		
		GameObject.Destroy(gameObject);
		
	}

	public AsteroidController PrefabInstantiate(PlanetController planet, Vector2 orbit, Vector2 position)
	{
		AsteroidController a = ((GameObject)GameObject.Instantiate(gameObject)).GetComponent<AsteroidController>();
		FlyController fly = a.GetComponent<FlyController>();

		a.transform.position = position;

		fly.PrepareFly(orbit,planet);
		return a;
	}
}
