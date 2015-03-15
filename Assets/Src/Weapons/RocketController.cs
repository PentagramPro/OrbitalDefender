using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	enum Modes {Delay,Engine,Fly}
	Modes state = Modes.Delay;
	public float DelayTime = 1;
	public float DelayTorqueMult = 1;
	public float EnginesTime = 4;
	public float EnginesTorqueMult = 0;

	public float Power = 100;
	public Vector2 EnginePos;


	MissileController missile;
	float BaseTorque = 0;

	CountTime Counter = new CountTime();
	// Use this for initialization
	void Start () {
		missile = GetComponent<MissileController>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {




		Rigidbody2D r = GetComponent<Rigidbody2D>();
		Vector2 pos = transform.TransformPoint(EnginePos);
		Vector2 force = transform.TransformDirection(0,Power*r.mass,0);

		switch(state)
		{
		case Modes.Delay:
			//r.AddForceAtPosition(force,pos);
			r.AddTorque(GetTorque()*r.mass*DelayTorqueMult);


			if(Counter.Count(DelayTime))
			{
				state = Modes.Engine;
				missile.TrailEffect.gameObject.SetActive(true);
				r.angularVelocity = 0;
				Counter.Reset();
			}
			break;
		case Modes.Engine:
			r.AddForceAtPosition(force,pos);
			r.AddTorque(GetTorque()*r.mass*EnginesTorqueMult);
			

			if(Counter.Count(EnginesTime))
			{
				state = Modes.Fly;
				Counter.Reset();
				missile.DetachTrail();
			}
			break;
		case Modes.Fly:

			break;
		}


	}

	public void OnFire(Vector2 direction)
	{
		missile = GetComponent<MissileController>();
		EnginesTime*=direction.magnitude;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;
		transform.rotation =  Quaternion.Euler(0,0,angle);

		BaseTorque = -direction.x;


		missile.TrailEffect.gameObject.SetActive(false);
	}

	public float VelocityAngle
	{
		get{
			Vector2 velocity = transform.InverseTransformDirection(GetComponent<Rigidbody2D>().velocity.normalized);
			float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg-90;
			return angle;
		}
	}
	float GetTorque()
	{

		//return VelocityAngle*0.01f;
		return BaseTorque;
	}
	void OnDrawGizmos()
	{
		Vector2 pos = transform.TransformPoint(EnginePos);
		Vector2 force = transform.TransformDirection(0,Power,0);
		Gizmos.DrawWireSphere(pos,1f);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(pos,pos+force);

		Gizmos.color = Color.green;
		Gizmos.DrawLine(pos,pos+GetComponent<Rigidbody2D>().velocity);
	}
}
