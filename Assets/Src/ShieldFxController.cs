using UnityEngine;
using System.Collections;

public class ShieldFxController : MonoBehaviour {
	float phase = 0;
	public float PhaseSpeed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		phase+=Time.deltaTime*PhaseSpeed;
		if(phase>1000)
			phase-=1000;
		renderer.material.SetFloat("_Phase",phase);
	}
}
