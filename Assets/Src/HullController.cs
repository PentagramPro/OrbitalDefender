using UnityEngine;
using System.Collections;

public class HullController : MonoBehaviour {

	public float MaxHp = 10;
	public float Hp{get;internal set;}

	public bool DamagedByMissiles = true;
	public bool DamagedByFireballs = false;

	public int InitialShieldPower = 0;

	public ShieldFxController Shield;
	int shieldPower = 0;

	public int ShieldPower{
		get{
			return shieldPower;
		}
		set{
			shieldPower = Mathf.Max(value,0);
			Shield.ShieldEnabled = value>0;
		}
	}

	void Awake()
	{
		Hp = MaxHp;
	}
	// Use this for initialization
	void Start () {
		if(Shield!=null)
		{

			ShieldPower = InitialShieldPower;
		}
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
