using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class ResultScore : MonoBehaviour {

	private GameObject canvas;
	private Text SumScoreText;
//	private Text DifficulityText;
	private Text Player1ScoreText,Player2ScoreText;
//	private Text RankingText;
	private Text RankText;
	private int[] ScoreRank;
	private int i,sumScore;
	private string line;
	//保存できるランキングの最大値
	private static int RankMax = 100;
	private Text firstScore;
	private Text secondScore;
	private Text thirdScore;
	private Text fourScore;
	private Text fiveScore;
	private Text RankingIN;
	private Text RankingNum;
	public GameObject Oukan1;
	public GameObject Oukan2;
	public GameObject difficulityImgSweet;
	public GameObject difficulityImgMild;
	public GameObject difficulityImgBitter;



	//Aラン~Dランへの区分けする変数(満点:100,000と仮定)
	private static int rankA = 80000;
	private static int rankB = 70000;
	private static int rankC = 60000;
	private static int rankD = 0;


	// Use this for initialization
	void Start () {
//		//ダミーデータ
//		Score.player1_score = 10000;
//		Score.player2_score = 40000;
		sumScore = Score.player1_score + Score.player2_score;
//		ModeSelecter.SelectedMode = 0;

		ScoreRank = new int[RankMax];
		canvas = transform.FindChild ("Canvas").gameObject;
		SumScoreText = canvas.transform.FindChild("SumScore").gameObject.transform.GetComponentInChildren<Text>();
//		DifficulityText = canvas.transform.FindChild("Difficulity").gameObject.transform.GetComponentInChildren<Text>();
		Player1ScoreText = canvas.transform.FindChild("Player1Score").gameObject.transform.GetComponentInChildren<Text>();
		Player2ScoreText = canvas.transform.FindChild("Player2Score").gameObject.transform.GetComponentInChildren<Text>();
//		RankingText = canvas.transform.FindChild("Ranking").gameObject.transform.GetComponentInChildren<Text>();
		RankText = canvas.transform.FindChild("Rank").gameObject.transform.GetComponentInChildren<Text>();
		firstScore = canvas.transform.FindChild("1st_Score").gameObject.transform.GetComponentInChildren<Text>();
		secondScore = canvas.transform.FindChild("2nd_Score").gameObject.transform.GetComponentInChildren<Text>();
		thirdScore = canvas.transform.FindChild("3rd_Score").gameObject.transform.GetComponentInChildren<Text>();
		fourScore = canvas.transform.FindChild("4th_Score").gameObject.transform.GetComponentInChildren<Text>();
		fiveScore = canvas.transform.FindChild("5th_Score").gameObject.transform.GetComponentInChildren<Text>();
		RankingIN = canvas.transform.FindChild("RankingIN").gameObject.transform.GetComponentInChildren<Text>();
		RankingNum = canvas.transform.FindChild("RankingNum").gameObject.transform.GetComponentInChildren<Text>();



		sumScore = Score.player1_score + Score.player2_score;
		Player1ScoreText.text = string.Format("{0:D6}",Score.player1_score);
		Player2ScoreText.text = string.Format("{0:D6}",Score.player2_score);
		SumScoreText.text =string.Format("{0:D6}",sumScore);
		//ランキング配列の初期化
		for(i = 0;i < RankMax;i++) {
			ScoreRank[i] = 0;
		}

		//難易度での表示,ランキングの変更
		if(ModeSelecter.SelectedMode == 0) {
			Instantiate (difficulityImgSweet);
//			DifficulityText.text = "Sweet";
			ReadFile("Sweet");
//			RankingSearch();
			SetOukan();
			SetText ();
			RankDivision();
//			RankingPrint();
			WriteFile("Sweet");
		} else if(ModeSelecter.SelectedMode == 1) {
			Instantiate (difficulityImgMild);
//			DifficulityText.text = "Mild";
			ReadFile("Mild");
//			RankingSearch();
			SetOukan();
			SetText ();
			RankDivision();
//			RankingPrint();
			WriteFile("Mild");
		} else if(ModeSelecter.SelectedMode == 2) {
			Instantiate (difficulityImgBitter);
//			DifficulityText.text = "Bitter";
			ReadFile("Bitter");
//			RankingSearch();
			SetOukan();
			SetText ();
			RankDivision();
//			RankingPrint();
			WriteFile("Bitter");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Q)) {
			FadeManager.Instance.LoadLevel("start",0.5f);
		}
	}

	void SetOukan(){
		if (Score.player1_score > Score.player2_score) {
			Instantiate (Oukan1);
		} else if (Score.player1_score < Score.player2_score) {
			Instantiate (Oukan2);
		} else {
			Instantiate (Oukan1);
			Instantiate (Oukan2);
		}
	}

	void SetText(){
		var rank = RankingSearch ();
		if (rank == 0) {
			RankingNum.text = (rank+1).ToString() + "st";
		} else if (rank == 1) {
			RankingNum.text = (rank+1).ToString() + "nd";
		} else if (rank == 2) {
			RankingNum.text = (rank+1).ToString() + "rd";
		} else {
			RankingNum.text = (rank+1).ToString() + "th";
		}
		if (rank < 5) {
			RankingIN.text = "Ranking IN!";
		} else {
			RankingIN.text = "Ranking Out,,,";
		}
		firstScore.text = ScoreRank [0].ToString ();
		secondScore.text = ScoreRank [1].ToString ();
		thirdScore.text = ScoreRank [2].ToString ();
		fourScore.text = ScoreRank [3].ToString ();
		fiveScore.text = ScoreRank [4].ToString ();
	}

	//ランキングデータの取り出し
	void ReadFile(string Difficulity) {
		i = 0;
		FileInfo fi = new FileInfo(Application.dataPath + "/" + "Scripts" + "/" + "ResultScreen" + "/" + Difficulity + "ResultScore.txt");
//		FileInfo fi = new FileInfo("./" + Difficulity + "ResultScoあっあre.txt");

		StreamReader file = new StreamReader(fi.OpenRead(), Encoding.UTF8);
	
		while((line = file.ReadLine()) != null) {
			line = line.Replace(Environment.NewLine, "");
			ScoreRank[i] = Int32.Parse(line);
			i++;
		}
		file.Close();
	}

	//ランキングデータの書き込み
	void WriteFile(string Difficulity) {
		i = 0;
		FileInfo fi = new FileInfo(Application.dataPath  + "/" + "Scripts" + "/" + "ResultScreen" + "/"  + Difficulity + "ResultScore.txt");
//		FileInfo fi = new FileInfo("./" + Difficulity + "ResultScore.txt");

		StreamWriter sw;
		File.Create(fi.FullName).Dispose();
		sw = new StreamWriter(fi.FullName,false);
		for(i = 0;i < RankMax;i++) {
			sw.WriteLine(ScoreRank[i].ToString());
		}
		sw.Close();
	}

	//ランキングに入っているか検索
	int RankingSearch() {
		i = 0;
		int rank = 100;
		while(i < RankMax) {
			//プレイヤーのデータとランキングに入ったかを判定
			if(sumScore >= ScoreRank[i]) {
				rank = i;
				for(i = RankMax-1;i > rank;i--) {
					ScoreRank[i] = ScoreRank[i-1];
				}
				ScoreRank[rank] = sumScore;
				break;
			}
			i++;
		}
		return rank;
	}

	//合計スコアでランク(A~Dを決定)
	void RankDivision() {
		if(sumScore >= rankA) {
			RankText.text = "A";
		} else if(sumScore >= rankB) {
			RankText.text = "B";
		} else if(sumScore >= rankC) {
			RankText.text = "C";
		} else if(sumScore >= rankD) {
			RankText.text = "D";
		}
	}

	//ランキングの表示
//	void RankingPrint() {
//		RankingText.text = "1st  " + ScoreRank[0].ToString() + "\n" + "2nd " + ScoreRank[1].ToString() + "\n"
//		 + "3rd " + ScoreRank[2].ToString() + "\n" + "4th " + ScoreRank[3].ToString() + "\n" + "5th " + ScoreRank[4].ToString();
//	}
}
