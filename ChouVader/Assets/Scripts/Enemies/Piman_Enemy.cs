using UnityEngine;
using System.Collections;

public class Piman_Enemy : Enemy {

	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	public override void Update () {
		unit.rb.AddForce (transform.right.normalized * unit.speed);
	}
}
