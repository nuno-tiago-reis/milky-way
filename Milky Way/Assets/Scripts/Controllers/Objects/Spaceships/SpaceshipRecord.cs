namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements a spaceship configuration.
	/// </summary>
	public sealed class SpaceshipRecord
	{
		#region [Attributes]
		/// <summary>
		/// Gets or sets the spaceship identifier.
		/// </summary>
		/// <value>
		/// The spaceship identifier.
		/// </value>
		public int SpaceshipID;

		/// <summary>
		/// The spaceships current standing in the race.
		/// </summary>
		public int CurrentStanding;

		/// <summary>
		/// The spaceships current lap in the race.
		/// </summary>
		public int CurrentLap;
		/// <summary>
		/// The spaceships current checkpoint in the race.
		/// </summary>
		public int CurrentCheckpoint;

		/// <summary>
		/// The spaceships lap time in the current lap.
		/// </summary>
		public float CurrentLapTime;
		/// <summary>
		/// The spaceships best lap time in the current race.
		/// </summary>
		public float BestLapTime;
		/// <summary>
		/// The spaceships total lap time in the current race.
		/// </summary>
		public float TotalLapTime;
		#endregion

		#region [Methods]
		/// <summary>
		/// Initializes a new instance of the <see cref="SpaceshipRecord"/> class.
		/// </summary>
		/// 
		/// <param name="spaceship">The spaceship.</param>
		public SpaceshipRecord(SpaceshipController spaceship)
		{
			this.SpaceshipID = spaceship != null ? spaceship.ID : 0;
		}
		#endregion
	}
}