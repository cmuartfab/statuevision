using UnityEngine;
using System.Collections;
using Leap;

public class StatueScript : MonoBehaviour {
	
	Controller controller;
	Listener listener;

	public float maxDistance;

	private bool manipulable = false;
	private bool selected = false;
	static float bendAmount;
	static float bendAngle;
	private bool audioPlayed = false;

	private GameObject FPSController;
	private StatueViewPort statueViewPort;
	private Transform target;
	private Material[] materials;
	private Transform childStatue;
	private AudioSource audio;

	void Start() {

		//initialize Leap motion detection
		controller = new Controller ();

		//read player position as target
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;

		//initialize reference to materials 
		childStatue = this.gameObject.transform.GetChild (0).GetChild (0);
		materials = childStatue.GetComponent<Renderer> ().materials;

		//ties the right sculpture mesh from child
		this.gameObject.GetComponent<MeshCollider>().sharedMesh = childStatue.gameObject.GetComponent<MeshFilter>().mesh;

		//create trigger collider
		childStatue.gameObject.AddComponent<CapsuleCollider>();
		childStatue.GetComponent<CapsuleCollider> ().isTrigger = true;

		//initialize audio
		audio = GetComponent<AudioSource> ();
		}

	void OnTriggerEnter(Collider other) {
		if (!selected) {
			selected = true;
			if (!audioPlayed) {
				audio.Play ();
				audioPlayed = true;
			} else {
				audio.UnPause ();
			}
			statueViewPort.SelectStatue (this.gameObject);		
		}
	}

	void checkPlayerDistance() {
		if (selected && 
			Vector3.Distance (target.position, this.transform.position) > maxDistance) {
			selected = false;
			audio.Pause();
			statueViewPort.DeselectStatue ();
		}
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
	}

	public void SetStatueViewPort(StatueViewPort newViewPort) {
		statueViewPort = newViewPort;
	}

	public void SetIsManipulable (bool isManipulable) {
		manipulable = isManipulable;
	}
	
	void Update() {
		checkPlayerDistance ();
		if (manipulable) {
			Manipulate ();
		}
		//apply bend and twist to all materials
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].SetFloat("_BendAmount", bendAmount);
			materials[i].SetFloat("_BendAngle", bendAngle);
			//			materials[i].SetFloat("_TwistAmount", 0);
		}

	}
}
