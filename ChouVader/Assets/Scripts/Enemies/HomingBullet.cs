using UnityEngine;
using System.Collections;

public class HomingBullet : Bullet {
	private GameObject target;
	public bool homingOn = true;
	public float rangeOfHomingOff = 10.0f;

	// Use this for initialization
//	public virtual IEnumerator Start () {
//	
//	}

	public override void setVelocity(){

		GameObject player1 = GameObject.Find ("Player1");
		GameObject player2 = GameObject.Find ("Player2");
		if (player1 != null && player2 != null) {
			if ((player1.transform.position - transform.position).magnitude < (player2.transform.position - transform.position).magnitude) {
				target = player1;
			} else {
				target = player2;
			}
		} else if (player1 != null) {
			target = player1;
		} else if (player2 != null) {
			target = player2;
		} else {
			target = null;
		}

		if (target == null) {
			rb.velocity = transform.up * speed;
			return;
		}

		Vector2 toTarget = (target.transform.position - transform.position).normalized;

		rb.velocity = toTarget * speed;

	}

	// Update is called once per frame
	public override void Update () {
		if (homingOn && target != null) {
			rb.velocity = (target.transform.position - transform.position).normalized * speed;
			Vector2 toTarget = (target.transform.position - transform.position);
			if (toTarget.magnitude < rangeOfHomingOff) {
				homingOn = false;
			}
		}
	}
}
