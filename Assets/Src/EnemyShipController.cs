using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

	enum Modes{
		Flying,Orbiting
	}

	Modes state = Modes.Flying;
	public Vector2 OrbitPoint;
	public PlanetController Planet;
	PointGravityController PointGravity;
	Vector2 Direction;
	float distanceToOrbit = 0;
	public float FlySpeed = 100;

	void Awake()
	{
		PointGravity = GetComponent<PointGravityController>();

	}
	// Use this for initialization
	void Start () {
		PointGravity.enabled =false;
		rigidbody2D.isKinematic = true;
		distanceToOrbit = ((Vector2)transform.position-OrbitPoint).magnitude;
		Direction = OrbitPoint-(Vector2)(transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(state==Modes.Flying)
		{
			float newDist = ((Vector2)transform.position-OrbitPoint).magnitude;
			if(newDist>distanceToOrbit)
			{
				state = Modes.Orbiting;
				rigidbody2D.isKinematic = false;
				PointGravity.enabled = true;
				float toPlanet = (transform.position-Planet.transform.position).magnitude;
				rigidbody2D.AddForce(Direction.normalized*Mathf.Sqrt(Planet.CalculateGravity(toPlanet)*toPlanet), ForceMode2D.Impulse);
			}
			else
			{
				transform.position+=(Vector3)(Direction.normalized*FlySpeed*Time.fixedDeltaTime);
			}
			distanceToOrbit = newDist;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(OrbitPoint, 0.5f);
		Gizmos.DrawLine(OrbitPoint,OrbitPoint-Direction);
	}
}
