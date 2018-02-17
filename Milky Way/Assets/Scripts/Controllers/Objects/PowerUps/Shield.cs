// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Implements the shield power-up.
	/// </summary>
	/// 
	/// <seealso cref="PowerUp" />
	public sealed class Shield : PowerUp
	{
		#region [Constants and Statics]
		/// <summary>
		/// The default Health.
		/// </summary>
		public const float DefaultHealth = 500.0f;
		/// <summary>
		/// The default duration.
		/// </summary>
		public const float DefaultDuration = 15.0f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The shields Health.
		/// </summary>
		public float Health { get; private set; }
		/// <summary>
		/// The shields duration.
		/// </summary>
		public float Duration { get; private set; }

		/// <summary>
		/// The shields power-up controller.
		/// </summary>
		public ShieldController ShieldController
		{
			get; private set;
		}
		/// <inheritdoc />
		public override PowerUpController PowerUpController
		{
			get { return this.ShieldController; }
		}
		#endregion

		#region [Methods]
		/// <inheritdoc/>
		public override void PowerUpCreate()
		{
			base.PowerUpCreate();

			// Initialize the shields properties
			this.Health = DefaultHealth;
			this.Duration = DefaultDuration;
		}

		/// <inheritdoc/>
		public override void PowerUpUpdate()
		{
			base.PowerUpUpdate();
		}

		/// <inheritdoc/>
		public override void PowerUpDestroy()
		{
			base.PowerUpDestroy();
		}

		/// <inheritdoc/>
		public override bool Activate(SpaceshipController spaceship)
		{
			if (this.Active == false)
			{
				// Update the flag
				this.Active = true;
				// Update the spaceship
				this.Spaceship = spaceship;

				// Create the power-up
				this.ShieldController = ResourceManager.LoadGameObject<ShieldController>(string.Format("Objects/PowerUps/{0}", this.Name));
				this.ShieldController.transform.localRotation = Quaternion.identity;
				this.ShieldController.transform.position = Vector3.zero;
				this.ShieldController.Spaceship = this.Spaceship;
				this.ShieldController.Duration = this.Duration;
				this.ShieldController.Health = this.Health;

				// Initialize the shields controller
				this.ShieldController.ObjectCreate();

				return this.ShieldController.Activate();
			}

			return false;
		}
		#endregion
	}
}