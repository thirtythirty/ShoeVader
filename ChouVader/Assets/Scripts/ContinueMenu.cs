using UnityEngine;
using System.Collections;

public class ContinueMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.N)
			|| MyController.Controller1.switch1 || MyController.Controller1.switch2 || MyController.Controller2.switch1 || MyController.Controller2.switch2){
			// コンティニュー
			Time.timeScale = 1;
			FindObjectOfType<GameManager> ().Continue();
			Destroy (gameObject);
		} else if (Input.GetKey(KeyCode.C) || MyController.Controller1.switch3 || Input.GetKey(KeyCode.M) || MyController.Controller2.switch3){
			// スタート画面に戻る
		}
	}
}
