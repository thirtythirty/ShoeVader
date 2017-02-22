using UnityEngine;
using System.Collections;

public class StickyCustard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 7.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D c){
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		if (layerName == "Bullet(Enemy)") {
			Destroy (c.gameObject);
		}
		if (layerName == "Enemy" || layerName == "EnemyInvincible") {
			var enemy = c.gameObject.GetComponents<Enemy> ();
			c.gameObject.GetComponent<Enemy> ().GetSlow (gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D c){
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		if (layerName == "Enemy") {
			c.gameObject.GetComponent<Enemy> ().nowSlow = false;
		}
	}
}
