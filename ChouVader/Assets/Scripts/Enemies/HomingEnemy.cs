using UnityEngine;
using System.Collections;

public class HomingEnemy : Enemy {
	private GameObject target;
	public bool homingOn = true;

	public override void init(){
		base.init ();

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
	}

	public override void Move (float speed)
	{
//		base.Move (speed);
	}

	// Update is called once per frame
	public override void Update () {
		if (homingOn && target != null) {
			if (nowSlow) {
				unit.rb.velocity = (target.transform.position - transform.position).normalized * (unit.speed/10.0f);
			} else {
				unit.rb.velocity = (target.transform.position - transform.position).normalized * unit.speed;
			}
		}
	}
}
