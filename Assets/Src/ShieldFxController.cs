using UnityEngine;
using System.Collections;

public class ShieldFxController : MonoBehaviour {
	enum Modes{ Enabled,Disabled,Enabling,Disabling }

	float phase = 0;
	public float PhaseSpeed = 5;
	public Animator ShieldAnimator;
	public SpriteRenderer ShieldRenderer;

	Modes _state = Modes.Disabled;
	Modes state {
		get{
			return _state;
		}
		set{
			_state = value;
		}
	}

	float curStateDuration = 0;
	float counter = 0;
	public bool ShieldEnabled{
		get{
			return state==Modes.Enabled || state==Modes.Enabling;
		}
		set{
			if(value)
			{
				if(state==Modes.Disabled || state==Modes.Disabling)
				{
					state = Modes.Enabling;
					if(!ShieldAnimator.gameObject.activeSelf)
						ShieldAnimator.gameObject.SetActive(true);
					ShieldAnimator.SetTrigger("Enable");
					curStateDuration = ShieldAnimator.GetCurrentAnimatorStateInfo(0).length;
					counter = 0;
				}
			}
			else
			{
				if(state==Modes.Enabled || state==Modes.Enabling)
				{
					state = Modes.Disabling;
					ShieldAnimator.SetTrigger("Disable");
					curStateDuration = ShieldAnimator.GetCurrentAnimatorStateInfo(0).length;
					counter = 0;
				}
			}
		}
	}

	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
		if(ShieldEnabled==false)
		{
			ShieldAnimator.gameObject.SetActive(false);
		}
	}

	bool UpdateCounter(float duration)
	{
		counter+=Time.deltaTime;
		return counter>duration;
	}

	// Update is called once per frame
	void Update () {
		switch(state)
		{
		case Modes.Disabled:
			break;
		case Modes.Disabling:
			UpdatePhase();
			if(UpdateCounter(curStateDuration))
			{
				ShieldAnimator.gameObject.SetActive(false);
				state = Modes.Disabled;
			}
			break;
		case Modes.Enabled:
			UpdatePhase();
			break;
		case Modes.Enabling:
			UpdatePhase();
			if(UpdateCounter(curStateDuration))
			{
			
				state = Modes.Enabled;
			}
			break;
		}
	}

	void UpdatePhase()
	{
		phase+=Time.deltaTime*PhaseSpeed;
		if(phase>1000)
			phase-=1000;
		ShieldRenderer.material.SetFloat("_Phase",phase);
	}
}
