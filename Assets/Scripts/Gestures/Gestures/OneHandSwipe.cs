namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture which checks if either hand is performing a swiping motion.
	/// </summary>
	public sealed class OneHandSwipe : Gesture 
	{
		/// <summary>
		/// Constructs the OneHandSwipe gesture.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		public OneHandSwipe (SkeletonWrapper kinect) : base (GestureType.OneHandSwipe)
		{
			// This only has one stage and two components.
			Stage stage = new Stage();
			stage.AddComponent (new HandSwipeComponent (kinect, SideComponent.Side.Left));
			stage.AddComponent (new HandSwipeComponent (kinect, SideComponent.Side.Right));
			
			// The user can swipe either hand but only one will be registered.
			stage.mode = Stage.Mode.Or;
			
			m_stages.Add (stage);
		}
	}
}