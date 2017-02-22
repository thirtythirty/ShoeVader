using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestScore : MonoBehaviour {

	private GameObject canvas;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		canvas = transform.FindChild ("Canvas").gameObject;
		scoreText = canvas.transform.FindChild("ScoreData").gameObject.transform.GetComponentInChildren<Text>();
		var sum_score = Score.player1_score + Score.player2_score;
		scoreText.text = "Score " + string.Format("{0:D6}",sum_score);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
