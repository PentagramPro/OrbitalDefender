using UnityEngine;
using System.Collections.Generic;

public class PointGravityController : MonoBehaviour {

	List<Transform> Planets = new List<Transform>();
	float maxGravDist = 500;
	float maxGravity = 35.0f;
	// Use this for initialization
	void Start () {
	
		GameObject[] p = GameObject.FindGameObjectsWithTag("Planet");
		foreach(GameObject o in p)
		{
			Planets.Add(o.GetComponent<Transform>());
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(Transform planet in Planets)
		{
			float dist = Vector3.Distance(planet.position, transform.position);
			if (dist <= maxGravDist) {
				Vector3 v = planet.position - transform.position;
				rigidbody2D.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
			}
		}
	}
}
