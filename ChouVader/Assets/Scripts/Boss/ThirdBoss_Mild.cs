using UnityEngine;
using System.Collections;

public class ThirdBoss_Mild : Boss {
	public GameObject SPPosition;
	private Coroutine MoveCorutine;
	private int motionCount = 0;
	public GameObject randomBullet;
	public GameObject fake;
	public GameObject fakeAndMovePosition;
	public bool SpMotionOn = true;

	public GameObject LastExplosion;
	private float MaxHP;

	public override void init ()
	{
		base.init ();
		MaxHP = hp;
	}
	public override void Update () {
		StatusUpdate ();

		if (nowMoving == false) {
			motionCount += 1;

			if (hp < (MaxHP/ 2) && SpMotionOn == true) {
				SpMotionOn = false;
				nowMoving = true;
				StartCoroutine (FakeBody ());
			}

			if (motionCount % 7 == 0 || (hp < MaxHP/2 && motionCount %4 == 0)) {
				int spmotion_pattern_rand = Random.Range (0, 1);
				if (spmotion_pattern_rand == 0) {
					nowMoving = true;
					StartCoroutine (ShotTwoBarrange ());
				} else if (spmotion_pattern_rand == 1) {
					nowMoving = true;
					StartCoroutine (CallEnemy(0));
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
				StartCoroutine (ShotRandomBullet ());
			} else if (motion_pattern_rand == 2) {
				nowMoving = true;
				StartCoroutine (CallEnemy(1));
			}
		}
	}

	public IEnumerator ShotRandomBullet(){
		Instantiate (randomBullet, transform.position, transform.rotation);
		yield return new WaitForSeconds (2.0f);

		nowMoving = false;
		yield break;
	}

	public IEnumerator ShotTwoBarrange(){
		if (barrages.Length < 2) {
			nowMoving = false;
		}

		Instantiate (barrages [0], transform.position, transform.rotation);
		var barrage1 = barrages [0].GetComponent<Barrage> ();
		yield return new WaitForSeconds (barrage1.barrageTimes*barrage1.waitTime);

		Instantiate (barrages [1], transform.position, transform.rotation);
		var barrage2 = barrages [1].GetComponent<Barrage> ();
		yield return new WaitForSeconds (barrage2.barrageTimes*barrage2.waitTime);

		yield return new WaitForSeconds (2.0f);

		nowMoving = false;
		yield break;
	}

	public IEnumerator FakeBody(){
		if (fake== null) {
			nowMoving = false;

			yield break;
		}

		yield return StartCoroutine (MoveToPointForCoustom (startPosition.transform.position, speed));
		Instantiate(fake, transform.position, transform.rotation);

		yield return StartCoroutine (MoveToPointForCoustom (fakeAndMovePosition.transform.position, speed));
		nowMoving = false;

		yield break;
	}
//	public IEnumerator ShotManySpear(){
//		if (ManySpear == null) {
//			nowMoving = false;
//
//			yield break;
//		}
//
//		yield return StartCoroutine (MoveToPointForCoustom (startPosition.transform.position, speed));
//		nowMoving = true;
//		Instantiate(ManySpear);
//
//		rb.velocity = new Vector2(0, 0);
//		SpriteRenderer.sprite = SpSprite;
//
//		yield return new WaitForSeconds (10.0f);
//
//		nowMoving = false;
//		rb.velocity = new Vector2(0, 0);
//		yield break;
//	}
//
//	IEnumerator CallPyramid(){
//		if (Pyramid == null) {
//			nowMoving = false;
//
//			yield break;
//		}
//
//		yield return StartCoroutine (RushCustom (speed * 2, false));
//		nowMoving = true;
//		Instantiate(Pyramid);
//
//		if (hp < MaxHP/2) {
//			SpriteRenderer.sprite = Damege2Sprite;
//		} else {
//			SpriteRenderer.sprite = SpSprite;
//		}
//		rb.velocity = new Vector2(0, 0);
//
//		yield return new WaitForSeconds (2.0f);
//
//		nowMoving = false;
//		yield break;
//	}
//
//	public IEnumerator RushCustom(float rushSpeed,bool endMoving){
//		Vector2 mix = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
//		Vector3 rushPosition = new Vector2 (transform.position.x, mix.y+2.0f);
//
//		Vector3 returnPosition = transform.position;
//		Vector2 moveVector = (rushPosition - transform.position).normalized * rushSpeed;
//		rb.velocity = moveVector;
//
//		while (true) {
//			if ((rushPosition - transform.position).magnitude <= (moveVector.magnitude/5.0f)) {
//				transform.position = rushPosition;
//
//				break;
//			}
//			yield return new WaitForEndOfFrame();
//		}
//
//		if (endMoving) {
//			nowMoving = false;
//		}
//		rb.velocity = new Vector2(0, 0);
//		yield break;
//	}

//	public IEnumerator CallEnemyAddSPSprite(int waveNumber){
//		if (callEnemyWaves.Length <= waveNumber) {
//			nowMoving = false;
//
//			yield break;
//		}
//		GameObject wave = (GameObject)Instantiate(callEnemyWaves[waveNumber]);
//		rb.velocity = new Vector2(0, 0);
//		if (hp < MaxHP/2) {
//			SpriteRenderer.sprite = Damege2Sprite;
//		} else {
//			SpriteRenderer.sprite = SpSprite;
//		}
//
//		yield return new WaitForSeconds (5.0f);
//
//		nowMoving = false;
//		rb.velocity = new Vector2(0, 0);
//		yield break;
//	}

	public override void destroyAction(){
		Destroy (BossStatusField);

		StartCoroutine (DestroyMotion ());
	}

	IEnumerator DestroyMotion(){
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
