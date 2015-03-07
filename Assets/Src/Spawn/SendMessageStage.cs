using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StageController))]
public class SendMessageStage : MonoBehaviour {

	public GameObject Target;
	public string MessageName;
	public EnemyGeneratorController Generator {get;internal set;}
	// Use this for initialization
	void Start () {
		
	}
	
	void Awake()
	{
		Generator = GetComponentInParent<EnemyGeneratorController>();
		

	}
	
	// Update is called once per frame
	void Update () {
		Target.SendMessage(MessageName, SendMessageOptions.DontRequireReceiver);
		Generator.OnStageComplete();
		gameObject.SetActive(false);
	}
}
