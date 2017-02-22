using UnityEngine;
using System.Collections;

public class ManyBullets : Bullet {

	public GameObject[] bullets;
	public float waitTimeBetweenBullet;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < bullets.Length; i++) {
			bullets[i].GetComponent<Bullet> ().BaseVelocity = new Vector3 (0, 0, 0);
		}
		StartCoroutine(ManyBulletShot());
	}

	IEnumerator ManyBulletShot(){
		for(int i = 0; i < bullets.Length; i++){
			for (int j = 0; j < bullets [i].transform.childCount; j++) {

				Transform shotPosition = bullets [i].transform.GetChild (j);

				Instantiate (bullets [i], transform.position + shotPosition.position,
					shotPosition.transform.rotation);

			}
			yield return new WaitForSeconds (waitTimeBetweenBullet);
		}
	}

}