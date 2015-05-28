using UnityEngine;
using System.Collections;
using Leap;

namespace UnityStandardAssets.CrossPlatformInput 
{
	public class LeapControl : MonoBehaviour 
	{

		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
		public string mouseXAxisName = "Mouse X"; // The name given to the mouse x axis for the cross platform input
		public string mouseYAxisName = "Mouse Y"; // The name given to the mouse y axis for the cross platform input

		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseXVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseYVirtualAxis; // Reference to the joystick in the cross platform input

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

			m_MouseXVirtualAxis = new CrossPlatformInputManager.VirtualAxis(mouseXAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_MouseXVirtualAxis);

			m_MouseYVirtualAxis = new CrossPlatformInputManager.VirtualAxis(mouseYAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(m_MouseYVirtualAxis);
		}

		void UpdateVirtualAxes()
		{
			Frame frame = controller.Frame ();
			float horizontal = 0f;
			float vertical = 0f;
			float mouseX = 0f;
			float mouseY = 0f;

			for (int i = 0; i < frame.Hands.Count; i++) 
			{
				if (frame.Hands [i].IsRight) 
				{
					mouseX = frame.Hands [i].Fingers [1].Direction.x;
					mouseY= frame.Hands [i].Fingers [1].Direction.y;

					Leap.Vector palmPosition = frame.Hands [i].PalmPosition;

					float forx = palmPosition.x;
					float fory = palmPosition.y;
					float forz = palmPosition.z;

					//print (frame.Hands [i].Fingers [1].Direction);

					vertical = -forz / 100f;
				}	
			}
			m_HorizontalVirtualAxis.Update(horizontal);
			
			m_VerticalVirtualAxis.Update(vertical);

			m_MouseXVirtualAxis.Update(mouseX);
			
			m_MouseYVirtualAxis.Update(mouseY);

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