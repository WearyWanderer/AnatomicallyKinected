using System;
using System.Collections.Generic;


/// <summary>
/// Contains all Gesture recognition code.
/// </summary>
namespace MIG.GestureRecognition
{
	/// <summary>
	/// An encapsulation of individual stages and components that make up a single recognised gesture.
	/// </summary>
	public class Gesture
	{
		#region Implementation data

		protected List<Stage> m_stages      = new List<Stage>(0); //!< A list of stages which make up the gesture.
		protected int         m_stage       = 0;                  //!< The current stage of the gesture.

		protected GestureType m_type        = GestureType.None;   //!< The type of the gesture.
		protected float       m_magnitude   = 0f;                 //!< The magnitude of the gesture, if applicable.

		protected float       m_timeTaken   = 0f;                 //!< How long it has taken for the gesture to be performed, in seconds.
		protected float       m_timeOnStage = 0f;                 //!< The time spent on the current gesture.

		#endregion


		#region Events

		/// <summary>
		/// Occurs when the Gesture is recognised.
		/// </summary>
		public event EventHandler<GestureRecognisedEventArgs> OnRecognised;

		#endregion


		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Gesture class.
		/// </summary>
		/// <param name="type"> The type of the Gesture, this probably shouldn't be GestureType.None. </param>
		public Gesture (GestureType type)
		{
			m_type = type;
		}

		#endregion


		#region Operators

		/// <summary>
		/// Does the object exist?
		/// </summary>
		public static implicit operator bool (Gesture gesture)
		{
			return gesture != null;
		}

		#endregion


		#region Getters, setters and properties

		/// <summary>
		/// Gets the GestureType of the Gesture.
		/// </summary>
		/// <value> The GestureType. </value>
		public GestureType type
		{
			get { return m_type; }
		}


		/// <summary>
		/// Gets the magnitude of the gesture, this may not be applicable.
		/// </summary>
		/// <value> The value of the magnitude. </value>
		public float magnitude
		{
			get { return m_magnitude; }
		}


		/// <summary>
		/// Gets the time taken to recognise the Gesture.
		/// </summary>
		/// <value> The time value. </value>
		public float timeTaken
		{
			get { return m_timeTaken; }
		}


		/// <summary>
		/// Gets the number of stages that make up the gesture.
		/// </summary>
		/// <value> The total number of stages. </value>
		public int stageCount
		{
			get { return m_stages.Count; }
		}


		/// <summary>
		/// Add a stage onto the end of the tracked stages.
		/// </summary>
		/// <param name="stage"> The stage to be added. </param>
		public void AddStage (Stage stage)
		{
			if (stage)
			{
				m_stages.Add (stage);
			}
		}


		/// <summary>
		/// Inserts the stage at the specified index.
		/// </summary>
		/// <param name="index"> The index to add the stage at. </param>
		/// <param name="stage"> The stage to be added. </param>
		public void InsertStage (int index, Stage stage)
		{
			if (stage && index > 0 && index <= m_stages.Count)
			{
				m_stages.Insert (index, stage);
			}
		}


		/// <summary>
		/// Removes the stage at the specified index.
		/// </summary>
		/// <param name="index"> This must be valid otherwise nothing will happen. </param>
		public void RemoveStage (int index)
		{
			if (index < m_stages.Count && index >= 0)
			{
				m_stages.RemoveAt (index);
			}
		}


		/// <summary>
		/// Removes the desired stage from the list of tracked stages.
		/// </summary>
		/// <param name="stage"> The stage to be removed. </param>
		public void RemoveStage (Stage stage)
		{
			if (stage)
			{
				m_stages.Remove (stage);
			}
		}

		#endregion


		#region Public interface

		/// <summary>
		/// Updates the Gesture, this will attempt to recognise the Gesture and trigger the OnGestureRecognised event.
		/// </summary>
		/// <param name="deltaTime"> How much time has passed, in seconds, since the last update. </param>
		public void Update (float deltaTime)
		{
			// Pre-condition: No point doing anything if there are no stages.
			int stages    = m_stages.Count,
			    lastStage = stages - 1;

			if (stages > 0)
			{
				// Update the timer with the delta value.
				m_timeOnStage += deltaTime;
				m_timeTaken   += deltaTime;

				// We need to evaluate the current stage and any others if the current stage evaluates to true.
				bool checkStage = true;
				while (checkStage)
				{
					// Obtain the result and check if we should move on.
					Component.Result result = m_stages[m_stage].Evaluate();

					if (result.evaluation)
					{
						m_magnitude += result.magnitude;

						// If we're at the last stage then trigger the gesture and finish.
						if (m_stage == lastStage)
						{
							OnRecognised (this, new GestureRecognisedEventArgs (m_type, m_magnitude, timeTaken));							
							Reset();
							
							checkStage = false;
						}

						// Increment the counter.
						else
						{
							m_stage       = ++m_stage % stages;
							m_timeOnStage = 0f;
						}
					}

					// Break out of the loop when the evaluation fails and wait for the next update.
					else
					{
						checkStage = false;
					}
				}

				// Reset the current stage if we've taken too long.
				if (m_timeOnStage > m_stages[m_stage].timeForStage)
				{
					Reset();
				}
			}
		}

		#endregion


		#region Internal workings

		/// <summary>
		/// Reset the current stage index, the accumlated magnitude, the time spent on the current stagge and the 
		/// total time taken for the gesture.
		/// </summary>
		protected void Reset()
		{
			m_stage       = 0;
			m_magnitude   = 0f;
			m_timeOnStage = 0f;
			m_timeTaken   = 0f;
		}

		#endregion
	}
}