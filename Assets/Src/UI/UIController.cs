using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	public MessageController MsgController;
	public NumericFieldController Score;
	public HpBarController HpBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
