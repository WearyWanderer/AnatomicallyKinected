namespace MIG
{
	/// <summary>
	/// A representation of every Gesture recognised by the application.
	/// </summary>
	public enum GestureType
	{
		None          = 0, //!< Represents nothing being detected.
		OneHandSwipe  = 1, //!< A swipe of either the left or right hand.
		TwoHandedPull = 2, //!< A physical pull gesture which requires the use of both hands.
		TwoHandedPush = 3, //!< A physical push gesture which requires the use of both hands.
		ArmsOverHead  = 4  //!< The arms are above the users head.
	}
}