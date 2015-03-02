using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	public static string RecordPrefName = "ScoreRecord";

	enum Modes {Playing, Menu, Gameover}
	public MessageController MsgController;
	public NumericFieldController Score;
	public HpBarController HpBar;

	public MenuController Menu;
	public GameoverMenu Gameover;
	public ActiveAreaController ActiveArea;

	Modes state = Modes.Playing;

	// Use this for initialization
	void Start () {
		Menu.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape") && state==Modes.Playing)
		{
			state = Modes.Menu;
			Time.timeScale = 0;
			Menu.gameObject.SetActive(true);
		}

	}

	public void OnGameOver()
	{
		state = Modes.Gameover;
		//Time.timeScale = 0;
		Gameover.gameObject.SetActive(true);

		int record = PlayerPrefs.GetInt(RecordPrefName);
		int score = Score.Value;

		Gameover.SetScore(score,record);

		if(score>record)
			PlayerPrefs.SetInt(RecordPrefName,score);
	}

	public void SaveGame()
	{
		SaveLoad.Checkpoint();
	}

	void OnApplicationPause()
	{
		Debug.Log("Saving");
		//LevelSerializer.Checkpoint();
		SaveGame();
	}

	public void OnContinuePlaying()
	{
		if(state==Modes.Menu)
		{
			state = Modes.Playing;
			Time.timeScale = 1;
			Menu.gameObject.SetActive(false);
		}
	}

	public string BigMessage{
		set{
			MsgController.DisplayMessage(value);
		}
	}

	public void AddScore(int toAdd)
	{
		Score.AddValue(toAdd);
	}
}
