using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public string SortingLayerName = "Background";
	public int SortingOrder = 0;
	public float parallax = 0;

	Vector2 baseScale;
	Vector2 baseOffset;

	// Use this for initialization
	void Start () {
		MeshRenderer r = GetComponent<MeshRenderer>();
		r.sortingLayerName = SortingLayerName;
		r.sortingOrder = SortingOrder;
		baseScale = GetComponent<Renderer>().material.mainTextureScale;
		baseOffset = GetComponent<Renderer>().material.mainTextureOffset;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 delta = new Vector2(1,1)*parallax*Camera.main.orthographicSize;
		GetComponent<Renderer>().material.mainTextureScale = baseScale+delta;
		GetComponent<Renderer>().material.mainTextureOffset = baseOffset-delta/2;
	}
}
