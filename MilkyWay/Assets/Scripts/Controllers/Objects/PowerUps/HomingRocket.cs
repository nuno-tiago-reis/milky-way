// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Implements the HomingRocket power-up.
	/// </summary>
	/// 
	/// <seealso cref="PowerUp" />
	public sealed class HomingRocket : PowerUp
	{
		#region [Constants and Statics]
		/// <summary>
		/// The default damage.
		/// </summary>
		public const float DefaultDamage = 150.0f;
		/// <summary>
		/// The default force.
		/// </summary>
		public const float DefaultForce = 1.0f;
		/// <summary>
		/// The default duration.
		/// </summary>
		public const float DefaultDuration = 90.0f;
		/// <summary>
		/// The default setup duration.
		/// </summary>
		public const float DefaultSetupDuration = 1.5f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The homing-rockets damage.
		/// </summary>
		public float Damage { get; private set; }
		/// <summary>
		/// The homing-rockets force.
		/// </summary>
		public float Force { get; private set; }
		/// <summary>
		/// The homing-rockets duration.
		/// </summary>
		public float Duration { get; private set; }
		/// <summary>
		/// The homing-rockets setup duration.
		/// </summary>
		public float SetupDuration { get; private set; }

		/// <summary>
		/// The homing-rocket power-up controller.
		/// </summary>
		public HomingRocketController HomingRocketController
		{
			get; private set;
		}
		/// <inheritdoc />
		public override PowerUpController PowerUpController
		{
			get { return this.HomingRocketController; }
		}
		#endregion

		#region [Methods]
		/// <inheritdoc/>
		public override void PowerUpCreate()
		{
			base.PowerUpCreate();

			// Initialize the homing-rockets properties
			this.Damage = DefaultDamage;
			this.Force = DefaultForce;
			this.Duration = DefaultDuration;
			this.SetupDuration = DefaultSetupDuration;
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
			if(this.Active == false)
			{
				// Update the flag
				this.Active = true;
				// Update the spaceship
				this.Spaceship = spaceship;

				// Create the power-up
				this.HomingRocketController = ResourceManager.LoadGameObject<HomingRocketController>(string.Format("Objects/PowerUps/{0}", this.Name));
				this.HomingRocketController.transform.localRotation = Quaternion.identity;
				this.HomingRocketController.transform.position = Vector3.zero;
				this.HomingRocketController.Spaceship = this.Spaceship;
				this.HomingRocketController.Damage = this.Damage;
				this.HomingRocketController.Speed = this.Force;
				this.HomingRocketController.Duration = this.Duration;
				this.HomingRocketController.SetupDuration = this.SetupDuration;

				// Initialize the homing-rockets controller
				this.HomingRocketController.ObjectCreate();

				return this.HomingRocketController.Activate();
			}

			return false;
		}
		#endregion
	}
}