using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public NumericFieldController ScoreDisplay;
	public Toggle AdToggle;
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


	public void OnShowAdChanged(bool val)
	{
		PlayerPrefs.SetInt(AdController.PrefShowBanner,val?1:0);
	}

	// Use this for initialization
	void Start () {
		//string path = "Assets/Resources/Prefabs/Ships/Ship1.prefab";
		
		//GameObject prefab = (GameObject)Resources.LoadAssetAtPath(path,typeof(GameObject));
		//Debug.Log("path: "+path+", val="+(prefab==null));

		if(AdToggle!=null)
		{
			AdToggle.isOn = !(PlayerPrefs.HasKey(AdController.PrefShowBanner) 
			                  && PlayerPrefs.GetInt(AdController.PrefShowBanner)==0);
		}

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
