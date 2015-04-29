namespace MIG.GestureRecognition
{
	/// <summary>
	/// A gesture which checks if both arms are above the head.
	/// </summary>
	public sealed class ArmsOverHead : Gesture 
	{
		/// <summary>
		/// Constructs the ArmsOverHead gesture.
		/// </summary>
		/// <param name="kinect"> The SkeletonWrapper object used to obtain information on Kinect. </param>
		public ArmsOverHead (SkeletonWrapper kinect) : base (GestureType.ArmsOverHead)
		{
			// This only has one stage and two components.
			Stage stage = new Stage();
			stage.AddComponent (new HandOverHeadComponent (kinect, SideComponent.Side.Left));
			stage.AddComponent (new HandOverHeadComponent (kinect, SideComponent.Side.Right));
			
			// The user must cross both arms at the same time.
			stage.mode = Stage.Mode.And;

			m_stages.Add (stage);
		}
	}
}