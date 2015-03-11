using UnityEngine;
using System.Collections;

public class LayerChanger : MonoBehaviour {


	public ParticleSystem Particles;
	public string LayerName;
	public int SortingOrder = 0;


	// Use this for initialization
	void Awake () {
		if(GetComponent<Renderer>()!=null)
		{

			Particles.GetComponent<Renderer>().sortingLayerName = LayerName;
			Particles.GetComponent<Renderer>().sortingOrder = SortingOrder;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
