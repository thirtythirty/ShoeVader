using UnityEngine;
using System.Collections;

public class ExplosionEnemy : Enemy {
	private GameObject player1;
	private GameObject player2;

	public bool homingOn = true;
	public float rangeOfExplosion = 10.0f;

	public override void init(){
		base.init ();

		player1 = GameObject.Find ("Player1");
		player2 = GameObject.Find ("Player2");
	}

	// Update is called once per frame
	public override void Update () {
		if (player1 != null) {
			Vector2 toTarget = (player1.transform.position - transform.position);
			if (toTarget.magnitude < rangeOfExplosion) {
				unit.Explosion ();
				Destroy (gameObject);
			}
		}
		if (player2 != null) {

			Vector2 toTarget = (player2.transform.position - transform.position);
			if (toTarget.magnitude < rangeOfExplosion) {
				unit.Explosion ();
				Destroy (gameObject);
			}
		}
	}
}
