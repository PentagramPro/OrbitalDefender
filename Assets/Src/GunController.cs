using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public MissileController MissilePrefab;
	public BarrelController Barrel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMoveBarrel(Vector2 direction)
	{
		Barrel.SetDirection(direction);
	}

	public void OnFire(Vector2 direction)
	{
		GameObject go = GameObject.Instantiate(MissilePrefab.gameObject) as GameObject;
		MissileController mc = go.GetComponent<MissileController>();
		mc.transform.position = transform.position;
		mc.rigidbody2D.AddForce(direction*200,ForceMode2D.Impulse);

	}
}
