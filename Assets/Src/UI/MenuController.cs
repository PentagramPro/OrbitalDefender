using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public NumericFieldController ScoreDisplay;

	public string GameSceneName = "game";
	public string MenuSceneName = "menu";

	public void OnNewGame()
	{
		Application.LoadLevel(GameSceneName);
	}

	public void OnContinueGame()
	{
		Debug.Log("Resuming");
	
		SaveLoad.Resume();
		//LevelSerializer.Resume();
	}

	public void OnExitToMenu()
	{
		Application.LoadLevel(MenuSceneName);
	}



	// Use this for initialization
	void Start () {
		//string path = "Assets/Resources/Prefabs/Ships/Ship1.prefab";
		
		//GameObject prefab = (GameObject)Resources.LoadAssetAtPath(path,typeof(GameObject));
		//Debug.Log("path: "+path+", val="+(prefab==null));

		if(ScoreDisplay!=null)
		{
			if(PlayerPrefs.HasKey(UIController.RecordPrefName))
			{
				ScoreDisplay.Value = PlayerPrefs.GetInt(UIController.RecordPrefName);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
