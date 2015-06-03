using UnityEngine;
using System.Collections;

public class TurnToFace : MonoBehaviour {

	public float strength;

	private GameObject FPSController;
	private Transform target;
	private float dx;
	private float dy;
	private float radians;
	private float angle;

	// Use this for initialization
	void Start () {
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;

		dx = transform.position.x - target.transform.position.x;
		dy = transform.position.z - target.transform.position.z;
		radians = Mathf.Atan2(dx,dy);
		angle = radians * 180 / Mathf.PI + 180;
		
//		var rotateZ = Mathf.LerpAngle(transform.rotation.z, angle, Time.time);
		
		transform.eulerAngles = new Vector3(-90, transform.rotation.y, angle);
//		transform.eulerAngles = new Vector3(-90, transform.eulerAngles.y, transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {

	}
}