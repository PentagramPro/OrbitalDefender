using UnityEngine;
using System.Collections;

public class BonusBallController : MonoBehaviour {
	enum Modes{
		Charging, Flying, Exploding
	}
	public float ChargeTime=1,ExplodeTime=1;
	public float FlySpeed = 50;
	public Transform Target;
	public Bonus Type;

	Modes state = Modes.Charging;
	CountTime counter = new CountTime();


	void Awake()
	{
		TrailRenderer tr = GetComponent<TrailRenderer>();
		tr.sortingLayerName = "Ships";
		tr.sortingOrder = 100;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
		case Modes.Charging:
			if(counter.Count(ChargeTime))
				state = Modes.Flying;
			break;
		case Modes.Flying:
			float dist = (transform.position-Target.position).magnitude;
			float lerpSpeed = FlySpeed/dist;
			transform.position = Vector3.Lerp(transform.position,Target.position,lerpSpeed*Time.deltaTime);
			if(transform.position==Target.position)
			{
				counter.Reset();
				state = Modes.Exploding;
				Target.SendMessage("OnBonus",Type,SendMessageOptions.DontRequireReceiver);
			}
			break;
		case Modes.Exploding:
			if(counter.Count(ChargeTime))
				state = Modes.Exploding;
			break;
		}


	}

	public BonusBallController Instantiate(Vector3 startPos, Transform target, Bonus bonus)
	{


		BonusBallController sb = this.InstantiateFromComponent<BonusBallController>();
		sb.transform.position = startPos;
		sb.Type = bonus;
		sb.Target = target;
		return sb;
	}
}
