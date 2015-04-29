using System.Collections.Generic;


namespace MIG.GestureRecognition
{
	/// <summary>
	/// A stage which makes up a gesture, this could have multiple components to it which may be required
	/// to evaluate any/all to true for the stage to end.
	/// </summary>
	public sealed class Stage
	{
		#region Enums

		/// <summary>
		/// An enum which indicates the evaluation mode to use for the stage.
		/// </summary>
		public enum Mode
		{
			And = 0, //!< All components must evaluate to true for the stage to be complete.
			Or  = 1  //!< Requires at least one component to evaluate to true.
		}

		#endregion


		#region Implementation data
		
		private List<Component>  m_components   = new List<Component>(0); //!< A list of components that make up a stage of a Gesture.
		private Mode             m_mode         = Mode.And;               //!< The mode to use for evaluating the stage.
		private float            m_timeForStage = 0f;                     //!< How long the stage is allowed to take to be recognised, used externally.
		private Component.Result m_result       = new Component.Result(); //!< Avoid constant memory allocation by caching a Result object.

		#endregion


		#region Operators

		/// <summary>
		/// Does the object exist?
		/// </summary>
		public static implicit operator bool (Stage stage)
		{
			return stage != null;
		}

		#endregion


		#region Getters, setters and properties

		/// <summary>
		/// Gets or sets the mode to be used during the evaluation of the stage.
		/// </summary>
		/// <value> The mode currently in use. </value>
		public Mode mode
		{
			get { return m_mode; }
			set { m_mode = value; }
		}


		/// <summary>
		/// Gets or sets How long the stage is allowed to take to be recognised.
		/// </summary>
		/// <value> A time value, this must be higher than 0. </value>
		public float timeForStage
		{
			get { return m_timeForStage; }
			set
			{
				if (value >= 0f)
				{
					m_timeForStage = value;
				}
			}
		}


		/// <summary>
		/// Adds a Component to be tracked by the Stage.
		/// </summary>
		/// <param name="component"> The component to be tracked. </param>
		public void AddComponent (Component component)
		{
			// Ignore nulls.
			if (component)
			{
				m_components.Add (component);
			}
		}


		/// <summary>
		/// Removes a component from the list of tracked components.
		/// </summary>
		/// <returns> Whether the removal was successful. </returns>
		/// <param name="component"> The component to be removed. </param>
		public bool RemoveComponent (Component component)
		{
			return m_components.Remove (component);
		}


		/// <summary>
		/// Update the cached result object.
		/// </summary>
		/// <param name="evaluation"> Whether the evaluation was successful. </param>
		/// <param name="magnitude"> The magnitude to set the Result object to. </param>
		/// <returns> The Result object. </returns>
		private Component.Result UpdateResult (bool evaluation, float magnitude)
		{
			m_result.evaluation = evaluation;
			m_result.magnitude  = magnitude;

			return m_result;
		}

		#endregion


		#region Evaluation

		/// <summary>
		/// Evaluates the gesture stage to check whether the stage has completed or not.
		/// </summary>
		public Component.Result Evaluate()
		{
			switch (m_mode)
			{
				case Mode.Or:
					return EvaluateAny();

				case Mode.And:
					return EvaluateAll();

				default:
					throw new System.NotSupportedException ("Case doesn't exist in MIG.Gesture.Stage.Evaluate(), " + m_mode.ToString());
			}
		}


		/// <summary>
		/// Checks every component to see if any evaluate to true.
		/// </summary>
		/// <returns> The result of the evaluation. </returns>
		private Component.Result EvaluateAny()
		{
			// Loop through each component.
			foreach (Component component in m_components)
			{
				// Evaluate the component and check if the result is true.
				Component.Result result = component.Evaluate();

				if (result && result.evaluation)
				{
					return UpdateResult (true, result.magnitude);
				}
			}

			// We have failed my lord!
			return UpdateResult (false, 0f);
		}


		/// <summary>
		/// Checks every components to see if they all evaluate to true, if one is false then the rest of the components are skipped.
		/// </summary>
		/// <returns> The result of the evaluation. Null if </returns>
		private Component.Result EvaluateAll()
		{
			// Accumlate the magnitudes of each component.
			float magnitude = 0f;

			// Check every component unless one evaluates to false.
			foreach (Component component in m_components)
			{
				Component.Result result = component.Evaluate();

				if (!result || !result.evaluation)
				{
					// Skip the rest of the components since all are required to be true.
					return UpdateResult (false, 0f);
				}

				// Increase the accumulator.
				magnitude += result.magnitude;
			}

			// Success!!!!!!
			return UpdateResult (true, magnitude);
		}

		#endregion
	}
}