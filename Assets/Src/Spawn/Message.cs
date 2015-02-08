using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour, ISpawner {

	public string Text;

	protected Stage stage;
	
	void Awake()
	{
		stage = GetComponent<Stage>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ISpawner implementation

	public void NextSpawn ()
	{
		stage.Generator.MsgController.DisplayMessage(Text);
		Debug.Log("Msg: "+Text);
	}

	#endregion
}
