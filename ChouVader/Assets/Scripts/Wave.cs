using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
	public float waitTime = 10.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, waitTime + 60.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
