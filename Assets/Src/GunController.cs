using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public MissileController MissilePrefab;
	public BarrelController Barrel;
	public float FireStrength = 100;
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
		mc.transform.position = (Vector2)transform.position+direction.normalized*3;
		mc.rigidbody2D.AddForce(direction*FireStrength,ForceMode2D.Impulse);
		mc.SendMessage("OnFire", direction);
	}
}
