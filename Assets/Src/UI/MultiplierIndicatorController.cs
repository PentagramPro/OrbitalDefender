using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplierIndicatorController : MonoBehaviour {
	Text text;
	Animator animator;
	int multiplier = 0;

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

	public Vector3 WorldPosition{
		get{
			return Camera.main.ScreenToWorldPoint(transform.position);
		}
	}
	public void ResetMultiplier()
	{
		multiplier = 0;
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
