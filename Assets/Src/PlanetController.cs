using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public EnemyShipController EnemyShipPrefab;
	public float MaxGravity=35, MaxDistance=500;
	// Use this for initialization
	void Start () {
		EnemyShipController ship = ((GameObject)Object.Instantiate(EnemyShipPrefab.gameObject)).GetComponent<EnemyShipController>();

		Vector2 orbit =  new Vector2(0,100).Rotate(Random.Range(0,359));
		Vector2 trace = new Vector2(orbit.y,-orbit.x).normalized*300;

		ship.OrbitPoint = orbit+(Vector2)transform.position;
		ship.Planet = this;
		ship.transform.position = ship.OrbitPoint+trace;

	}

	public float CalculateGravity(float distance)
	{
		return Mathf.Max((1.0f - distance / MaxDistance) * MaxGravity,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
