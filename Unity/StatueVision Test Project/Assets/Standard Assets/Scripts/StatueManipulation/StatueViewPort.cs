using UnityEngine;
using System.Collections;

public class StatueViewPort : MonoBehaviour {
	
	private GameObject[] statues;
	private GameObject statueClone;

	public void Start() 
	{
		//connect all statue scripts with this script
		statues = GameObject.FindGameObjectsWithTag ("Statue");
		foreach (GameObject statue in statues) 
		{
			statue.GetComponent<StatueScript>().SetStatueViewPort(this);
		}
	}

	public void SelectStatue(GameObject statue) 
	{
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
