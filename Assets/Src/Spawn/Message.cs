using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour, ISpawner {

	public string Text;

	protected SpawnerStage stage;
	
	void Awake()
	{
		stage = GetComponent<SpawnerStage>();
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
		stage.Generator.UI.BigMessage = Text;
		Debug.Log("Msg: "+Text);
	}




	public bool CanSpawn ()
	{
		return true;
	}


	#endregion
}
