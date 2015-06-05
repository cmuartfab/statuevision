using UnityEngine;
using System.Collections;
using Leap;

	public class LeapControl : MonoBehaviour 
	{

		public float rightMin_x = 50;
		public float rightMax_x = 250;
		public float rightMin_z = -150;
		public float rightMax_z = 150;

		public float leftMin_x = -250;
		public float leftMax_x = -50;
		public float leftMin_z = -150;
		public float leftMax_z = 150;

		private Controller controller;
		private Frame frame;
		private bool rightSide;
		private bool leftSide;
		private UnityStandardAssets.CrossPlatformInput.LeapFirstPersonControl leapFirstPersonControl;

		// Use this for initialization
		void Start () 
		{
			controller = new Controller ();
			leapFirstPersonControl = new UnityStandardAssets.CrossPlatformInput.LeapFirstPersonControl ();
			leapFirstPersonControl.Init();
		}
		
		// Update is called once per frame
		void Update () 
		{
			frame = controller.Frame ();

			//default to false so hand positions are checked every frame
			rightSide = false;
			leftSide = false;
			
			//check that there are hands are on right side
			for (int i = 0; i < frame.Hands.Count; i++) 
			{
				Leap.Vector palmPosition = frame.Hands[i].PalmPosition;
				
				if (palmPosition.x > rightMin_x && 
				    palmPosition.x < rightMax_x && 
				    palmPosition.z > rightMin_z && 
				    palmPosition.z < rightMax_z) rightSide = true;
				
				
				if (palmPosition.x > leftMin_x && 
				    palmPosition.x < leftMax_x && 
				    palmPosition.z > leftMin_z && 
				    palmPosition.z < leftMax_z) leftSide = true;
				
			}
	
			leapFirstPersonControl.Update (frame, rightSide);

			//PrintPosition (leftSide, rightSide);
		}

		public Frame GetFrame()
		{
			return frame;
		}
			
		void PrintPosition(bool leftSide, bool rightSide)
		{
			if (rightSide && leftSide)
				print ("both");
			else if (rightSide)
				print ("right");
			else if (leftSide)
				print ("left");
			else
				print ("neither");
		}
	}	