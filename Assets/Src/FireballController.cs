using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

	public float Damage {get;set;}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMissileCollision(MissileController missile)
	{
		GameObject.Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Fireball")
			return;
		else
		{
			coll.gameObject.SendMessage("OnFireball",this);
			GameObject.Destroy(gameObject);
		}
	}

	public FireballController PrefabInstantiate(float damage, Vector3 position, Vector2 impulse)
	{
		FireballController fireball = ((GameObject)GameObject.Instantiate(gameObject)).GetComponent<FireballController>();
		fireball.Damage = damage;
		fireball.transform.position = position;
		fireball.GetComponent<Rigidbody2D>().AddForce( impulse,ForceMode2D.Impulse);
		return fireball;
	}
}
