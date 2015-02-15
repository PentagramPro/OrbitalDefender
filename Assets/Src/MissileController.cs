using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	public GameObject ExplosionEffect;
	public float Lifetime = 10;
	public float Damage {get;set;}
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
			Destory();
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Planet" || coll.gameObject.tag=="Fireball" )
		{
			Destory();
		}
		else if(coll.gameObject.tag=="Enemy" || coll.gameObject.tag=="Asteroid")
		{
			coll.gameObject.SendMessage("OnMissileCollision",this);
			Destory();
			//time = Lifetime-1;
		}
	}

	void Destory()
	{
		GameObject.Instantiate(ExplosionEffect,transform.position,Quaternion.identity);
		GameObject.Destroy(gameObject);
	}
}
