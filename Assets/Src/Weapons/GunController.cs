using UnityEngine;
using System.Collections.Generic;

public class GunController : MonoBehaviour {

	public Transform WeaponsList;

	public BarrelController Barrel;

	public float Damage = 10;

	[StoreThis]
	string weaponName;

	WeaponController weapon = null;

	public WeaponController Weapon{
		get{
			if(weapon==null)
			{
				weapon = WeaponsList.Find(weaponName).GetComponent<WeaponController>();
				if(weapon==null)
					throw new UnityException("Cannot find weapon "+weaponName);
			}
			return weapon;
		}
	}

	//PlanetController planet;

	void Awake()
	{
		WeaponController[] weapons = WeaponsList.GetComponentsInChildren<WeaponController>();
		foreach(WeaponController w in weapons)
		{
			if(w.Default)
				weapon = w;
		}
		if(weapon==null)
			weapon = weapons[0];
		weaponName = weapon.name;

		//planet = GetComponentInParent<PlanetController>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMoveBarrel(Vector2 direction)
	{
		Barrel.SetDirection(direction);
	}

	public void OnFire(Vector2 direction, bool alt)
	{
		MissileController p = alt ? Weapon.AltMissilePrefab : Weapon.MainMissilePrefab;
		MissileController mc = p.PrefabInstantiate(
			(Vector2)transform.position+direction.normalized*3,
			Damage,
			direction*p.FirePower);


		mc.SendMessage("OnFire", direction);

	}

	public void RandomWeapon()
	{
		WeaponController[] weapons = WeaponsList.GetComponentsInChildren<WeaponController>();

		if(weapons==null || weapons.Length==0)
			return;

		if(weapons.Length==1)
		{
			weapon = weapons[0];
			weaponName = weapon.name;
			return;
		}

		if(weapons.Length==2)
		{
			weapon = Weapon==weapons[0]?weapons[1]:weapons[0];
			weaponName = weapon.name;
			return;
		}

		int count=0;
		int i = Random.Range(0,weapons.Length-1);
		while(Weapon==weapons[i] || weapons[i].Default)
		{
			i++;
			if(i>=weapons.Length)
				i=0;
			count++;
			if(count>weapons.Length+1)
				throw new UnityException("Error in weapons list");
		}

		weapon = weapons[i];
		weaponName = weapon.name;

	}
}
