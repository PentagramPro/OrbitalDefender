using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public float MinSize=10,MaxSize=100;
	public float ZoomSpeed=50;
	float targetSize;
	// Use this for initialization
	void Start () {
		camera.orthographicSize = MinSize;
		targetSize = MinSize;

	}


	
	// Update is called once per frame
	void Update () {

		if(targetSize!=camera.orthographicSize)
		{
			if(targetSize>camera.orthographicSize)
			{
				camera.orthographicSize+=ZoomSpeed*Time.smoothDeltaTime;
				if(camera.orthographicSize>targetSize)
					camera.orthographicSize = targetSize;
			}
			else
			{
				camera.orthographicSize-=ZoomSpeed*Time.smoothDeltaTime;
				if(camera.orthographicSize<targetSize)
					camera.orthographicSize = targetSize;
			}
		}
	}

	public void OnChangeZoom(float val)
	{
		float w =  camera.orthographicSize*camera.aspect;
		float h =  camera.orthographicSize;
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
