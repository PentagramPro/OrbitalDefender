using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	public struct FragmentsArg
	{
		public GameObject cause;
		public FragmentsArg(GameObject cause)
		{
			this.cause = cause;
		}
	}
	public FxRemover TrailEffect;
	public GameObject ExplosionEffect;
	public float Lifetime = 10;
	public float Damage {get;set;}
	public bool ResetMultiplier = true;

	float time = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		time+=Time.fixedDeltaTime;
		if(time>Lifetime)
			Destory(null);
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{

		if(coll.gameObject.tag=="Enemy" || coll.gameObject.tag=="Asteroid" || coll.gameObject.tag=="Planet" ||coll.gameObject.tag=="Fireball" || coll.gameObject.tag=="Powerup")
		{
			coll.gameObject.SendMessage("OnMissileCollision",this);
			Destory(coll.gameObject);
			//time = Lifetime-1;
		}
	}

	public void DetachTrail()
	{
		if(TrailEffect!=null)
		{
			TrailEffect.transform.parent = null;
			TrailEffect.enabled = true;
			ParticleSystem ps = TrailEffect.GetComponent<ParticleSystem>();
			if(ps!=null)
				ps.Stop();
		}
	}

	void Destory(GameObject cause)
	{

		DetachTrail();
		gameObject.SendMessage("OnLaunchFragments",new FragmentsArg(cause),SendMessageOptions.DontRequireReceiver);
		GameObject.Instantiate(ExplosionEffect,transform.position,Quaternion.identity);
		GameObject.Destroy(gameObject);

	}

	public MissileController PrefabInstantiate(Vector3 position, float damage, Vector2 impulse)
	{
		GameObject go = GameObject.Instantiate(gameObject) as GameObject;
		MissileController mc = go.GetComponent<MissileController>();
		mc.transform.position = position;
		mc.Damage = damage;
		mc.GetComponent<Rigidbody2D>().AddForce(impulse,ForceMode2D.Impulse);

		return mc;
	}
}
