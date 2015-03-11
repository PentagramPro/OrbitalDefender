using UnityEngine;
using System.Collections;

public class TurretController: MonoBehaviour {

	PeriodicFireController fireControlller;
	BossController Boss;
	// Use this for initialization
	void Start () {
		fireControlller = GetComponent<PeriodicFireController>();
		GetComponent<Collider2D>().enabled = false;
		Boss = GetComponent<BossController>();
		if(Boss==null)
			Boss = GetComponentInParent<BossController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnHullDestroyed(float amount)
	{
		GetComponent<Animator>().SetTrigger("Explode");
		gameObject.SendMessage("StopFire",SendMessageOptions.DontRequireReceiver);

	}


	public void ActivateTurret()
	{
		GetComponent<Animator>().SetTrigger("Init");

	}

	public void OnTurretExploded()
	{
		GameObject.Destroy(gameObject);
		Boss.OnTurretDestroyed();
	}

	public void OnTurretInitiated()
	{
		GetComponent<Collider2D>().enabled = true;
		gameObject.SendMessage("StartFire",SendMessageOptions.DontRequireReceiver);
	}
}
