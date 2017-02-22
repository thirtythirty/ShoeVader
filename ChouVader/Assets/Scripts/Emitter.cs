using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {
	public GameObject[] waves;

	private int currentWave;

//	private Manager manager;

	// Use this for initialization
	IEnumerator Start () {
		if (waves.Length == 0) {
			yield break;
		}

//		manager = FindObjectOfType<Manager> ();
		while (true) {
//			while (manager.IsPlaying () == false) {
//				yield return new WaitForEndOfFrame ();
//			}

			GameObject wave = (GameObject)Instantiate (waves [currentWave]);
			wave.transform.parent = transform;

//			while (wave.transform.childCount != 0) { // すべて破壊されるまで待つ
//				yield return new WaitForEndOfFrame ();
//			}
			yield return new WaitForSeconds(wave.GetComponent<Wave>().waitTime);

//			Destroy (wave);

			if (waves.Length <= ++currentWave) {
				currentWave = 0;
				break;
			}
		}

		FindObjectOfType<Stage> ().CallBoss ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
