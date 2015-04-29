using UnityEngine;


namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture component which checks if a hand is above the head.
	/// </summary>
	public sealed class HandOverHeadComponent : SideComponent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MIG.Gesture.HandoverHeadComponent"/> class.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		/// <param name="side"> The side of the skeleton to check. </param>
		public HandOverHeadComponent (SkeletonWrapper kinect, Side side)
		{
			this.kinect = kinect;
			this.side   = side;
		}
		
		
		/// <summary>
		/// Examines the angle and position of the arm to check if the hand is above the head.
		/// </summary>
		/// <returns> The result of the evaluation. </returns>
		public override sealed Result Evaluate()
		{
			// We need the index of each bone to test.
			int offset = (int) side,
			    head   = (int) Kinect.NuiSkeletonPositionIndex.Head,
			    hand   = (int) Kinect.NuiSkeletonPositionIndex.HandLeft + offset;

			// The hand must be above the head.
			if (kinect.bonePos[skeleton, hand].y > kinect.bonePos[skeleton, head].y)
			{
				// We probably won't use the magnitude for this.
				m_result.magnitude = kinect.boneVel[skeleton, hand].y;

				return m_result;
			}

			// We have failed my lord.
			return null;
		}
	}
}