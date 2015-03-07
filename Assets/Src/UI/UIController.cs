using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	public static string RecordPrefName = "ScoreRecord";

	enum Modes {Playing, Menu, Gameover,Victory}
	public MessageController MsgController;
	public NumericFieldController Score;
	public HpBarController HpBar;

	public MenuController Menu;
	public GameoverMenu Gameover;
	public VictoryMenu VictoryMenu;
	public ActiveAreaController ActiveArea;

	Modes state = Modes.Playing;
	Vector3 hpBarDefPos;
	RectTransform hpBarRect;

	Canvas canvas;


	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
		hpBarRect  = HpBar.GetComponent<RectTransform>();
		hpBarDefPos = hpBarRect.position;
		Menu.gameObject.SetActive(false);
		Time.timeScale = 1;
	}


	public void ShiftTopBar(bool shift)
	{


		hpBarRect.position = hpBarDefPos-(shift?new Vector3(0,50*Screen.dpi/160f	,0):Vector3.zero);
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

	public void OnVictory()
	{
		state = Modes.Victory;
		VictoryMenu.gameObject.SetActive(true);

		int record = PlayerPrefs.GetInt(RecordPrefName);
		int score = Score.Value;
		
		VictoryMenu.SetScore(score,record);
		
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
