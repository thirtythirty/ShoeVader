using UnityEngine;
using System.Collections;

public class LastAttemptEnemy : MonoBehaviour{

	public GameObject LastAttemptBullet; 

	void OnDestroy() {
		for (int i = 0; i < LastAttemptBullet.transform.childCount; i++) {

			Transform shotPosition = LastAttemptBullet.transform.GetChild(i);
			Instantiate (LastAttemptBullet, transform.position,
				shotPosition.transform.rotation);
		}
	}
}