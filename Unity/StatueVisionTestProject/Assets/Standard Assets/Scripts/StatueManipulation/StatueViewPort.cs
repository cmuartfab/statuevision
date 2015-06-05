using UnityEngine;
using System.Collections;

public class StatueViewPort : MonoBehaviour {
	
	private GameObject[] statues;
	private GameObject statueClone;

	public void Start() 
	{
	}

	public void SelectStatue(GameObject statue) 
	{
		GameObject.Destroy (statueClone);
		print ("Select");
		statueClone = Instantiate (statue, Vector3.zero, Quaternion.identity) as GameObject;
		statueClone.GetComponent<StatueScript> ().SetIsManipulable (true);
	}

	public void DeselectStatue()
	{
		print ("Deselect");
		GameObject.Destroy (statueClone);
	}
}
