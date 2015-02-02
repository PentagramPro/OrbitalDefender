using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDirection(Vector2 dir)
	{
		transform.rotation = Quaternion.Euler(0,0,Vector2.Angle(dir,new Vector2(1,0)));
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,1f);
	}
}
