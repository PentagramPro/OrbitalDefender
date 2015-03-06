using UnityEngine;
using System.Collections.Generic;

public class BossController : MonoBehaviour {


	public List<BossPhase> Phases;
	
	int phase = 0;

	int turretsDestroyed = 0;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTurretDestroyed()
	{
		BossPhase p = Phases[phase];
		turretsDestroyed++;
		if(turretsDestroyed==p.Turrets.Count)
		{
			if(p.TriggerName!="")
			{
				animator.SetTrigger(p.TriggerName);
				phase++;
			}
		}
	}

	public void OnActivatePhase()
	{
		if(phase>=Phases.Count)
			return;
		BossPhase p = Phases[phase];
		turretsDestroyed = 0;
		foreach(TurretController t in p.Turrets)
			t.ActivateTurret();
	}


}
