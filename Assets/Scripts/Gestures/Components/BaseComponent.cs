namespace MIG.GestureRecognition
{
	/// <summary>
	/// A base class for all components that make up a detected Gesture.
	/// </summary>
	public abstract class Component
	{
		#region Evaluation result

		/// <summary>
		/// A result of a component evaluation, pairs a success flag and magnitude together.
		/// </summary>
		public sealed class Result
		{
			public bool  evaluation = true; //!< Whether the evaluation was successful or not.
			public float magnitude  = 0f;   //!< The magnitude of the component, this may not be applicable.


			/// <summary>
			/// Initializes a new instance of the Result class.
			/// </summary>
			/// <param name="evaluation"> Indicates whether the component evaluated to true. </param>
			/// <param name="magnitude"> Any applicable magnitude of the component. </param>
			public Result (bool evaluation = true, float magnitude = 0f)
			{
				this.evaluation = evaluation;
				this.magnitude  = magnitude;
			}
			
			
			/// <summary> 
			/// Does the object exist? 
			/// </summary>
			public static implicit operator bool (Result result)
			{
				return result != null;
			}
		}

		#endregion


		#region Implementation data

		protected SkeletonWrapper m_kinect    = null;         //!< A refernece to the SkeletonWrapper object used to check the Kinect system.
		protected int             m_skeleton  = 0;            //!< The ID of the skeleton to check.
		protected float           m_tolerance = 0f;           //!< Any tolerance to use during evaluations to avoid subtle movements triggering components.
		protected Result          m_result    = new Result(); //!< We cache the Result object to avoid run-time memory allocation.

		#endregion


		#region Operators
		
		/// <summary> 
		/// Does the object exist? 
		/// </summary>
		public static implicit operator bool (Component component)
		{
			return component != null;
		}

		#endregion


		#region Properties and abstract methods
		
		/// <summary>
		/// Gets or sets the SkeletonWrapper used to obtain Kinect information.
		/// </summary>
		/// <value> The SkeletonWrapper currently in use. </value>
		public SkeletonWrapper kinect
		{
			get { return m_kinect; }
			set { m_kinect = value; }
		}

		/// <summary>
		/// Gets or sets the ID of the skeleton to check. Uses array notation so the ID starts at 0.
		/// </summary>
		/// <value> The current ID value. </value>
		public int skeleton
		{
			get { return m_skeleton; }
			set
			{
				if (value < 2 && value >= 0)
				{
					m_skeleton = value;
				}
			}
		}


		/// <summary>
		/// Gets or sets the tolerance of the component. This effects the required velocity for the component to evalutate to true.
		/// </summary>
		/// <value> The current tolerance value. </value>
		public float tolerance
		{
			get { return m_tolerance; }
			set 
			{
				if (value >= 0f)
				{
					m_tolerance = value;
				}
			}
		}


		/// <summary>
		/// Evaluate the component and retrieve the result.
		/// </summary>
		public abstract Result Evaluate();

		#endregion
	}
}