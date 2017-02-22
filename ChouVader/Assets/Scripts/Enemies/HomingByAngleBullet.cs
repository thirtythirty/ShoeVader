﻿using UnityEngine;
using System.Collections;

public class HomingByAngleBullet : Bullet {
	private GameObject target;
	public bool homingOn = true;
	public float rangeOfHomingOff = 1.0f;
	public float angle = 0.0f;
	public float addAngle = 1.0f;

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

		rb.velocity = transform.up * speed;

	}

	// Update is called once per frame
	public override void Update () {
		if (homingOn && target != null) {
			Vector2 toTarget = (target.transform.position - transform.position).normalized;

			var leftAngle = angle + addAngle;
			var rightAngle = angle - addAngle;

			var leftAngleCos = Mathf.Cos (addAngle * Mathf.Deg2Rad);
			var rightAngleCos = Mathf.Cos (-addAngle * Mathf.Deg2Rad);
			var toTargetDotSelf = Vector2.Dot (toTarget, new Vector2 (Mathf.Cos ((angle+90) * Mathf.Deg2Rad), Mathf.Sin ((angle+90) * Mathf.Deg2Rad)));


			var leftDot = Vector2.Dot (toTarget, new Vector2 (Mathf.Cos ((leftAngle+90) * Mathf.Deg2Rad), Mathf.Sin ((leftAngle+90) * Mathf.Deg2Rad)));
			var rightDot = Vector2.Dot (toTarget, new Vector2 (Mathf.Cos ((rightAngle+90) * Mathf.Deg2Rad), Mathf.Sin ((rightAngle+90) * Mathf.Deg2Rad)));

			if (leftDot >= rightDot) {
				if (toTargetDotSelf >= leftAngleCos) {
					var toTargetAngle = Mathf.Acos (toTargetDotSelf);
					angle += toTargetAngle;
				} else {
					angle += addAngle;
				}
			} else {
				if (toTargetDotSelf >= rightAngleCos) {

					var toTargetAngle = Mathf.Acos (toTargetDotSelf);
					angle -= toTargetAngle;
				} else {
					angle -= addAngle;
				}
			}
//			transform.Rotate (new Vector3 (0f, 0f, angle));
			transform.rotation = Quaternion.Euler(0, 0, angle);

			rb.velocity = transform.up * speed;

			var toTargetMagnitude = (target.transform.position - transform.position).magnitude;
			if (toTargetMagnitude < rangeOfHomingOff) {
				homingOn = false;
			}
		}
	}
}