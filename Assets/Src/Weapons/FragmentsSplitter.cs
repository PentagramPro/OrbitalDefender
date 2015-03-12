using UnityEngine;
using System.Collections;

public class FragmentsSplitter : MonoBehaviour {

	public MissileController SplitsPrefab;
	public float Damage = 10;
	public int Count = 1;
	public float Angle = 10;
	public float Shift = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLaunchFragments(MissileController.FragmentsArg arg)
	{
		if(arg.cause!=null)
			return;
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		Vector2 p = r.velocity*r.mass, p2=p;
		Vector3 shift = p.normalized*Shift;
		MissileController mc = SplitsPrefab.PrefabInstantiate(transform.position+shift,Damage,p);
		mc.transform.rotation = transform.rotation;
		for(int i=0;i<Count;i++)
		{
			p = p.Rotate(Angle);
			p2 = p2.Rotate(-Angle);
			shift = p.normalized*Shift;
			mc = SplitsPrefab.PrefabInstantiate(transform.position+shift,Damage,p);
			mc.transform.rotation = transform.rotation;

			shift = p2.normalized*Shift;
			mc = SplitsPrefab.PrefabInstantiate(transform.position+shift,Damage,p2);
			mc.transform.rotation = transform.rotation;
		}
	}
}
