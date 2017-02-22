using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int speed = 10;
	public float lifeTime = 5;
	public float power = 1;
	public Rigidbody2D rb;
	public Vector3 BaseVelocity = new Vector3(0,0,0);

	// Use this for initialization
	public virtual IEnumerator Start () {
		rb = GetComponent<Rigidbody2D> ();
		setVelocity ();
		Destroy (gameObject, lifeTime);

		yield break;
	}

	public virtual void setVelocity(){
		rb.velocity = BaseVelocity + transform.up.normalized * speed;
	}
	// Update is called once per frame
	public virtual void Update () {
//		rb.AddForce (transform.right.normalized * speed);

	}

	public void BaseVelocitySet(Vector3 v){
		BaseVelocity = v;
	}
}
