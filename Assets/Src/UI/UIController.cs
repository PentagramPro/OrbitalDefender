using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	enum Modes {Playing, Menu}
	public MessageController MsgController;
	public NumericFieldController Score;
	public HpBarController HpBar;

	public MenuController Menu;

	Modes state = Modes.Playing;

	// Use this for initialization
	void Start () {
		Menu.gameObject.SetActive(false);
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
