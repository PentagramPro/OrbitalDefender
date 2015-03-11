using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public float MinSize=10,MaxSize=100;
	public float ZoomSpeed=50;
	float targetSize;
	// Use this for initialization
	void Start () {
		GetComponent<Camera>().orthographicSize = MinSize;
		targetSize = MinSize;

	}


	
	// Update is called once per frame
	void Update () {

		if(targetSize!=GetComponent<Camera>().orthographicSize)
		{
			if(targetSize>GetComponent<Camera>().orthographicSize)
			{
				GetComponent<Camera>().orthographicSize+=ZoomSpeed*Time.smoothDeltaTime;
				if(GetComponent<Camera>().orthographicSize>targetSize)
					GetComponent<Camera>().orthographicSize = targetSize;
			}
			else
			{
				GetComponent<Camera>().orthographicSize-=ZoomSpeed*Time.smoothDeltaTime;
				if(GetComponent<Camera>().orthographicSize<targetSize)
					GetComponent<Camera>().orthographicSize = targetSize;
			}
		}
	}

	public void OnChangeZoom(float val)
	{
		float w =  GetComponent<Camera>().orthographicSize*GetComponent<Camera>().aspect;
		float h =  GetComponent<Camera>().orthographicSize;
		float calcMax = MaxSize;
		if(w<h)
			calcMax+=h-w;
		targetSize = MinSize+(calcMax-MinSize)*val;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position,MinSize);
		Gizmos.DrawWireSphere(transform.position,MaxSize);
	}
}
