using UnityEngine;
using System.Collections;

public class SecondBoss_Mild : Boss {
	SpriteRenderer SpriteRenderer;

	public GameObject SPPosition;
	private Coroutine MoveCorutine;
	private int motionCount = 0;
	public GameObject[] Curry;

	public Sprite NormalSprite;
	public Sprite SpSprite;
	public Sprite Damege2Sprite;
	public GameObject LastExplosion;
	public GameObject ManySpear;
	public GameObject Pyramid;
	public GameObject RushPosition;
	private float MaxHP;
	public AudioSource PyramidBoice;
	public AudioSource StripBoice;
	public AudioSource SpearBoice;
	public AudioSource LastBoice;
	public bool StripOn = false;
	public GameObject stripBarrage;


	public override void init ()
	{
		base.init ();
		MaxHP = hp;
		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	public override void Update () {
		StatusUpdate ();

		if (nowMoving == false) {
			motionCount += 1;

			if (StripOn == false && hp < MaxHP/2) {
				StripOn = true;
				nowMoving = true;
				StartCoroutine (StripBarrage ());
				return;
			}

			if (StripOn) {
				SpriteRenderer.sprite = SpSprite;
			} else {
				SpriteRenderer.sprite = NormalSprite;
			}

			if (motionCount % 7 == 0 || (hp < MaxHP/2 && motionCount %4 == 0)) {
				int spmotion_pattern_rand = Random.Range (0, 3);
				if (spmotion_pattern_rand == 0) {
					nowMoving = true;
					StartCoroutine (CallPyramid ());
				} else if (spmotion_pattern_rand == 1) {
					if (callEnemyWaves.Length > 1) {
						nowMoving = true;
						StartCoroutine (CallEnemyAddSPSprite (1));
					}
				} else if (spmotion_pattern_rand == 2) {
					nowMoving = true;
					StartCoroutine (ShotManySpear ());
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
				StartCoroutine (RushCustom (speed * 2, true));
			} else if (motion_pattern_rand == 2) {
				nowMoving = true;
				StartCoroutine (CallEnemy(0));
			}
		}
	}

	public IEnumerator StripBarrage(){
		if (stripBarrage == null) {
			nowMoving = false;
			yield break;
		}

		yield return StartCoroutine (MoveToPointForCoustom (startPosition.transform.position, speed));

		StripBoice.Play ();
		SpriteRenderer.sprite = SpSprite;

		Instantiate (stripBarrage, transform.position, transform.rotation);

		yield return new WaitForSeconds (5.0f);

		nowMoving = false;
	}

	public IEnumerator ShotManySpear(){
		if (ManySpear == null) {
			nowMoving = false;

			yield break;
		}


		yield return StartCoroutine (MoveToPointForCoustom (startPosition.transform.position, speed));
		nowMoving = true;
		SpearBoice.Play ();

		Instantiate(ManySpear);

		rb.velocity = new Vector2(0, 0);

		yield return new WaitForSeconds (10.0f);

		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}

	IEnumerator CallPyramid(){
		if (Pyramid == null) {
			nowMoving = false;

			yield break;
		}
		PyramidBoice.Play ();
		yield return new WaitForSeconds (2.0f);

		yield return StartCoroutine (RushCustom (speed * 2, false));
		nowMoving = true;
		Instantiate(Pyramid);

		SpriteRenderer.sprite = SpSprite;

		rb.velocity = new Vector2(0, 0);

		yield return new WaitForSeconds (2.0f);

		nowMoving = false;
		yield break;
	}

	public IEnumerator RushCustom(float rushSpeed,bool endMoving){
		Vector2 mix = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector3 rushPosition = new Vector2 (transform.position.x, mix.y+2.0f);

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

		if (endMoving) {
			nowMoving = false;
		}
		rb.velocity = new Vector2(0, 0);
		yield break;
	}

	public IEnumerator CallEnemyAddSPSprite(int waveNumber){
		if (callEnemyWaves.Length <= waveNumber) {
			nowMoving = false;

			yield break;
		}
		SpearBoice.Play ();

		GameObject wave = (GameObject)Instantiate(callEnemyWaves[waveNumber]);
		rb.velocity = new Vector2(0, 0);

		SpriteRenderer.sprite = SpSprite;

		yield return new WaitForSeconds (5.0f);

		nowMoving = false;
		rb.velocity = new Vector2(0, 0);
		yield break;
	}

	public override void destroyAction(){
		Destroy (BossStatusField);

		StartCoroutine (DestroyMotion ());
	}

	IEnumerator DestroyMotion(){
		LastBoice.Play ();
		SpriteRenderer.sprite = Damege2Sprite;
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
