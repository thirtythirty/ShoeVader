using UnityEngine;
using System.Collections;


public class PlayerBullet : Bullet {
	public int player_num;
	public bool extraBulletCanShot;
	public GameObject extraBullet;
	public Color MixBulletColoer;
		
	void OnTriggerEnter2D(Collider2D c){
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		if (layerName == "Bullet(Player)") {
			if (extraBulletCanShot == true) {
				if (player_num != c.gameObject.GetComponent<PlayerBullet> ().player_num) {
					power = power*1.5f;
					gameObject.GetComponent<SpriteRenderer> ().color = MixBulletColoer;

//					Instantiate (extraBullet, transform.position, transform.rotation);

					extraBulletCanShot = false;
				}
			}
		}
	}
}
