using UnityEngine;
using System.Collections;

public class GameManagerSelecter : MonoBehaviour {
	public GameObject Sweet;
	public GameObject Mild;
	public GameObject Bitter;

	// Use this for initialization
	void Start () {
//		ModeSelecter.SelectedMode = 1;

		if (ModeSelecter.SelectedMode == 0) {
			Instantiate (Sweet);
		} else if (ModeSelecter.SelectedMode == 1) {
			Instantiate (Mild);
		} else if (ModeSelecter.SelectedMode == 2) {
			Instantiate (Bitter);
		} else {
			Debug.Log ("Errror unknown mode");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
