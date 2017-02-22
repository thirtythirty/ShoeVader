using UnityEngine;
using System.Collections;

public class Barrage : MonoBehaviour {

	public GameObject bullet;
	public int bulletTotal = 10;
	public int barrageTimes = 5;
	public float waitTime = 1.0f;
	public float angleRate = 0.01f;
	public Vector3 BaseVelocity = new Vector3(0,0,0);


	// Use this for initialization
	void Start () {
		bullet.GetComponent<Bullet> ().BaseVelocity = BaseVelocity;

		StartCoroutine (ShotBarrage ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	void ShotBulletByAngle(float angle){
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3 (0, 0, angle);

		Instantiate(bullet, transform.position, rotation);
	}

	IEnumerator ShotBarrage(){
		float angle_interval = 360.0f / bulletTotal;

		for (int i = 0; i < barrageTimes; i++) {
			for (int j = 0; j < bulletTotal; j++) {
				float angle = angle_interval * j+i*angleRate;
				ShotBulletByAngle (angle);
			}
			yield return new WaitForSeconds (waitTime);
		}

		Destroy (gameObject);
		yield break;
	}
}
