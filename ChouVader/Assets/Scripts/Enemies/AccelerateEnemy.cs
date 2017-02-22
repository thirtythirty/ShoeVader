using UnityEngine;
using System.Collections;

public class AccelerateEnemy : Enemy {
	private Quaternion Forcedirection = Quaternion.Euler(0, 0, 0);
	public float Angle = 0.0f;
	public int ForceSize = 1;
	public Vector3 BaseVelocity = new Vector3(0,0,0);

	public override void Move(float speed){
		unit.rb.velocity = BaseVelocity + transform.up * -1 * speed;
	}

	public override void Update (){
		if (LayerMask.LayerToName (gameObject.layer) == "EnemyInvincible") {
			return;
		}
		Forcedirection = Quaternion.Euler (0, 0, Angle);

		unit.rb.AddForce (Forcedirection * Vector2.right * ForceSize);
	}
}
