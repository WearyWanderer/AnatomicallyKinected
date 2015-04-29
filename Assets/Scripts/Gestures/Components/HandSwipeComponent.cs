using UnityEngine;


namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture component which checks for a left or right handed swipe.
	/// </summary>
	public sealed class HandSwipeComponent : SideComponent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MIG.Gesture.HandSwipeComponent"/> class.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		/// <param name="side"> The side of the skeleton to check. </param>
		/// <param name="tolerance"> How fast the hand must move in units per second to be detected. </param>
		public HandSwipeComponent (SkeletonWrapper kinect, Side side, float tolerance = 2.0f)
		{
			this.kinect    = kinect;
			this.side      = side;
			this.tolerance = tolerance;
		}


		/// <summary>
		/// Examines the velocity of the desired hand to see if it's making a swiping motion.
		/// </summary>
		/// <returns> The result of the evaluation. </returns>
		public override sealed Result Evaluate()
		{
			// Obtain the bone indices we'll need.
			int chestBone = (int) Kinect.NuiSkeletonPositionIndex.ShoulderCenter;
			int handBone  = (int) Kinect.NuiSkeletonPositionIndex.HandLeft + (int) side;
		
			// The hand must be in front of the chest.
			if (kinect.bonePos[skeleton, chestBone].z < kinect.bonePos[skeleton, handBone].z)
			{
				// The velocity of the hand must be higher than the tolerance.
				float handVelocity = kinect.boneVel[skeleton, handBone].x;
				
				if (Mathf.Abs (handVelocity) >= tolerance)
				{
					m_result.magnitude  = (handVelocity - tolerance) * Time.deltaTime;
					
					return m_result;
				}
			}
			
			// Return nothing if we have failed.
			return null;
		}
	}
}