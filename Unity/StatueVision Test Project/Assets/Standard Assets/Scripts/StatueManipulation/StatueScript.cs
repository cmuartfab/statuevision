using UnityEngine;
using System.Collections;
using Leap;

public class StatueScript : MonoBehaviour {
	
	public float maxDistance;
	public float leftMin_x = -250;
	public float leftMax_x = -50;
	public float leftMin_z = -150;
	public float leftMax_z = 150;

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
	private Frame frame;
	private LeapControl leapControl;

	void Start() {

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
		
		statueViewPort = GameObject.FindGameObjectWithTag("StatueViewPort").GetComponent<StatueViewPort>();
		leapControl = GameObject.FindGameObjectWithTag("LeapControl").GetComponent<LeapControl>();

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
	
	void Manipulate(Frame frame) {
		//frame is where leap motion data is stored. Calculate bend angle based on hand position
		print ("Working");
		for (int i = 0; i < frame.Hands.Count; i++) {
			Leap.Vector palmPosition = frame.Hands [i].PalmPosition;
			if (palmPosition.x > leftMin_x && 
				palmPosition.x < leftMax_x && 
				palmPosition.z > leftMin_z && 
				palmPosition.z < leftMax_z) 
			{
				float dx = palmPosition.x + 150;
				print (dx);
				float dz = palmPosition.z;
				bendAngle = 360 - Mathf.Atan2 (dx, dz) * 180f / Mathf.PI;
				bendAmount = Mathf.Sqrt (Mathf.Pow (dx, 2f) + Mathf.Pow (dz, 2f)) * 0.0001f;
			}		
		}
	}
	
	public void SetStatueViewPort(StatueViewPort newViewPort) {
		statueViewPort = newViewPort;
	}
	
	public void SetIsManipulable (bool isManipulable) {
		manipulable = isManipulable;
	}
	
	public void Update() {
		frame = leapControl.GetFrame();
		checkPlayerDistance ();
		if (manipulable) {
			Manipulate (frame);
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
