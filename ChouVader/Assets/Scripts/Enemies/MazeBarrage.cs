using UnityEngine;
using System.Collections;

public class MazeBarrage : MonoBehaviour {
	public string[] lines = {
		"##########################$$$$$$#########################",
		"#####################$$$$$###############################",
		"###########################$$$$$#########################",
		"##################################$$$$$##################",
		"###########################$$$$$#########################"
	};

	public float waitTime = 3f;
	public float angle_interval = 5.0f;
	public GameObject bullet;
	public float beginAngle = 0.0f;
	public float endAngle = 360.0f;
	private int bulletTotal = 0;

	// Use this for initialization
	void Start () {
		if (lines.Length > 0) {
			bulletTotal = lines [0].Length;
		}
		bullet.GetComponent<Bullet> ().BaseVelocity = new Vector3 (0, 0, 0);

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
		float angle_interval = (endAngle - beginAngle) / bulletTotal;

		for (int i = 0; i < lines.Length; i++) {
			for (int j = 0; j < lines[i].Length; j++) {
				if (lines [i] [j] == '#') {
					float angle = beginAngle + angle_interval * j;
					ShotBulletByAngle (angle);
				}
			}
			yield return new WaitForSeconds (waitTime);
		}

		Destroy (gameObject);
		yield break;
	}
}
