﻿using UnityEngine;
using System.Collections.Generic;

public class MultiplierController : MonoBehaviour {


	public BonusBallController BonusBallPrefab;

	public MultiplierIndicatorController Indicator;
	Dictionary<int,Bonus> bonuses = new Dictionary<int, Bonus>();
	PlanetController planet;

	void Awake(){
		planet = GetComponent<PlanetController>();
		bonuses[2] = Bonus.Shield;
		bonuses[10] = Bonus.Shield;
		bonuses[20] = Bonus.Shield;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ApplyBonus(Bonus b)
	{

		switch(b)
		{
		case Bonus.Shield:
			BonusBallPrefab.Instantiate(Indicator.WorldPosition,planet.transform,b);
			break;
		}
	}

	public void AddMultiplier()
	{
		int m = Indicator.IncrementMultiplier();
		if(bonuses.ContainsKey(m))
		{
			ApplyBonus(bonuses[m]);
		}
	}

	public void ResetMultiplier()
	{
		Indicator.ResetMultiplier();
	}
}