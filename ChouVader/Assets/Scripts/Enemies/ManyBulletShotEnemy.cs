using UnityEngine;
using System.Collections;

public class ManyBulletShotEnemy : Enemy {

	// Use this for initialization
	public GameObject[] bullets;
	public float waitTimeBetweenBullet = 0.0f;

	public override void ShotBullet(){
		StartCoroutine ("ManyBulletShot");
	}

	IEnumerator ManyBulletShot(){
		for(int i = 0; i < bullets.Length; i++){
			for (int j = 0; j < bullets [i].transform.childCount; j++) {

				Transform shotPosition = bullets [i].transform.GetChild (j);

				Instantiate (bullets [i], transform.position + shotPosition.position,
					shotPosition.transform.rotation);
				
				yield return new WaitForSeconds (waitTimeBetweenBullet);
			}
		}
	}
}
