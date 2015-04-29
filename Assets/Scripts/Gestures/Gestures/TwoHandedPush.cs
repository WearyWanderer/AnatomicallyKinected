namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture which checks if either hand is pushing away from the chest.
	/// </summary>
	public sealed class TwoHandedPush : Gesture 
	{
		/// <summary>
		/// Constructs the TwoHandedPush gesture.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		public TwoHandedPush (SkeletonWrapper kinect) : base (GestureType.TwoHandedPush)
		{
			// This only has one stage and two components.
			Stage stage = new Stage();
			stage.AddComponent (new ArmPushComponent (kinect, SideComponent.Side.Left));
			stage.AddComponent (new ArmPushComponent (kinect, SideComponent.Side.Right));
			
			// The user must push with both hands at the same time.
			stage.mode = Stage.Mode.And;
			
			m_stages.Add (stage);
		}
	}
}