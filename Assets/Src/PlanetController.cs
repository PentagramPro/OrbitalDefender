using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public UIController UI;

	public float MaxGravity=35, MaxDistance=500;
	public float MaxHP = 100;
	float curHP;

	public int EnemyShips{get;set;}

	void Awake()
	{
		EnemyShips = 0;
		curHP = MaxHP;
		UI.HpBar.HP = MaxHP;
	}
	// Use this for initialization
	void Start () {


	}

	void OnFireball(FireballController fireball) 
	{

		DamagePlanet(fireball.Damage);

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
	}

	public void DamagePlanet(float amount)
	{
		curHP-=amount;
		UI.HpBar.HP = curHP;
	}
}
