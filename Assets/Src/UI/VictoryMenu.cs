using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryMenu : MonoBehaviour {

	public Text ScoreText;

	public void SetScore(int score, int record)
	{
		if(score<=record)
		{
			ScoreText.text = string.Format("Your score: {0}\nRecord: {1}",score,record);
		}
		else
		{
			ScoreText.text = string.Format("New record: {0}",score);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
