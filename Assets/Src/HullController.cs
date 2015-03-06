using UnityEngine;
using System.Collections;

public class HullController : MonoBehaviour {

	[StoreThis]
	public float MaxHp = 10;

	[StoreThis]
	float hp;

	[StoreThis]
	bool stopReporting = false;

	public float Hp{
	get{
			return hp;
		}
	}

	public bool DamagedByMissiles = true;
	public bool DamagedByFireballs = false;

	public int InitialShieldPower = 0;

	public ShieldFxController Shield;

	[StoreThis]
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

	[ExecuteAfterLoad]
	void OnDeserialized()
	{
		ShieldPower = shieldPower;
	}

	void Awake()
	{
		hp = MaxHp;
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

	public void AddHp(float amount)
	{
		hp = Mathf.Min(MaxHp,Hp+amount);
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

				gameObject.SendMessage("OnShieldDestroyed", amount,SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			hp-=amount;
			if(hp<=0)
			{
				hp=0;
				if(!stopReporting)
					gameObject.SendMessage("OnHullDestroyed", amount,SendMessageOptions.DontRequireReceiver);
				stopReporting = true;
			}
			else
			{
				if(!stopReporting)
					gameObject.SendMessage("OnHullDamaged", amount,SendMessageOptions.DontRequireReceiver);
			}
		}

	}
}
