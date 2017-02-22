using UnityEngine;
using System.Collections;

public class LaserBullet : Bullet {
	public GameObject laserRemain;
	
	// Update is called once per frame
	public override void Update () {
		Instantiate(laserRemain, transform.position,
			transform.rotation);
	}
}
