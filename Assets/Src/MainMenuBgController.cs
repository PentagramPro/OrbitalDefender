using UnityEngine;
using System.Collections;

public class MainMenuBgController : MonoBehaviour {

	public float ScrollSpeed = 0.2f;
	float offset = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		offset+=ScrollSpeed*Time.deltaTime;
		renderer.material.mainTextureOffset = new Vector2(0,offset);
	}
}
