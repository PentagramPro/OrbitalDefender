using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DirectionIndicatorController : MonoBehaviour {

	Image image;
	RectTransform rt;
	float fullH = 1;
	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		image = GetComponent<Image>();
		fullH = rt.rect.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool Visible{
		set{
			image.enabled = value;
		}
	}
	public void SetOrientation(Vector2 pt1, Vector2 pt2)
	{
		Vector2 dir = pt2-pt1;
		rt.position = (pt1+pt2)/2;
		float a = Vector2.Angle(dir,new Vector2(-1,0))-90;
		rt.rotation = Quaternion.Euler(0,0,a);
		rt.localScale = new Vector2(1,dir.magnitude/fullH);
		//Debug.Log("New:"+rt.rotation.eulerAngles);
	}
}
