using UnityEngine;
using System.Collections.Generic;

public class PointGravityController : MonoBehaviour {

	List<PlanetController> Planets = new List<PlanetController>();
	// Use this for initialization
	void Start () {
	
		GameObject[] p = GameObject.FindGameObjectsWithTag("Planet");
		foreach(GameObject o in p)
		{
			Planets.Add(o.GetComponent<PlanetController>());
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(PlanetController planet in Planets)
		{
			float dist = Vector3.Distance(planet.transform.position, transform.position);
			if (dist <= planet.MaxDistance) {
				Vector3 v = planet.transform.position - transform.position;
				GetComponent<Rigidbody2D>().AddForce(v.normalized * planet.CalculateGravity(dist, GetComponent<Rigidbody2D>().mass));
			}
		}
	}
}
