using UnityEngine;
using System.Collections;

public class Piman_EnemyBullet : Bullet {

	// Use this for initialization
	public override IEnumerator Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up.normalized * speed;
		Destroy (gameObject, lifeTime);

		yield break;
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}
}
