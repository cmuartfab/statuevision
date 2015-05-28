using UnityEngine;
using System.Collections;
using Leap;

namespace UnityStandardAssets.CrossPlatformInput 
{
	public class LeapControl : MonoBehaviour 
	{

		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		private Controller controller;

		// Use this for initialization
		void Start () 
		{
			controller = new Controller ();

			CreateVirtualAxes();
		}
		
		// Update is called once per frame
		void Update () 
		{
			UpdateVirtualAxes ();
		}

		void CreateVirtualAxes()
		{
			// create new axes based on axes to use
			m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);

			m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
		}

		void UpdateVirtualAxes()
		{
			Frame frame = controller.Frame ();
			float horizontal = 0f;
			float vertical = 0f;

			for (int i = 0; i < frame.Hands.Count; i++) 
			{
				if (frame.Hands [i].IsRight) 
				{
					float forx = frame.Hands [i].Fingers [1].Direction.x;
					float fory = frame.Hands [i].Fingers [1].Direction.y;
					//print (frame.Hands [i].Fingers [1].Direction);
					if (fory > -0.4 && fory < 0.4) 
					{
						vertical = 1f - Mathf.Abs(fory);
						horizontal = forx * 3f;
					}
					float back = frame.Hands [0].Fingers [0].Direction.x;
					if (back > -1f && back < -0.5 && !(fory > -0.2 && fory < 0.2)) 
					{
						vertical = -1;					
					}
				}	
			}
			m_HorizontalVirtualAxis.Update(horizontal);
			
			m_VerticalVirtualAxis.Update(vertical);
		}

		void MouseControl () 
		{
			Frame frame = controller.Frame ();
			for (int i = 0; i < frame.Hands.Count; i++) 
			{
				if (frame.Hands [i].IsLeft) 
				{
					Leap.Vector palmPosition = frame.Hands [i].PalmPosition;
					float dx = palmPosition.x;
					float dz = palmPosition.z;
				}
			}
		}
	}	
}