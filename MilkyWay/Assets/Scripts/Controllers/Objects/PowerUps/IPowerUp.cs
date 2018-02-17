// MilkyWay
using MilkyWay.Objects.PowerUps;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay
{
	/// <summary>
	/// Defines the necessary methods to implement a PowerUp.
	/// 
	/// These methods are necessary to control the PowerUps Lifecycle.
	/// These events are used to initialized, update and destroy the PowerUp.
	/// </summary>
	public interface IPowerUp
	{
		#region [Attributes]
		/// <summary>
		/// The power-ups name.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Whether the power-up is active.
		/// </summary>
		bool Active { get; }
		/// <summary>
		/// The power-ups controller.
		/// </summary>
		PowerUpController PowerUpController { get; }
		#endregion

		#region [Method]
		/// <summary>
		/// Creates the PowerUp.
		/// </summary>
		void PowerUpCreate();
		/// <summary>
		/// Updates the PowerUp.
		/// </summary>
		void PowerUpUpdate();
		/// <summary>
		/// Destroys the PowerUp.
		/// </summary>
		void PowerUpDestroy();

		/// <summary>
		/// Activates the PowerUp.
		/// </summary>
		/// 
		/// <param name="spaceship">The spaceship activating the PowerUp.</param>
		bool Activate(SpaceshipController spaceship);
		#endregion
	}
}