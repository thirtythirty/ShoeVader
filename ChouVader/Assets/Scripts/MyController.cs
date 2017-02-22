using UnityEngine;
using System.Collections;

public class MyController : MonoBehaviour {
	public static SerialHandler Controller1 = new SerialHandler();
	public static SerialHandler Controller2 = new SerialHandler();

	public SerialHandler Controller1_;
	public SerialHandler Controller2_;

	// Use this for initialization
	void Start () {
		if (Controller1 != null) {
			Controller1.CloseSerialIfOpen ();
		}
		if (Controller2 != null) {
			Controller2.CloseSerialIfOpen ();
		}
		Controller1 = Controller1_;
		Controller2 = Controller2_;
	}
}
