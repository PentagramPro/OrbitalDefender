using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	enum MenuParameter{None,Resume}
	MenuParameter parameter = MenuParameter.None;

	public string GameSceneName = "game";
	public string MenuSceneName = "menu";

	public void OnNewGame()
	{
		Application.LoadLevel(GameSceneName);
	}

	public void OnContinueGame()
	{
		Debug.Log("Resuming");
		parameter = MenuParameter.Resume;
		//LevelSerializer.Resume();
	}

	public void OnExitToMenu()
	{
		Application.LoadLevel(MenuSceneName);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
