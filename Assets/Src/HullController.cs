using UnityEngine;
using System.Collections;

public class HullController : MonoBehaviour {

	public float MaxHp;
	public float Hp{get;internal set;}

	public bool DamagedByMissiles = true;
	public bool DamagedByFireballs = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMissileCollision(MissileController missile)
	{
		if(!DamagedByMissiles)
			return;
		Damage(missile.Damage);
		
	}

	void OnFireball(FireballController fireball) 
	{
		if(!DamagedByFireballs)
			return;
		Damage(fireball.Damage);
		
	}

	void Damage(float amount)
	{
		Hp-=amount;
		if(Hp<=0)
		{
			Hp=0;
			gameObject.SendMessage("OnHullDestroyed", amount);
		}
		else
		{
			gameObject.SendMessage("OnHullDamaged", amount);
		}


	}
}
