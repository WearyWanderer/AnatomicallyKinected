using System.Collections.Generic;
using UnityEngine;

using MIG.GestureRecognition;


/// <summary>
/// Contains code relating to the MIG module.
/// </summary>
namespace MIG
{
	
	/// <summary>
	/// A class used to monitor and manage every Kinect gesture recognised by the application.
	/// </summary>
	[RequireComponent (typeof (SkeletonWrapper))]
	public sealed class GestureManager : MonoBehaviour
	{
		#region Implementation data

		private List<Gesture> m_gestures  = new List<Gesture>(0); //!< A list of each Gesture being tracked by the GestureManager.

		private	GestureType   m_current   = GestureType.None;     //!< The currently recognised GestureType.
		private float         m_magnitude = 0f;                   //!< The magnitude of the velocity of the recognised Gesture.
		private float         m_timeTaken = 0f;                   //!< The amount of time it took to recognise the current gesture.

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
		
		
		/// <summary>
		/// Gets the time it took to recognise the current gesture.
		/// </summary>
		/// <value> A time value in seconds. If  </value>
		public float timeTaken
		{
			get { return m_timeTaken; }
		}

		#endregion


		#region Behaviour functionality

		/// <summary>
		/// Initialises the GestureManager, adding each tracked Gesture to the internal list.
		/// </summary>
		private void Awake()
		{
			// Get our SkeletonWrapper so we can construct some gestures.
			SkeletonWrapper kinect = GetComponent<SkeletonWrapper>();
		
			// Add the tracked gestures to the list.
			m_gestures.Add (new OneHandSwipe (kinect));
			m_gestures.Add (new TwoHandedPull (kinect));
			m_gestures.Add (new TwoHandedPush (kinect));
			m_gestures.Add (new ArmsOverHead (kinect));
			
			// Give the event handler of each gesture a function to use.
			foreach (Gesture gesture in m_gestures)
			{
				gesture.OnRecognised += OnGestureRecognised;
			}
		}
		
		
		/// <summary>
		/// Updates each Gesture, evaluating whether they have been recognised.
		/// </summary>
		private void Update()
		{
			// Reset the current gesture.
			m_current   = GestureType.None;
			m_magnitude = 0f;
			m_timeTaken = 0f;
		
			// We need to get each gesture to evaluate itself by calling update.
			foreach (Gesture gesture in m_gestures)
			{
				gesture.Update (Time.deltaTime);
			}
		}

		#endregion
		
		
		#region Event management

		private void OnGestureRecognised (object sender, GestureRecognisedEventArgs args)
		{
			m_current   = args.type;
			m_magnitude = args.magnitude;
			m_timeTaken = args.timeTaken;
		}
		
		#endregion
	}
}