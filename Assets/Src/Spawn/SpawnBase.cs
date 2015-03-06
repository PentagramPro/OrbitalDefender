using UnityEngine;
using System.Collections;

public class SpawnBase : MonoBehaviour {

	public float MinOrbit = 20,MaxOrbit = 100;
	

	
	
	protected SpawnerStage stage;
	
	void Awake()
	{
		stage = GetComponent<SpawnerStage>();
	}

	void OnDrawGizmosSelected()
	{
		
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Vector3.zero,MinOrbit);
		Gizmos.DrawWireSphere(Vector3.zero,MaxOrbit);
		
	}


}
