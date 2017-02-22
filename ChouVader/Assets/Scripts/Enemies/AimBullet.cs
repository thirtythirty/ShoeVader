using UnityEngine;
using System.Collections;

public class AimBullet : Bullet {
	private GameObject target;
	private GameObject player2;

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

		rb.velocity = (target.transform.position - transform.position).normalized * speed;


//		base.setVelocity ();
	}
}
