using UnityEngine;
using System.Collections;

public class FirstBoss_Mild : Boss {
	SpriteRenderer SpriteRenderer;

	public GameObject SPPosition;
	public GameObject mazeBarrage;
	private Coroutine MoveCorutine;
	private int motionCount = 0;
	public GameObject[] ManyBitterGourd;
	public GameObject[] positionsForBitterGourd;
	public Sprite NormalSprite;
	public Sprite SPSprite;
	public Sprite DamegeSprite;
	public GameObject LastExplosion;

	public override void init ()
	{
		base.init ();

		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	public override void Update () {
		StatusUpdate ();

		if (nowMoving == false) {
			motionCount += 1;
			SpriteRenderer.sprite = NormalSprite;

			if (motionCount % 8 == 0) {
				int spmotion_pattern_rand = Random.Range (0, 3);
				if (spmotion_pattern_rand == 0) {
					if (ManyBitterGourd.Length > 0) {
						nowMoving = true;
						StartCoroutine (CallBitterGourd (0));
					}
				} else if (spmotion_pattern_rand == 1){
					if (ManyBitterGourd.Length > 1) {
						nowMoving = true;
						StartCoroutine (CallBitterGourd (1));
					}
				} else if (spmotion_pattern_rand == 2) {
					nowMoving = true;
					StartCoroutine (ShotMazeBarrage ());
				}
				return;
			}

			int motion_pattern_rand = Random.Range (0, 3);
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
				StartCoroutine (CallEnemyAddSPSprite (0));
			}
		}
	}


	public IEnumerator ShotMazeBarrage(){
		if (mazeBarrage == null) {
			nowMoving = false;

			yield break;
		}

		yield return StartCoroutine (MoveToPointForCoustom (startPosition.transform.position, speed));
		nowMoving = true;
		Instantiate(mazeBarrage);

		rb.velocity = new Vector2(0, 0);
		SpriteRenderer.sprite = SPSprite;

		yield return new WaitForSeconds (14.0f);

		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}

	IEnumerator CallBitterGourd(int index){
		if (ManyBitterGourd[index] == null || positionsForBitterGourd[index] == null) {
			nowMoving = false;

			yield break;
		}

		yield return StartCoroutine (MoveToPointForCoustom (positionsForBitterGourd[index].transform.position, speed));
		nowMoving = true;

		rb.velocity = new Vector2(0, 0);
		SpriteRenderer.sprite = SPSprite;

		GameObject wave = (GameObject)Instantiate(ManyBitterGourd[index]);
		yield return new WaitForSeconds (wave.GetComponent<Wave>().waitTime);
		nowMoving = false;
		yield break;
	}

	public IEnumerator CallEnemyAddSPSprite(int waveNumber){
		if (callEnemyWaves.Length <= waveNumber) {
			nowMoving = false;

			yield break;
		}
		GameObject wave = (GameObject)Instantiate(callEnemyWaves[waveNumber]);
		rb.velocity = new Vector2(0, 0);

		SpriteRenderer.sprite = SPSprite;
		yield return new WaitForSeconds (3.0f);

		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}

	public override void destroyAction(){
		Destroy (BossStatusField);

		StartCoroutine (DestroyMotion ());
	}

	IEnumerator DestroyMotion(){
		SpriteRenderer.sprite = DamegeSprite;
		var renderer_ = GetComponent<Renderer>();

		int count = 10;
		while (count > 0){
			//透明にする
			renderer_.material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			renderer_.material.color = new Color (1,1,1,1);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}

		Instantiate (LastExplosion, transform.position, transform.rotation);

	

		Destroy (gameObject);
	}
}
