using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // いらない

using UnityEngine.SceneManagement; 



public class ModeSelecter : MonoBehaviour {
	public static int SelectedMode = 1;
	public static int playerMax = 2;
	/*public GameObject eventSystemGameObject;
	public GameObject Buttons;
	public int nowSelect = 0;
	private EventSystem eventSystem;*/
	public GameObject myController;

	//画像
	public Image startPanel;
	public Sprite startSprite1;
	public Sprite startSprite2;
	public Sprite startSprite3;
	public Sprite startSpriteSelected1;
	public Sprite startSpriteSelected2;
	public Sprite startSpriteSelected3;

	//音
	private AudioSource audioSource;
	public AudioClip moveSoundClip;
	public AudioClip selectSoundClip;

	//移動したか
	private bool modeChanged = false;

	private bool switchOn = false;


	// Use this for initialization
	void Start () {
		Instantiate (myController);

		audioSource = startPanel.GetComponent<AudioSource>();
		audioSource.clip = moveSoundClip;
	}

	// Update is called once per frame
	/*void Update () {
		if (Input.GetKey (KeyCode.Z) || MyController.Controller1.switch1 || Input.GetKey (KeyCode.B) || MyController.Controller2.switch1) {
			SelectedMode = 0;
//			SceneManager.LoadScene ("game");
			FadeManager.Instance.LoadLevel("game",0.5f);
		} else if (Input.GetKey (KeyCode.X) || MyController.Controller1.switch2 || Input.GetKey (KeyCode.N) || MyController.Controller2.switch2) {
			SelectedMode = 2;
//			SceneManager.LoadScene ("game");
			FadeManager.Instance.LoadLevel("game",0.5f);
		} else if (Input.GetKey (KeyCode.C) || MyController.Controller1.switch3 || Input.GetKey (KeyCode.M) || MyController.Controller2.switch3) {
			SelectedMode = 1;
//			SceneManager.LoadScene ("game");
			FadeManager.Instance.LoadLevel("game",0.5f);
		}
	}*/

	void Update () {
		if (Input.GetKeyUp(KeyCode.Alpha1)) {
			ModeSelecter.playerMax = 1;
			Debug.Log (ModeSelecter.playerMax);
		}
		if (Input.GetKeyUp(KeyCode.Alpha2)) {
			ModeSelecter.playerMax = 2;
			Debug.Log (ModeSelecter.playerMax);
		}

		if (switchOn) {
			return;
		}

		//右移動
		if (Input.GetKeyUp (KeyCode.X) || Input.GetKeyUp (KeyCode.A) || MyController.Controller1.switch2 || Input.GetKeyUp (KeyCode.N) || Input.GetKeyUp (KeyCode.G) || MyController.Controller2.switch2) {

			Debug.Log (MyController.Controller1.switch1);
			if (MyController.Controller1.switch2) {
				switchOn = true;
				StartCoroutine (WaitANDSwitchOff ());
			}
			if (MyController.Controller2.switch2) {
				switchOn = true;
				StartCoroutine (WaitANDSwitchOff ());
			}

			if (SelectedMode <= 0) {
				SelectedMode = 2;
			} else {
				SelectedMode--;
			}
			modeChanged = true;
			//左移動
		} else if (Input.GetKeyUp (KeyCode.Z) || Input.GetKeyUp (KeyCode.D) || MyController.Controller1.switch1 || Input.GetKeyUp (KeyCode.B) || Input.GetKeyUp (KeyCode.J) || MyController.Controller2.switch1) {
			if (MyController.Controller1.switch1) {
				switchOn = true;
				StartCoroutine (WaitANDSwitchOff ());
			}
			if (MyController.Controller2.switch1) {
				switchOn = true;
				StartCoroutine (WaitANDSwitchOff ());
			}

			if (SelectedMode >= 2) {
				SelectedMode = 0;
			} else {
				SelectedMode++;
			}
			modeChanged = true;

			//決定
		} else if (Input.GetKeyUp (KeyCode.C) || Input.GetKeyUp (KeyCode.W) || MyController.Controller1.switch3 || Input.GetKeyUp (KeyCode.M) || Input.GetKeyUp (KeyCode.Y) || MyController.Controller2.switch3) {
			if (MyController.Controller1.switch3) {
				switchOn = true;
				StartCoroutine (WaitANDSwitchOff ());
			}
			if (MyController.Controller2.switch3) {
				switchOn = true;
				StartCoroutine (WaitANDSwitchOff ());
			}

			//画像切り替え
			switch (SelectedMode) {
			case 0:
				startPanel.sprite = startSpriteSelected1;
				break;
			case 1:
				startPanel.sprite = startSpriteSelected2;
				break;
			case 2:
				startPanel.sprite = startSpriteSelected3;
				break;
			}
			//効果音
			audioSource.clip = selectSoundClip;
			audioSource.Play ();

			//待機と遷移
			StartCoroutine(sleepForLoadScene(1.5f));
		}



		if (modeChanged) {
			//効果音
			audioSource.Play();

			//カーソルの座標移動
			switch (SelectedMode) {
			case 0:
				startPanel.sprite = startSprite1;
				break;
			case 1:
				startPanel.sprite = startSprite2;
				break;
			case 2:
				startPanel.sprite = startSprite3;
				break;
			}
		}
	}

	IEnumerator sleepForLoadScene(float min){
		yield return new WaitForSeconds (min);

		//画面遷移
		FadeManager.Instance.LoadLevel("game",0.5f);
	}

	IEnumerator WaitANDSwitchOff(){
		switchOn = true;

		yield return new WaitForSeconds (0.3f);

		switchOn = false;
		yield break;
	}
}