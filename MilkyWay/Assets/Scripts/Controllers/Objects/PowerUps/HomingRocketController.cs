// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Utility;
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Implements the HomingRocket Controller.
	/// </summary>
	/// 
	/// <seealso cref="PowerUpController" />
	public sealed class HomingRocketController : PowerUpController
	{
		#region [Attributes]
		/// <summary>
		/// The homing-rockets damage.
		/// </summary>
		public float Damage { get; set; }
		/// <summary>
		/// The homing-rockets speed.
		/// </summary>
		public float Speed { get; set; }
		/// <summary>
		/// The homing-rockets setup duration.
		/// </summary>
		public float SetupDuration { get; set; }
		/// <summary>
		/// The homing-rockets setup duration timer.
		/// </summary>
		public Timer SetupDurationTimer { get; set; }

		/// <summary>
		/// The homing-rockets target spaceship.
		/// </summary>
		public SpaceshipController SpaceshipTarget { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override void ObjectCreate()
		{
			base.ObjectCreate();

			// Initialize the setup timer
			this.SetupDurationTimer = new Timer(TimerMode.Countdown, this.SetupDuration);
		}

		/// <inheritdoc />
		public override void ObjectUpdate()
		{
			base.ObjectUpdate();

			// Update the setup timer
			this.SetupDurationTimer.Update();

			// Face the rocket towards the target
			this.transform.LookAt(this.SpaceshipTarget.transform.position, Vector3.up);

			// The rocket is still in the setup phase
			if(this.SetupDurationTimer.Finished == false)
			{
				// Increase the rockets height
				this.transform.position += this.transform.up * 5.0f * Time.deltaTime;
			}
			else
			{
				// The rocket just ended the setup phase
				if(this.DurationTimer.Finished == false)
				{
					// Release the rocket from the parent so it can move freely
					if(this.DurationTimer.Running == false)
					{
						this.DurationTimer.Start();

						this.transform.parent = null;
					}
					else
					{
						// Increase the speed
						this.Speed += 0.125f * Time.deltaTime;

						// Calculate the targets direction
						Vector3 direction = this.SpaceshipTarget.transform.position - this.transform.position;
						direction.Normalize();

						// Translate the rocket towards the target
						this.transform.Translate(direction * this.Speed, Space.World);
						// Rotate the rocket around its SpaceshipForward axis
						this.transform.Rotate(this.transform.forward, 45.0f * Time.deltaTime);
					}
				}
				else
				{
					ObjectDestroy();
				}
			}
		}

		/// <inheritdoc />
		public override void ObjectDestroy()
		{
			base.ObjectDestroy();
		}

		/// <inheritdoc />
		public override bool Activate()
		{
			if (base.Activate() == false)
				return false;

			// Search for the nearest spaceship
			GameObject nearestSpaceship = null;
			float nearestSpaceshipDistance = float.MaxValue;

			foreach(GameObject spaceship in GameObject.FindGameObjectsWithTag(LayerManager.GetTagName(Tag.Spaceship)))
			{
				float distance = Vector3.Distance(spaceship.transform.position, this.transform.position);

				if(distance < nearestSpaceshipDistance && this.Spaceship.gameObject != spaceship)
				{
					nearestSpaceship = spaceship;
					nearestSpaceshipDistance = distance;
				}
			}

			if (nearestSpaceship != null)
			{
				// Store the spaceship target
				this.SpaceshipTarget = nearestSpaceship.GetComponent<SpaceshipController>();
				// Update the position
				this.transform.position = this.Spaceship.transform.position;

				// Start the setup
				this.SetupDurationTimer.Start();
			}

			return nearestSpaceship != null;
		}

		/// <summary>
		/// Called when [trigger enter].
		/// </summary>
		/// 
		/// <param name="collider">The collider.</param>
		public void OnTriggerEnter(Collider collider)
		{
			// Collision with spaceships
			if(collider.gameObject.layer == LayerManager.GetLayer(Layer.Spaceships))
			{
				// Collision with hostile spaceships
				if(collider.gameObject == this.SpaceshipTarget.gameObject)
				{
					// Inflict more damage to spaceships ahead in the standings
					if(this.Spaceship.Record.CurrentStanding > this.SpaceshipTarget.Record.CurrentStanding)
						this.SpaceshipTarget.InflictDamage(this.Damage * 10);
					else
						this.SpaceshipTarget.InflictDamage(this.Damage);

					// Instantiate the explosion and make it follow the target
					GameObject explosion = ResourceManager.LoadGameObject("Objects/Explosions/Red Explosion");
					explosion.transform.parent = this.SpaceshipTarget.transform;
					explosion.transform.position = this.SpaceshipTarget.transform.position;
					
					// Destroy the missile
					ObjectDestroy();
				}
			}

			// Collision with other power-ups
			if(collider.gameObject.layer == LayerManager.GetLayer(Layer.PowerUps))
			{
				// Collision with the shield power-up
				if(collider.gameObject.tag == LayerManager.GetTagName(Tag.Shield))
				{
					ShieldController shield = collider.transform.GetComponent<ShieldController>();

					if (shield.Spaceship == this.SpaceshipTarget)
					{
						// Instantiate the explosion and make it follow the target
						GameObject explosion = ResourceManager.LoadGameObject("Objects/Explosions/Green Explosion");
						explosion.transform.parent = this.SpaceshipTarget.transform;
						explosion.transform.position = this.SpaceshipTarget.transform.position;

						// Destroy the missile
						ObjectDestroy();
					}
				}
			}
		}
		#endregion
	}
}