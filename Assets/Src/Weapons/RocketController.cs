﻿using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	enum Modes {Engine,Fly}
	Modes state = Modes.Engine;
	public float MaxTime = 4;
	public float Power = 100;
	public Vector2 EnginePos;
	public float TorqueMult = 1;
	MissileController missile;
	float BaseTorque = 0;

	float CurTime = 0;
	// Use this for initialization
	void Start () {
		missile = GetComponent<MissileController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(state==Modes.Engine)
		{
			Rigidbody2D r = GetComponent<Rigidbody2D>();
			Vector2 pos = transform.TransformPoint(EnginePos);
			Vector2 force = transform.TransformDirection(0,Power*r.mass,0);


			r.AddForceAtPosition(force,pos);



			r.AddTorque(GetTorque()*r.mass);

			CurTime+=Time.fixedDeltaTime;
			if(CurTime>=MaxTime)
			{
				state = Modes.Fly;
				//TrailEffect.SetActive(false);
				missile.DetachTrail();
			}
		}
	}

	public void OnFire(Vector2 direction)
	{
		MaxTime*=direction.magnitude;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;
		transform.rotation =  Quaternion.Euler(0,0,angle);
		CurTime = 0;
		BaseTorque = -direction.x;
		state = Modes.Engine;

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
		return BaseTorque*TorqueMult;
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