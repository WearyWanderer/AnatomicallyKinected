using UnityEngine;


namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture component which checks for a pushing motion on the left or right arm.
	/// </summary>
	public sealed class ArmPushComponent : SideComponent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MIG.Gesture.ArmPullComponent"/> class.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		/// <param name="side"> The side of the skeleton to check. </param>
		/// <param name="tolerance"> How fast the hand must travel to be recognised. </param> 
		public ArmPushComponent (SkeletonWrapper kinect, Side side, float tolerance = 0.5f)
		{
			this.kinect    = kinect;
			this.side      = side;
			this.tolerance = tolerance;
		}
		
		
		/// <summary>
		/// Examines the velocity of the desired arm to see if it's making pushing motion.
		/// </summary>
		/// <returns> The result of the evaluation. </returns>
		public override sealed Result Evaluate()
		{
			// Obtain the bone indices we'll need.
			int wristBone = (int)Kinect.NuiSkeletonPositionIndex.WristLeft + (int)side,
			chestBone = (int)Kinect.NuiSkeletonPositionIndex.ShoulderCenter;
			
			// The chest is a little too high.
			float heightOffset = 0.25f;
			
			// The wrist must be above the chest.
			if (kinect.bonePos [skeleton, chestBone].y - heightOffset < kinect.bonePos [skeleton, wristBone].y) 
			{
				// The velocity of the hand must be lower than the tolerance.
				float wristVelocity = kinect.boneVel [skeleton, wristBone].z;
				
				if (wristVelocity > tolerance) 
				{
					m_result.magnitude = (wristVelocity - tolerance) * Time.deltaTime;
					
					return m_result;
				}
			}
			
			// Return nothing if we have failed.
			return null;
		}
	}
}