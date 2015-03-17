using UnityEngine;
using System.Collections;


/// <summary>
/// A class used to monitor and manage every Kinect gesture recognised by the application.
/// </summary>
public sealed class GestureManager : MonoBehaviour
{
	#region Implementation data

	private	Gesture m_current   = Gesture.None; //!< The currently recognised Gesture.
	private float   m_magnitude = 0f;           //!< The magnitude of the velocity of the recognised Gesture.

	#endregion


	#region Getters, setters and properties

	/// <summary>
	/// Gets the currently recognised Gesture.
	/// </summary>
	/// <value> An enum representing the Gesture the user is performing. </value>
	public Gesture currentGesture
	{
		get { return m_current; }
	}


	/// <summary>
	/// Gets magnitude of the current Gesture. This can be used to manipulate the effects of the Gesture.
	/// </summary>
	/// <value> A positive or negative value. This could also be zero. </value>
	public float gestureMagnitude
	{
		get { return m_magnitude; }
	}

	#endregion
}
