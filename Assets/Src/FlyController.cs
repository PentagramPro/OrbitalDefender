using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour {
	public delegate void FlyingComplete();
	public event FlyingComplete OnFlyingComplete;

	public Vector2 OrbitPoint;

	public float FlySpeed = 100;
	public PlanetController Planet;

	PointGravityController PointGravity;
	float distanceToOrbit = 0;
	Vector2 Direction;

	void Awake()
	{
		PointGravity = GetComponent<PointGravityController>();
		Planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
	}

	// Use this for initialization
	void Start () {
		distanceToOrbit = ((Vector2)transform.position-OrbitPoint).magnitude;
		Direction = OrbitPoint-(Vector2)(transform.position);
	}
	
	void FixedUpdate () {

		float newDist = ((Vector2)transform.position-OrbitPoint).magnitude;
		if(newDist>distanceToOrbit)
		{

			rigidbody2D.isKinematic = false;
			PointGravity.enabled = true;
			float toPlanet = (transform.position-Planet.transform.position).magnitude;
			rigidbody2D.AddForce(Direction.normalized*Mathf.Sqrt(Planet.CalculateGravity(toPlanet)*toPlanet), ForceMode2D.Impulse);

			if(OnFlyingComplete!=null)
				OnFlyingComplete();

			enabled = false;
		}
		else
		{
			transform.position+=(Vector3)(Direction.normalized*FlySpeed*Time.fixedDeltaTime);
		}
		distanceToOrbit = newDist;


	}

	public void PrepareFly(Vector2 orbitPoint, PlanetController planet)
	{
		OrbitPoint = orbitPoint;
		Planet = planet;
		rigidbody2D.isKinematic = true;
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(OrbitPoint, 0.5f);
		Gizmos.DrawLine(OrbitPoint,OrbitPoint-Direction);
	}
}
