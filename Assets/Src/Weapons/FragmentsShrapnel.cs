using UnityEngine;
using System.Collections;

public class FragmentsShrapnel : MonoBehaviour {

	public MissileController ShrapnelPrefab;
	public float Damage = 5;
	public float Impulse = 10;
	public int Fragments = 3;
	public float SpawnShift = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLaunchFragments(MissileController.FragmentsArg arg)
	{

		Vector2 dir = Random.insideUnitCircle.normalized;
		for(int i=0;i<Fragments;i++)
		{
			MissileController mc = ShrapnelPrefab.PrefabInstantiate(
				transform.position,
				Damage,
				dir*Impulse);

			mc.transform.position+=(Vector3)dir*SpawnShift;
			if(arg.cause!=null)
				Physics2D.IgnoreCollision(mc.GetComponent<Collider2D>(),arg.cause.GetComponent<Collider2D>());

			dir = dir.Rotate(360/Fragments);
		}

	}
}
