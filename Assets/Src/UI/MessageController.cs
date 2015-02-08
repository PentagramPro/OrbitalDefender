using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour {
	enum Modes{Shown, Hiding, Hidden}
	public float Delay = 3;
	public float HideDelay = 2;
	Animator animator;
	Text text;
	Modes state = Modes.Hidden;
	float counter = 0;
	void Awake()
	{
		animator = GetComponent<Animator>();
		text = GetComponent<Text>();
		text.enabled = false;
	}


	public void DisplayMessage(string msg)
	{
		text.text = msg;
		text.enabled = true;
		counter = 0;
		state = Modes.Shown;
		animator.SetTrigger("Show");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(state==Modes.Shown)
		{
			counter+=Time.deltaTime;
			if(counter>Delay)
			{
				state = Modes.Hiding;

				counter = 0;
				animator.SetTrigger("Hide");
			}
		}
		else if(state==Modes.Hiding)
		{
			counter+=Time.deltaTime;
			if(counter>HideDelay)
			{
				text.enabled = false;
			}
		}

	}
}
