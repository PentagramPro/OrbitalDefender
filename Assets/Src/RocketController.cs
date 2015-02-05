using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	enum Modes {Engine,Fly}
	Modes state = Modes.Engine;
	public float MaxTime = 4;
	public float Power = 100;
	public Vector2 EnginePos;
	public GameObject TrailEffect;

	float CurTime = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(state==Modes.Engine)
		{
			Vector2 pos = transform.TransformPoint(EnginePos);
			Vector2 force = transform.TransformDirection(0,Power,0);
			rigidbody2D.AddForceAtPosition(force,pos);
			CurTime+=Time.fixedDeltaTime;
			if(CurTime>=MaxTime)
			{
				state = Modes.Fly;
				TrailEffect.SetActive(false);
			}
		}
	}

	public void OnFire(Vector2 direction)
	{
		MaxTime*=direction.magnitude;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;
		transform.rotation =  Quaternion.Euler(0,0,angle);
		CurTime = 0;
		state = Modes.Engine;

	}

	void OnDrawGizmos()
	{
		Vector2 pos = transform.TransformPoint(EnginePos);
		Vector2 force = transform.TransformDirection(0,Power,0);
		Gizmos.DrawWireSphere(pos,1f);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(pos,pos+force);
	}
}
