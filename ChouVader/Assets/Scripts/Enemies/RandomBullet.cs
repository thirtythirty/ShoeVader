using UnityEngine;
using System.Collections;

public class RandomBullet : MonoBehaviour {
	public GameObject bullet;
	public int bulletTotal = 10;
	public int barrageTimes = 5;
	public float waitTime = 1.0f;
	public float beginAngle = 0.0f;
	public float endAngle = 0.0f;

	// Use this for initialization
	void Start () {
		bullet.GetComponent<Bullet> ().BaseVelocity = new Vector3 (0, 0, 0);
		StartCoroutine (ShotRandom ());
	}

	void ShotBulletByAngle(float angle){
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3 (0, 0, angle);

		Instantiate(bullet, transform.position, rotation);
	}

	IEnumerator ShotRandom(){
		UnityEngine.Random.seed = (int)Time.time % 100;

		for (int i = 0; i < barrageTimes; i++) {
			for (int j = 0; j < bulletTotal; j++) {
				var r = Random.Range(beginAngle,endAngle);
				float angle = r;
				ShotBulletByAngle (angle);
			}
			yield return new WaitForSeconds (waitTime);
		}

		Destroy (gameObject);
		yield break;
	}
}
