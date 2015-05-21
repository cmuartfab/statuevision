using UnityEngine;
using System.Collections;

public class TwistToFace : MonoBehaviour {
	
	public float strength;

	private GameObject FPSController;
	private Transform target;
	private Material[] materials;
	private float offset;
	private float wobble;

	private float angle;
	private float oldAngle;
	private float dx;
	private float dy;
	private float radians;

	// Use this for initialization
	void Start () {
		FPSController = GameObject.FindWithTag ("Player");
		target = FPSController.transform;
		offset = Random.Range (0, 10);
		materials = this.GetComponent<Renderer> ().materials;

		dx = transform.position.x - target.transform.position.x;
		dy = transform.position.z - target.transform.position.z;
		radians = Mathf.Atan2(dx,dy);
		oldAngle = (radians * 180 / Mathf.PI) - transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {

		wobble = Mathf.Sin (offset + Time.time * 3) * 0.001f;

		//twist to face player
		dx = transform.position.x - target.transform.position.x;
		dy = transform.position.z - target.transform.position.z;
		radians = Mathf.Atan2(dx,dy);
		angle = (radians * 180 / Mathf.PI) - transform.eulerAngles.y + 180;

		//interpolate angle
		angle = Mathf.Lerp (oldAngle, angle, 0.3f);
		oldAngle = angle;

		for (int i = 0; i < materials.Length; i++)
		{
			//wobble
			materials[i].SetFloat("_BendAmount", wobble);
			materials[i].SetFloat("_TwistAmount", angle);
		}


	}
}