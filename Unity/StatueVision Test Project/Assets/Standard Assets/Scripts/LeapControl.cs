using UnityEngine;
using System.Collections;
using Leap;

public class LeapControl : MonoBehaviour {
	
	Controller controller;
	Listener listener;

	private float bendAmount;
	private float bendAngle;
	private bool manipulable;

	private GameObject FPSController;
	private Transform target;
	private Material[] materials;

	void Start() {

		//initialize Leap motion detection
		controller = new Controller ();

		//read player position as target
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;

		materials = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer> ().materials;

	}

	void OnTriggerEnter(Collider other) {
		print ("YO");
		manipulable = true;
	}

	void OnTriggerExit(Collider other) {
		manipulable = false;
	}

	void Manipulate() {
		//frame is where leap motion data is stored. Calculate bend angle based on hand position
		Frame frame = controller.Frame ();
		if (frame.Hands.Count > 0) {
			Leap.Vector palmPosition = frame.Hands[0].PalmPosition;
			float dx = palmPosition.x;
			float dz = palmPosition.z;
			bendAngle = 360 - Mathf.Atan2(dx, dz) * 180f / Mathf.PI;
			bendAmount = Mathf.Sqrt(Mathf.Pow(dx, 2f) + Mathf.Pow(dz, 2f)) * 0.0001f;
		}
		
		//apply bend and twist to all materials
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].SetFloat("_BendAmount", bendAmount);
			materials[i].SetFloat("_BendAngle", bendAngle);
			//			materials[i].SetFloat("_TwistAmount", 0);
		}
	}

	void Update() {
		if (manipulable) Manipulate ();
	}
}
