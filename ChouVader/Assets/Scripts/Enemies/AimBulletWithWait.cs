using UnityEngine;
using System.Collections;

public class AimBulletWithWait : AimBullet {
	public float waitTime = 0.0f;

	public override IEnumerator Start () {
		rb = GetComponent<Rigidbody2D> ();
		yield return new WaitForSeconds(waitTime);

		setVelocity ();
		Destroy (gameObject, lifeTime);

		yield break;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
