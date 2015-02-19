using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public string GameSceneName = "game";
	public string MenuSceneName = "menu";

	public void OnNewGame()
	{
		Application.LoadLevel(GameSceneName);
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
