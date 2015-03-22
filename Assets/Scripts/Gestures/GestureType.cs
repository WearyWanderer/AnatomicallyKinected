/// <summary>
/// A representation of every Gesture recognised by the application.
/// </summary>
public enum GestureType
{
	None          = 0, //!< Represents nothing being detected.
	HandSwipe     = 1, //!< A swipe of either the left or right hand.
	TwoHandedPull = 2, //!< A physical pull gesture which requires the use of both hands.
	TwoHandedPush = 3, //!< A physical push gesture which requires the use of both hands.
	ArmsCrossed   = 4  //!< The arms are crossed in front of the users chest.
}