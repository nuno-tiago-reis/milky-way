// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Implements the Smokescreen power-up.
	/// </summary>
	/// 
	/// <seealso cref="PowerUp" />
	public sealed class Smokescreen : PowerUp
	{
		#region [Constants and Statics]
		/// <summary>
		/// The default radius.
		/// </summary>
		public const float DefaultRadius = 50.0f;
		/// <summary>
		/// The default duration.
		/// </summary>
		public const float DefaultDuration = 25.0f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The smokescreens radius.
		/// </summary>
		public float Radius { get; private set; }
		/// <summary>
		/// The smokescreens duration.
		/// </summary>
		public float Duration { get; private set; }

		/// <summary>
		/// The smokescreens power-up controller.
		/// </summary>
		public SmokescreenController SmokescreenController
		{
			get; private set;
		}
		/// <inheritdoc />
		public override PowerUpController PowerUpController
		{
			get { return this.SmokescreenController; }
		}
		#endregion

		#region [Methods]
		/// <inheritdoc/>
		public override void PowerUpCreate()
		{
			base.PowerUpCreate();

			// Initialize the smokescreens properties
			this.Radius = DefaultRadius;
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
				this.SmokescreenController = ResourceManager.LoadGameObject<SmokescreenController>(string.Format("Objects/PowerUps/{0}", this.Name));
				this.SmokescreenController.transform.localRotation = Quaternion.identity;
				this.SmokescreenController.transform.position = Vector3.zero;
				this.SmokescreenController.Spaceship = this.Spaceship;
				this.SmokescreenController.Duration = this.Duration;
				this.SmokescreenController.Radius = this.Radius;

				// Initialize the smokescreens controller
				this.SmokescreenController.ObjectCreate();

				return this.SmokescreenController.Activate();
			}

			return false;
		}
		#endregion
	}
}