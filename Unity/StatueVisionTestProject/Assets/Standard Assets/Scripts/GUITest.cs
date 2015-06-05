using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	
	void OnGUI(){
		GUI.Box (new Rect (0,0,Screen.width / 2,50), "Top-left");
		GUI.Box (new Rect (Screen.width / 2,0,Screen.width / 2,50), "Top-right");
	}
	
}