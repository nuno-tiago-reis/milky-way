// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Utility;
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.Projectiles
{
	/// <summary>
	/// Abstract class representing a projectile.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	/// <seealso cref="IObjectController" />
	public abstract class ProjectileController : MonoBehaviour, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The projectiles default damage.
		/// </summary>
		public const float DefaultDamage = 5.0f;
		/// <summary>
		/// The projectiles default duration.
		/// </summary>
		public const float DefaultDuration = 10.0f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The projectiles damage.
		/// </summary>
		public float Damage { get; set; }
		/// <summary>
		/// The projectiles duration.
		/// </summary>
		public Timer DurationTimer { get; set; }

		/// <summary>
		/// The projectiles spaceship.
		/// </summary>
		public SpaceshipController Spaceship;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public virtual void ObjectCreate()
		{
			// Initialize the damage
			this.Damage = DefaultDamage;
			// Initialize the timer
			this.DurationTimer = new Timer(TimerMode.Countdown, DefaultDuration);
			this.DurationTimer.Start();

			// Update the parent
			ProjectileManager.Instance.AddProjectile(this);
		}

		/// <inheritdoc />
		public virtual void ObjectUpdate()
		{
			// Update the timer
			this.DurationTimer.Update();

			// The timer ran out
			if (this.DurationTimer.Current == 0.0f && this.DurationTimer.Finished)
				ObjectDestroy();
		}

		/// <inheritdoc />
		public virtual void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}

		/// <summary>
		/// Called when [trigger enter].
		/// </summary>
		/// 
		/// <param name="collider">The collider.</param>
		public virtual void OnTriggerEnter(Collider collider)
		{
			// Collision with spaceships
			if(collider.gameObject.layer == LayerManager.GetLayer(Layer.Spaceships))
			{
				// Collision with hostile spaceships
				if(collider.gameObject != this.Spaceship.gameObject)
				{
					// Inflict damage
					SpaceshipController spaceship = collider.GetComponent<SpaceshipController>();
					spaceship.InflictDamage(this.Damage);

					// Instantiate the explosion and make it follow the target
					GameObject explosion = ResourceManager.LoadGameObject("Objects/Explosions/Small Red Explosion");
					explosion.transform.parent = collider.transform;
					explosion.transform.position = 
						this.transform.position - (this.transform.position - collider.transform.position).magnitude * this.transform.forward;

					// Destroy the projectile
					ObjectDestroy();
				}
			}

			// Collision with other power-ups
			if(collider.gameObject.layer == LayerManager.GetLayer(Layer.PowerUps))
			{
				// Collision with the shield power-up
				if(collider.gameObject.tag == LayerManager.GetTagName(Tag.Shield))
				{
					// Instantiate the explosion and make it follow the target
					GameObject explosion = ResourceManager.LoadGameObject("Objects/Explosions/Small Green Explosion");
					explosion.transform.parent = collider.transform;
					explosion.transform.position =
						this.transform.position - (this.transform.position - collider.transform.position).magnitude * this.transform.forward;

					// Destroy the projectile
					ObjectDestroy();
				}
			}
		}
		#endregion
	}
}