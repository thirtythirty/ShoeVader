using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


public class Boss : Enemy {

	public float speed;
	public GameObject[] bullets;
	public GameObject[] barrages;
	public GameObject[] callEnemyWaves;
	public GameObject startPosition;
	public GameObject[] movePositions;
	public Rigidbody2D rb;
	public GameObject BossStatusField;
	protected GameObject BossHpBar;
	protected float Bar1_increment;
	protected float BarScaleY_origin;
	public string BossName;
	protected Text BossNameArea;

	public bool nowMoving = false;

	public void Shot(int index){
		for (int i = 0; i < bullets[index].transform.childCount; i++) {

			Transform shotPosition = bullets[index].transform.GetChild(i);
			Instantiate (bullets[index], transform.position,
				shotPosition.transform.rotation);
		}
	}

	// Use this for initialization
	public override IEnumerator Start () {
		rb = GetComponent<Rigidbody2D> ();

		BossStatusField = (GameObject)Instantiate (BossStatusField);
		GameObject canvas = BossStatusField.transform.FindChild ("Canvas").gameObject;
		BossNameArea = canvas.transform.FindChild("BossName").gameObject.transform.GetComponentInChildren<Text>();
		BossNameArea.text = BossName;
		BossHpBar = BossStatusField.transform.FindChild ("BossHpBar_front").gameObject;
		float BarScaleX = BossHpBar.transform.localScale.x;
		BarScaleY_origin = BossHpBar.transform.localScale.y;
		Bar1_increment = BarScaleX / hp;

		UnityEngine.Random.seed = (int)Time.time % 100;

		init ();

		nowMoving = true;
		StartCoroutine (MoveToPoint(startPosition.transform.position, speed));
		yield break;
	}

	public virtual void init(){

	}
	
	public override void Update () {
		StatusUpdate ();

		if (nowMoving == false) {
			int motion_pattern_rand = Random.Range (0, 4);
			if (motion_pattern_rand == 0) {
				int movePositions_index = Random.Range (0, movePositions.Length);
				if (movePositions.Length > movePositions_index) {
					nowMoving = true;
					StartCoroutine (MoveToPoint (movePositions [movePositions_index].transform.position, speed));
				}
			} else if (motion_pattern_rand == 1) {
				nowMoving = true;
				StartCoroutine (RushAndReturn (speed * 2, speed));
			} else if (motion_pattern_rand == 2) {
				nowMoving = true;
				StartCoroutine (CallEnemy (0));
			} else if (motion_pattern_rand == 3) {
				nowMoving = true;
				if (barrages.Length > 0) {
					Instantiate (barrages [0], transform.position, transform.rotation);
				}
			}
		}
	}

	public IEnumerator MoveToPoint(Vector3 movePosition, float moveSpeed){
		Vector2 moveVector = (movePosition - transform.position).normalized * moveSpeed;
		rb.velocity = moveVector;

		while (true) {
			if ((movePosition - transform.position).magnitude <= (moveVector.magnitude/5.0f)) {
				transform.position = movePosition;

				break;
			}
			yield return new WaitForEndOfFrame();
		}


		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}

	public IEnumerator RushAndReturn(float rushSpeed, float returnSpeed){
		Vector2 mix = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector3 rushPosition = new Vector2 (transform.position.x, mix.y);

		Vector3 returnPosition = transform.position;
		Vector2 moveVector = (rushPosition - transform.position).normalized * rushSpeed;
		rb.velocity = moveVector;

		while (true) {
			if ((rushPosition - transform.position).magnitude <= (moveVector.magnitude/5.0f)) {
				transform.position = rushPosition;

				break;
			}
			yield return new WaitForEndOfFrame();
		}

		Vector2 returnVector = (returnPosition - transform.position).normalized * returnSpeed;
		rb.velocity = returnVector;
		while (true) {
			if ((returnPosition - transform.position).magnitude <= (returnVector.magnitude/10.0f)) {
				transform.position = returnPosition;

				break;
			}
			yield return new WaitForEndOfFrame();
		}

		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}


	public IEnumerator CallEnemy(int waveNumber){
		if (callEnemyWaves.Length <= waveNumber) {
			nowMoving = false;

			yield break;
		}
		GameObject wave = (GameObject)Instantiate(callEnemyWaves[waveNumber]);
		rb.velocity = new Vector2(0, 0);

		yield return new WaitForSeconds (3.0f);

		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}


	public void StatusUpdate(){
		BossHpBar.transform.localScale = new Vector3(Bar1_increment*hp, BarScaleY_origin, 1);
	}

	public virtual void OnDestroy(){
		FindObjectOfType<GameManager> ().ChangeStage ();
		FindObjectOfType<Score> ().AddPoint (point/2, 1);
		FindObjectOfType<Score> ().AddPoint (point/2, 2);
	}

	public override void destroyAction(){
		Destroy (BossStatusField);

		unit.Explosion ();
		Destroy (gameObject);
	}
	public override void addPoint(int player_num){
		// 何もしない　（ポイント追加は別のところでやる）
	}

	// 移動後にnowMovingをfalseにしない版。
	// 組み合わせる時に使う。
	public IEnumerator MoveToPointForCoustom(Vector3 movePosition, float moveSpeed){
		Vector2 moveVector = (movePosition - transform.position).normalized * moveSpeed;
		rb.velocity = moveVector;

		while (true) {
			if ((movePosition - transform.position).magnitude <= (moveVector.magnitude/10.0f)) {
				transform.position = movePosition;

				break;
			}
			yield return new WaitForEndOfFrame();
		}


		rb.velocity = new Vector2(0, 0);
		yield break;
	}
}
