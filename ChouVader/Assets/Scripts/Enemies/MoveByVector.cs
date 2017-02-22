using UnityEngine;
using System.Collections;

// 使うのやめる



public class MoveByVector : MonoBehaviour {
	public Unit unit;

	// Use this for initialization
	void Start () {
		unit = GetComponent<Unit> ();
		unit.rb = GetComponent<Rigidbody2D> ();

	}
}
