using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public UIController UI;

	public float MaxGravity=35, MaxDistance=500;
	public int ShieldBonus = 3;

	HullController hull;
	public int EnemyShips{get;set;}

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
		UI.HpBar.HP = hull.MaxHp;
	}
	// Use this for initialization
	void Start () {


	}

	void OnBonus(Bonus type)
	{
		if(type==Bonus.Shield)
		{
			hull.ShieldPower = ShieldBonus;
		}
	}

	void OnShieldDestroyed(float amount)
	{
		
	}

	void OnHullDestroyed(float amount)
	{

	}

	void OnHullDamaged(float amount) 
	{

		UI.HpBar.HP = hull.Hp;

	}

	public float CalculateGravity(float distance)
	{
		return Mathf.Max((1.0f - distance / MaxDistance) * MaxGravity,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEnemyDestroyed(int score)
	{
		UI.AddScore(score);
		multiplier.AddMultiplier();
	}
}
