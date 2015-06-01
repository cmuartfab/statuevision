using UnityEngine;
using System.Collections;
using Leap;

namespace UnityStandardAssets.CrossPlatformInput 
{
	public class LeapFirstPersonControl
	{
		float horizontal;
		float vertical;
		float mouseX;
		float mouseY;

		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
		public string mouseXAxisName = "Mouse X"; // The name given to the mouse x axis for the cross platform input
		public string mouseYAxisName = "Mouse Y"; // The name given to the mouse y axis for the cross platform input
		
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseXVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseYVirtualAxis; // Reference to the joystick in the cross platform input
		
		public void CreateVirtualAxes()
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
		
		public void UpdateVirtualAxes()
		{
			m_HorizontalVirtualAxis.Update(horizontal);
			
			m_VerticalVirtualAxis.Update(vertical);
			
			m_MouseXVirtualAxis.Update(mouseX);
			
			m_MouseYVirtualAxis.Update(mouseY);

			horizontal = 0f;
			vertical = 0f;
			mouseX = 0f;
			mouseY = 0f;
		}

		public void AnalyzeFrame(Frame frame)
		{	
			//check that there are hands are on right side
			for (int i = 0; i < frame.Hands.Count; i++) 
			{
				Leap.Vector palmPosition = frame.Hands[i].PalmPosition;
				if (frame.Hands [i].IsRight) 
				{
					//change this to finger direction
					palmPosition = frame.Hands [i].PalmPosition;
				
					float forx = frame.Hands [i].Fingers [1].Direction.x;
					float fory = frame.Hands [i].Fingers [1].Direction.y;
					//print (frame.Hands [i].Fingers [1].Direction);

					vertical = -(palmPosition.z) / 100;
					mouseX = (palmPosition.x - 150) / 100;

					//vertical = -forz / 100f;
					//change htis to palm position

					//mouseX = frame.Hands [i].Fingers [1].Direction.x;
					//mouseY= frame.Hands [i].Fingers [1].Direction.y;
				}
			}
		}
	}	
}