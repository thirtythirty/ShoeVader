using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Score : MonoBehaviour {
	public static int player1_score = 0;
	public static int player2_score = 0;
	private int sum_score;
	private GameObject canvas;
	private Text scoreText;


	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void init(){
		sum_score = 0;
		player1_score = 0;
		player2_score = 0;
		canvas = transform.FindChild ("Canvas").gameObject;
		scoreText = canvas.transform.FindChild("ScoreData").gameObject.transform.GetComponentInChildren<Text>();
		ScoreUpdate ();

	}

	public void AddPoint(int point, int player_num){
		if (player_num == 1) {
			player1_score += point;
		} else if (player_num == 2) {
			player2_score += point;
		}
		ScoreUpdate ();
	}

	private void ScoreUpdate(){
		sum_score = player1_score + player2_score;
		scoreText.text = "Score " + string.Format("{0:D6}",sum_score);
	}
}