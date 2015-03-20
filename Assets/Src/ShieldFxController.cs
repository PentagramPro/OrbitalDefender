using UnityEngine;
using System.Collections;

public class ShieldFxController : MonoBehaviour {


	float phase = 0;
	public float PhaseSpeed = 5;
	public Animator ShieldAnimator;
	public SpriteRenderer ShieldRenderer;

	bool shieldEnabled = false;

	public bool ShieldEnabled{
		get{
			return shieldEnabled;
		}
		set{
			if(value && !shieldEnabled)
			{
				ShieldAnimator.SetTrigger("Enable");
			}
			else if(!value && shieldEnabled)
				ShieldAnimator.SetTrigger("Disable");

			shieldEnabled = value;
		}
	}

	public void OnDamageRecieved()
	{
		ShieldAnimator.SetTrigger("Damage");
	}


	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
	
	}



	// Update is called once per frame
	void Update () {
		UpdatePhase();

	}

	void UpdatePhase()
	{
		phase+=Time.deltaTime*PhaseSpeed;
		if(phase>1000)
			phase-=1000;
		ShieldRenderer.material.SetFloat("_Phase",phase);
	}
}
