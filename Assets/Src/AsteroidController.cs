using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public AsteroidController PrefabInstantiate(PlanetController planet, Vector2 orbit, Vector2 position)
	{
		AsteroidController a = ((GameObject)GameObject.Instantiate(gameObject)).GetComponent<AsteroidController>();

		return a;
	}
}
