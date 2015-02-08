using UnityEngine;
using System.Collections;

public class SpawnBase : MonoBehaviour {

	public float MinOrbit = 20,MaxOrbit = 100;
	

	
	
	protected Stage stage;
	
	void Awake()
	{
		stage = GetComponent<Stage>();
	}

	void OnDrawGizmosSelected()
	{
		
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Vector3.zero,MinOrbit);
		Gizmos.DrawWireSphere(Vector3.zero,MaxOrbit);
		
	}


}
