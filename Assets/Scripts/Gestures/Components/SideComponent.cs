namespace MIG.GestureRecognition
{
	/// <summary>
	/// A base class for components which can be performed on either the right or left side of the skeleton.
	/// </summary>
	public abstract class SideComponent : Component
	{
		#region Enums

		/// <summary>
		/// An enumeration of the desired side of the skeleton to check when evaluating the component. The values of
		/// the enumerations correspond to the global index offset of the implementation.
		/// </summary>
		public enum Side
		{
			Left  = 0, //!< Evaluate the left side.
			Right = 4  //!< Evaluate the right side.
		}

		#endregion


		#region Implementation data

		protected Side m_side = Side.Left; //!< The side of the skeleton to check.

		#endregion


		#region Side-specific functionality

		/// <summary>
		/// Gets or sets the side of the skeleton being evaluated.
		/// </summary>
		/// <value> The desired side to evaluate. </value>
		public Side side
		{
			get { return m_side; }
			set { m_side = value; }
		}

		#endregion
	}
}