using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public UIController UI;

	public float MaxGravity=35, MaxDistance=500;

	HullController hull;
	public int EnemyShips{get;set;}

	void Awake()
	{
		EnemyShips = 0;
		hull = GetComponent<HullController>();
		UI.HpBar.HP = hull.MaxHp;
	}
	// Use this for initialization
	void Start () {


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
	}
}
