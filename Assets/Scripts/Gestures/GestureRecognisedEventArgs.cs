namespace MIG
{
	/// <summary>
	/// The arguments for GestureRecognised events.
	/// </summary>
	public sealed class GestureRecognisedEventArgs : System.EventArgs
	{
		public GestureType type      = GestureType.None; //!< The type of gesture being recognised.
		public float       magnitude = 0f;               //!< The magnitude, if applicable, of the gesture.
		public float       timeTaken = 0f;               //!< How long it has taken to recognise the gesture.


		/// <summary>
		/// Initializes a new instance of the <see cref="MIG.GestureRecognisedEventArgs"/> class.
		/// </summary>
		/// <param name="type"> The type of the gesture recognised. </param>
		/// <param name="magnitude"> The magnitude, if applicable, of the gesture recognised. </param>
		/// <param name="timeTaken"> How long it took to recognise the gesture. </param>
		public GestureRecognisedEventArgs (GestureType type = GestureType.None, float magnitude = 0f, float timeTaken = 0f)
		{
			this.type      = type;
			this.magnitude = magnitude;
			this.timeTaken = timeTaken;
		}


		/// <summary>
		/// Does the object exist?
		/// </summary>
		public static implicit operator bool (GestureRecognisedEventArgs args)
		{
			return args != null;
		}
	}
}