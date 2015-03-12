using UnityEngine;
using System.Collections.Generic;

public class GunController : MonoBehaviour {

	public MissileController MissilePrefab;
	public BarrelController Barrel;
	public float FireStrength = 100;
	public float Damage = 10;

	public List<MissileController> Weapons;
	//PlanetController planet;

	void Awake()
	{
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

	public void OnFire(Vector2 direction)
	{

		MissileController mc = MissilePrefab.PrefabInstantiate(
			(Vector2)transform.position+direction.normalized*3,
			Damage,
			direction*FireStrength);


		mc.SendMessage("OnFire", direction);

	}

	public void RandomWeapon()
	{
		if(Weapons==null || Weapons.Count==0)
			return;

		if(Weapons.Count==1)
		{
			MissilePrefab = Weapons[0];
			return;
		}

		int i = Random.Range(0,Weapons.Count-1);
		if(MissilePrefab==Weapons[i])
			i++;
		if(i>=Weapons.Count)
			i=0;
		MissilePrefab = Weapons[i];

	}
}
