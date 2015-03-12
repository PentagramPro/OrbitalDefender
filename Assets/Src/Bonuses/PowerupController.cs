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

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.tag=="Planet")
		{
			StartDestruction();
		}
	}

	public void OnMissileCollision(MissileController missile)
	{

		StartDestruction();
	}


	void StartDestruction()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Planet");
		BonusBall.Instantiate(transform.position,p.transform,BonusType);
		animator.SetTrigger("Explode");
	}
}
