﻿using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public UIController UI;

	public float MaxGravity=35, MaxDistance=500;
	public int ShieldBonus = 3;
	public float HpBonus = 30;

	HullController hull;
	GunController gun;
	public GunController Gun{
		get{
			return gun;
		}
	}

	int enemyShips = 0;
	public int EnemyShips{get{
			return enemyShips;
		}
		set{
			enemyShips = Mathf.Max(0,value);
		}}
	public int Asteroids{get;set;}

	MultiplierController multiplier;
	public MultiplierController Multiplier
	{
		get{
			return multiplier;
		}
	}

	void Awake()
	{
		EnemyShips = 0;
		hull = GetComponent<HullController>();
		multiplier = GetComponent<MultiplierController>();
		gun = GetComponentInChildren<GunController>();
		UI.HpBar.HP = hull.MaxHp;
	}
	// Use this for initialization
	void Start () {


	}

	public void OnMissileCollision(MissileController missile)
	{
		if(missile.ResetMultiplier)
			Multiplier.ResetMultiplier();
	}

	void OnBonus(Bonus type)
	{
		switch(type)
		{
		case Bonus.Hp:
			hull.AddHp(HpBonus);
			UI.HpBar.HP = hull.Hp;
			break;
		case Bonus.Shield:
			hull.ShieldPower = ShieldBonus;
			break;
		case Bonus.Weapon:
			gun.RandomWeapon();
			break;
		}

	}

	void OnShieldDestroyed(float amount)
	{
		
	}

	public void OnVictory()
	{
		if(this.enabled)
		{
			UI.ActiveArea.gameObject.SetActive(false);
			UI.OnVictory();
			this.enabled = false;
		}
	}

	void OnHullDestroyed(float amount)
	{
		if(this.enabled)
		{
			UI.HpBar.HP = 0;
			UI.ActiveArea.gameObject.SetActive(false);
			UI.OnGameOver();
			this.enabled = false;
		}

	}

	void OnHullDamaged(float amount) 
	{

		UI.HpBar.HP = hull.Hp;

	}

	public float CalculateGravity(float distance, float mass)
	{
		return Mathf.Max((1.0f - distance / MaxDistance) * MaxGravity*mass,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnEnemyDestroyed(int score)
	{
		UI.AddScore(score*multiplier.Value);
		multiplier.AddMultiplier();
	}
}
