namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture which checks if either hand is pulling towards the chest.
	/// </summary>
	public sealed class TwoHandedPull : Gesture 
	{
		/// <summary>
		/// Constructs the TwoHandedPull gesture.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		public TwoHandedPull (SkeletonWrapper kinect) : base (GestureType.TwoHandedPull)
		{
			// This only has one stage and two components.
			Stage stage = new Stage();
			stage.AddComponent (new ArmPullComponent (kinect, SideComponent.Side.Left));
			stage.AddComponent (new ArmPullComponent (kinect, SideComponent.Side.Right));

			// The user must pull with both hands at the same time.
			stage.mode = Stage.Mode.And;
			
			m_stages.Add (stage);
		}
	}
}