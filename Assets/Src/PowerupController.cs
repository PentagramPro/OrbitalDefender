using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

	public BonusBallController BonusBall;
	public Bonus BonusType;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnExploded()
	{

		GameObject.Destroy(gameObject);
	}

	public void OnMissileCollision(MissileController missile)
	{
		GameObject p = GameObject.FindGameObjectWithTag("Planet");
		BonusBall.Instantiate(transform.position,p.transform,BonusType);
		animator.SetTrigger("Explode");

	}
}
