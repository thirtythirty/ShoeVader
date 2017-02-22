using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public Rigidbody2D rb;
	public float MoveSpeed;
	public float DestroyTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Move (MoveSpeed);
		Destroy (gameObject, DestroyTime);
	}

	public virtual void Move (float speed){
		rb.velocity = transform.up * -1 * speed;
	}

	public virtual void effect(Player player){

	}
}
