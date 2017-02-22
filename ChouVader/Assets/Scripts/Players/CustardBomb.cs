using UnityEngine;
using System.Collections;

public class CustardBomb : PlayerBullet {

	public GameObject castard;

	void OnDestroy(){
		Instantiate (castard, transform.position,
			transform.rotation);
	}
}
