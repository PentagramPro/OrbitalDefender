using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	public GameObject ExplosionEffect;
	public float Lifetime = 10;
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
		if(coll.gameObject.tag=="Planet")
		{
			Destory();
		}
		else if(coll.gameObject.tag=="Enemy")
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
