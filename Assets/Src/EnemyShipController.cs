using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

	enum Modes{
		Flying,Orbiting,Exploding
	}
	public FxRemover ExplosionsFx;

	Modes state = Modes.Flying;
	public Vector2 OrbitPoint;
	public PlanetController Planet;
	PointGravityController PointGravity;
	Vector2 Direction;
	float distanceToOrbit = 0;
	public float FlySpeed = 100;

	float explosionCounter = 0;
	public float ExplosionTime = 3;
	FxRemover explosionObject;

	void Awake()
	{
		PointGravity = GetComponent<PointGravityController>();

	}
	// Use this for initialization
	void Start () {
		//PointGravity.enabled =false;
		//rigidbody2D.isKinematic = true;
		distanceToOrbit = ((Vector2)transform.position-OrbitPoint).magnitude;
		Direction = OrbitPoint-(Vector2)(transform.position);
		Planet.EnemyShips++;
	}


	void OnDestroy()
	{
		Planet.EnemyShips--;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Enemy")
		{
			StartDestruction();
		}
	}

	public void OnMissileCollision(MissileController missile)
	{

		StartDestruction();

	}

	void StartDestruction()
	{
		explosionCounter = 0;
		state = Modes.Exploding;
		explosionObject = ((GameObject)GameObject.Instantiate(ExplosionsFx.gameObject)).GetComponent<FxRemover>();
		
		explosionObject.transform.parent = transform;
		explosionObject.transform.localPosition = Vector2.zero;
		explosionObject.enabled = false;
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
		else if(state==Modes.Exploding)
		{
			explosionCounter+=Time.fixedDeltaTime;
			if(explosionCounter>ExplosionTime)
			{
				explosionObject.transform.parent = null;
				explosionObject.enabled = true;
				explosionObject.particleSystem.Stop();
				GameObject.Destroy(gameObject);
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(OrbitPoint, 0.5f);
		Gizmos.DrawLine(OrbitPoint,OrbitPoint-Direction);
	}

	public EnemyShipController PrefabInstantiate(PlanetController planet, Vector2 orbit, Vector2 position, bool immobile)
	{
		EnemyShipController ship = ((GameObject)Object.Instantiate(gameObject)).GetComponent<EnemyShipController>();
		ship.PointGravity = ship.GetComponent<PointGravityController>();
		if(immobile)
		{
			ship.state = Modes.Orbiting;
			ship.rigidbody2D.isKinematic = false;
			ship.transform.position = orbit;
			ship.PointGravity.enabled =false;
		}
		else
		{
			ship.state = Modes.Flying;
			ship.OrbitPoint = orbit;
			ship.transform.position = position;
		}


		ship.Planet = planet;

		return ship;
	}
}
