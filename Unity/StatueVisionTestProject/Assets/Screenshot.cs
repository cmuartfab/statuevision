using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {

	// Use this for initialization
	private int pic = 0;
	
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.S)){
			Application.CaptureScreenshot("Users/clairehentschker/Documents/Development/statuevision/Unity/videoFrameOutput/ss_"+pic.ToString() + ".png");
			pic += 1;
		}
	}
}