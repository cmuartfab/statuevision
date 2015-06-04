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

		public float rightCenter_x = 150;
		public float rightCenter_z = 0;
		public float rightHorMax = 100;
		public float rightHorMin = 20;
		public float rightVerMax = 150;
		public float rightVerMin = 20;

		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
		public string mouseXAxisName = "Mouse X"; // The name given to the mouse x axis for the cross platform input
		public string mouseYAxisName = "Mouse Y"; // The name given to the mouse y axis for the cross platform input
		public float maxSphereRadius = 40f;
		
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseXVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_MouseYVirtualAxis; // Reference to the joystick in the cross platform input
		
		public void Init()
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
		
		public void Update(Frame frame, bool rightSide)
		{	
			horizontal = 0f;
			vertical = 0f;
			mouseX = 0f;
			mouseY = 0f;

			//check that there are hands are on right side
			for (int i = 0; i < frame.Hands.Count; i++) {

				Leap.Vector palmPosition = frame.Hands [i].PalmPosition;

				//hand over right side
				if (palmPosition.x > rightCenter_x - rightHorMax && 
					palmPosition.x < rightCenter_x + rightHorMax && 
					palmPosition.z > rightCenter_z - rightVerMax && 
					palmPosition.z < rightCenter_z + rightVerMax) 
				{
					Debug.Log("Right");
					//over sensitivity threshold
					if (palmPosition.x > rightCenter_x + rightHorMin ||
					    palmPosition.x < rightCenter_x - rightHorMin ||
					    palmPosition.z > rightCenter_z + rightVerMin ||
					    palmPosition.z < rightCenter_z - rightVerMin) 
					{
						//movement only with open palm
						Debug.Log("sensitive");
						if (frame.Hands [i].SphereRadius > maxSphereRadius) 
						{
							vertical = -(palmPosition.z) / 100;
							mouseX = (palmPosition.x - 150) / 100;
						}
					}
				}
			}


			m_HorizontalVirtualAxis.Update(horizontal);
			
			m_VerticalVirtualAxis.Update(vertical);
			
			m_MouseXVirtualAxis.Update(mouseX);
			
			m_MouseYVirtualAxis.Update(mouseY);

		}
	}	
}