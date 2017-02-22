using UnityEngine;
using System.Collections;

public class MoveByPointANDManyBulletShotEnemy : ManyBulletShotEnemy {
	public GameObject[] MovePoints;
	public int index = 0;
	public bool ArriveAndStop = true;

	public override void init(){
		base.init ();
		StartCoroutine ("MoveToPoint");
	}

	public override void Move(float speed){
		if (index < MovePoints.Length) {
			Vector2 moveVector = (MovePoints[index].transform.position - transform.position).normalized * speed;
			unit.rb.velocity = moveVector;
		}
	}

	IEnumerator MoveToPoint(){
		while(index < MovePoints.Length) {
			Vector2 moveVector = (MovePoints[index].transform.position - transform.position).normalized * unit.speed;
			unit.rb.velocity = moveVector;

			while (true) {
				if ((MovePoints[index].transform.position - transform.position).magnitude <= (moveVector.magnitude / 10.0f)) {
					transform.position = MovePoints[index].transform.position;

					break;
				}
				yield return new WaitForEndOfFrame ();
			}
			index += 1;
		}

		if (ArriveAndStop == true) {
			unit.rb.velocity = new Vector2 (0, 0);
		}
		yield break;
	}
}
