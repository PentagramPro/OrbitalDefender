using UnityEngine;
using System.Collections;

public class HullController : MonoBehaviour {

	public float MaxHp = 10;
	public float Hp{get;internal set;}

	public bool DamagedByMissiles = true;
	public bool DamagedByFireballs = false;

	public int ShieldPower = 0;
	public ShieldFxController Shield;

	void Awake()
	{
		Hp = MaxHp;
	}
	// Use this for initialization
	void Start () {
		if(Shield!=null && ShieldPower>0)
			Shield.ShieldEnabled = true;
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
		if(ShieldPower>0)
		{
			ShieldPower--;
			if(ShieldPower==0)
			{
				Shield.ShieldEnabled = false;
				gameObject.SendMessage("OnShieldDestroyed", amount);
			}
		}
		else
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
}
