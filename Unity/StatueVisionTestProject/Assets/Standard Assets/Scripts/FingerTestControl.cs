using UnityEngine;
using System.Collections;
using Leap;

public class FingerTestControl : MonoBehaviour {

	private Controller controller;
	private float walkSpeed;
	private bool isWalking;
	private float walkTimer;
	private float[] tapTimes;
	private int taps;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		controller.EnableGesture (Gesture.GestureType.TYPE_KEY_TAP);

		controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 10.0f);
		controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", .1f);
		controller.Config.SetFloat("Gesture.KeyTap.MinDistance", 0.05f);
		controller.Config.Save();

		tapTimes = new float[3];
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();
		if (!isWalking) {
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures [i];
				KeyTapGesture tap = new KeyTapGesture (gesture);
//				isWalking = true;
				walkTimer = Time.time;
				taps++;
			}
		}
		if (isWalking && Time.time <= walkTimer + 1) {

		}
		if (isWalking && Time.time > walkTimer + 1) {

		}
	}
}
