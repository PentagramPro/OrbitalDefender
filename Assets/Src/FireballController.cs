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

	public FireballController PrefabInstantiate()
	{
		return ((GameObject)GameObject.Instantiate(gameObject)).GetComponent<FireballController>();
	}
}
