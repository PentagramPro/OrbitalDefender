using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

	enum Modes{
		Appearing,Flying,Orbiting,Exploding
	}
	public FxRemover ExplosionsFx;
	public FireballController FireballPrefab;

	public int Score = 10;
	public float FirePeriod = 6;
	public float FireDamage = 5;
	public float FireImpulse = 5;

	public float FireAnimationShift = 0.5f;

	CameraController camController;

	[StoreThis]
	CountTime counter = new CountTime();

	//float fireCounter = 0;
	[StoreThis]
	Modes state = Modes.Flying;

	public PlanetController Planet;
	FlyController flyController;
	HullController hull;
	Animator animator;


	//float explosionCounter = 0;
	public float AppearingTime = 3;
	public float ExplosionTime = 3;
	FxRemover explosionObject;
	bool fireAnimationTriggered = false;

	public string PrefabName{get;set;}

	void Awake()
	{
		hull = GetComponent<HullController>();
		flyController = GetComponent<FlyController>();
		flyController.OnFlyingComplete+=OnFlyComplete;
		camController = Camera.main.GetComponent<CameraController>();
		animator = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
		if(Planet==null)
			Planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
		Planet.EnemyShips++;
	}


	void OnDestroy()
	{
		Planet.EnemyShips--;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Enemy" || coll.gameObject.tag=="Planet")
		{
			StartDestruction();
		}
	}

	void OnShieldDestroyed(float amount)
	{
		if(state==Modes.Orbiting)
			rigidbody2D.isKinematic = false;
	}

	void OnHullDamaged(float amount) 
	{

		
	}

	public void OnHullDestroyed(float amount)
	{
		Planet.OnEnemyDestroyed(Score);
		StartDestruction();

	}

	void StartDestruction()
	{
		counter.Reset();
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

			if(counter.Count(ExplosionTime))
			{
				explosionObject.transform.parent = null;
				explosionObject.enabled = true;
				explosionObject.particleSystem.Stop();
				GameObject.Destroy(gameObject);
			}
		}
		else
		{
			float dist = (transform.position-Planet.transform.position).magnitude;
			if(dist>camController.MaxSize)
				StartDestruction();

			if(state==Modes.Orbiting)
			{
				if(!fireAnimationTriggered && counter.Count(FirePeriod-FireAnimationShift))
				{
					animator.SetTrigger("Fire");
					fireAnimationTriggered = true;
				}
				if(counter.Count(FirePeriod))
				{
					FireballController fireball = FireballPrefab.PrefabInstantiate();
					fireball.Damage = FireDamage;
					fireball.transform.position = transform.position;
					fireball.rigidbody2D.AddForce(  (Planet.transform.position-transform.position).normalized*FireImpulse,ForceMode2D.Impulse);
					counter.Reset(Random.Range(0,FirePeriod*0.2f));
					fireAnimationTriggered= false;
				}
			}
			else if(state==Modes.Appearing)
			{
				if(counter.Count(AppearingTime))
				{
					counter.Reset();
					state = Modes.Orbiting;
					collider2D.enabled = true;
				}
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
		HullController hull = ship.GetComponent<HullController>();
		Animator anim = ship.GetComponent<Animator>();

		ship.PrefabName = name;
		if(immobile)
		{
			ship.state = Modes.Appearing;

			ship.rigidbody2D.isKinematic = hull.InitialShieldPower>0;

			ship.transform.position = orbit;
			fly.enabled = false;
			pgrav.enabled = false;
			collider2D.enabled = false;
			anim.SetTrigger("Appear");
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
