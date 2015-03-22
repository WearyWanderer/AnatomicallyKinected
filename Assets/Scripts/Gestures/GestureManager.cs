using UnityEngine;
using System.Collections;


/// <summary>
/// A class used to monitor and manage every Kinect gesture recognised by the application.
/// </summary>
public sealed class GestureManager : MonoBehaviour
{
	#region Implementation data

	private	GestureType m_current   = GestureType.None; //!< The currently recognised GestureType.
	private float       m_magnitude = 0f;               //!< The magnitude of the velocity of the recognised Gesture.

	#endregion


	#region Getters, setters and properties

	/// <summary>
	/// Gets the currently recognised GestureType.
	/// </summary>
	/// <value> An enum representing the GestureType the user is performing. </value>
	public GestureType currentGesture
	{
		get { return m_current; }
	}


	/// <summary>
	/// Gets magnitude of the current GestureType. This can be used to manipulate the effects of the GestureType.
	/// </summary>
	/// <value> A positive or negative value. This could also be zero. </value>
	public float gestureMagnitude
	{
		get { return m_magnitude; }
	}

	#endregion
}
