using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StageController))]
public class ActivationStage : MonoBehaviour {

	public GameObject Target;
	public EnemyGeneratorController Generator {get;internal set;}
	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		Generator = GetComponentInParent<EnemyGeneratorController>();

		Target.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
		if(Target==null)
			Generator.OnStageComplete();
	}

	public void ActivateStage()
	{
		//Generator = gen;

		gameObject.SetActive(true);


	}
}
