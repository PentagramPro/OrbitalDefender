using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplierIndicatorController : MonoBehaviour {
	Text text;
	Animator animator;
	int multiplier = 1;
	public Animator BonusGlow;
	public Color ShieldGlowColor;

	void Awake()
	{
		text = GetComponent<Text>();
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int Value
	{
		get{
			return multiplier;
		}
	}

	public void BonusEffect(Bonus b)
	{
		switch(b)
		{
		case Bonus.Shield:
			var img = BonusGlow.GetComponent<Image>();
			img.color = ShieldGlowColor;
			BonusGlow.SetTrigger("Tick");
			break;
		}
	}

	public Vector3 WorldPosition{
		get{
			return Camera.main.ScreenToWorldPoint(transform.position);
		}
	}
	public void ResetMultiplier()
	{
		multiplier = 1;
		UpdateText();
	}

	public int IncrementMultiplier()
	{
		multiplier++;
		animator.SetTrigger("Tick");
		UpdateText();
		return multiplier;
	}

	void UpdateText()
	{
		if(multiplier<=0)
			text.enabled = false;
		else
		{
			if(text.enabled==false)
				text.enabled = true;
			text.text = "x"+multiplier;
		}
	}
}
