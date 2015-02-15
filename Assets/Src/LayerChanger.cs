using UnityEngine;
using System.Collections;

public class LayerChanger : MonoBehaviour {


	public ParticleSystem Particles;
	public string LayerName;
	public int SortingOrder = 0;


	// Use this for initialization
	void Awake () {
		if(renderer!=null)
		{

			Particles.renderer.sortingLayerName = LayerName;
			Particles.renderer.sortingOrder = SortingOrder;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
