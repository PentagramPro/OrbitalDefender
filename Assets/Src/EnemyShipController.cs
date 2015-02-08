using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

	enum Modes{
		Flying,Orbiting,Exploding
	}
	public FxRemover ExplosionsFx;

	Modes state = Modes.Flying;

	public PlanetController Planet;
	FlyController flyController;



	float explosionCounter = 0;
	public float ExplosionTime = 3;
	FxRemover explosionObject;

	void Awake()
	{
	
		flyController = GetComponent<FlyController>();
		flyController.OnFlyingComplete+=OnFlyComplete;
	}
	// Use this for initialization
	void Start () {
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
		if(state==Modes.Exploding)
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

	void OnFlyComplete()
	{
		state = Modes.Orbiting;
	}

	public EnemyShipController PrefabInstantiate(PlanetController planet, Vector2 orbit, Vector2 position, bool immobile)
	{
		EnemyShipController ship = ((GameObject)Object.Instantiate(gameObject)).GetComponent<EnemyShipController>();
		FlyController fly = ship.GetComponent<FlyController>();
		PointGravityController pgrav = ship.GetComponent<PointGravityController>();

		if(immobile)
		{
			ship.state = Modes.Orbiting;
			ship.rigidbody2D.isKinematic = false;
			ship.transform.position = orbit;
			fly.enabled = false;
			pgrav.enabled = false;
		}
		else
		{
			ship.state = Modes.Flying;
			fly.PrepareFly(orbit,planet);

			ship.transform.position = position;
		}


		ship.Planet = planet;


		return ship;
	}
}
