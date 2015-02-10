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

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Fireball")
			return;
		GameObject.Destroy(gameObject);
		coll.gameObject.SendMessage("OnFireball",this);
	}

	public FireballController PrefabInstantiate()
	{
		return ((GameObject)GameObject.Instantiate(gameObject)).GetComponent<FireballController>();
	}
}
