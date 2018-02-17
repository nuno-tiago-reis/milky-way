// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Abstract class representing a power-up that can be picked up by a SpaceshipController.
	/// </summary>
	public abstract class PowerUp : IPowerUp
	{
		#region [Attributes]
		/// <inheritdoc />
		public string Name { get; protected set; }
		/// <inheritdoc />
		public bool Active { get; protected set; }
		/// <inheritdoc />
		public abstract PowerUpController PowerUpController { get; }

		/// <summary>
		/// The power-ups source spaceship.
		/// </summary>
		public SpaceshipController Spaceship;
		#endregion

		#region [Methods]
		/// <inheritdoc/>
		public virtual void PowerUpCreate()
		{
			this.Name = this.GetType().Name;
		}

		/// <inheritdoc/>
		public virtual void PowerUpUpdate()
		{
			if (this.Active)
			{
				// Update the power-up
				if(this.PowerUpController != null)
					this.PowerUpController.ObjectUpdate();
				// Destroy the power-up
				if(this.PowerUpController == null || this.PowerUpController.DurationTimer.Finished)
					PowerUpDestroy();
			}
		}

		/// <inheritdoc/>
		public virtual void PowerUpDestroy()
		{
			// Destroy the power-up
			if(this.PowerUpController != null)
				this.PowerUpController.ObjectDestroy();
			// Remove the power-up
			this.Spaceship.RemovePowerUp(this);
		}

		/// <inheritdoc/>
		public abstract bool Activate(SpaceshipController spaceship);
		#endregion
	}
}