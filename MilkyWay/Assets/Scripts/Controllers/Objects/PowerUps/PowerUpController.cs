// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Utility;
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Abstract class representing a power-up controller.
	/// </summary>
	public abstract class PowerUpController : MonoBehaviour, IPowerUpController
	{
		#region [Attributes]
		/// <inheritdoc />
		public float Duration { get; set; }
		/// <inheritdoc />
		public Timer DurationTimer { get; set; }

		/// <summary>
		/// The power-ups source spaceship.
		/// </summary>
		public SpaceshipController Spaceship { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public virtual void ObjectCreate()
		{
			// Initialize the timer
			this.DurationTimer = new Timer(TimerMode.Countdown, this.Duration);

			// Update the parent
			PowerUpManager.Instance.AddPowerUp(this);
		}

		/// <inheritdoc />
		public virtual void ObjectUpdate()
		{
			// Update the timer
			this.DurationTimer.Update();
			// The timer ran out
			if(this.DurationTimer.Finished)
				ObjectDestroy();
		}

		/// <inheritdoc />
		public virtual void ObjectDestroy()
		{
			Destroy(this.gameObject);

			// Update the parent
			PowerUpManager.Instance.RemovePowerUp(this);
		}

		/// <inheritdoc />
		public virtual bool Activate()
		{
			if (this.DurationTimer.Running == false)
			{
				this.DurationTimer.Start();

				return true;
			}

			return false;
		}
		#endregion
	}
}