// Unity
using UnityEngine;

// System
using System.Collections.Generic;

// MilkyWay
using MilkyWay.Utility;
using MilkyWay.Managers;
using MilkyWay.Objects.Projectiles;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements a spaceships shooter.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	/// <seealso cref="IObjectController" />
	public sealed class ShooterController : MonoBehaviour, IObjectController
	{
		#region [Constants and Static]
		/// <summary>
		/// The shooter delay.
		/// </summary>
		private const float ShooterDelay = 0.025f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The shooter timer
		/// </summary>
		private Timer ShooterTimer;
		/// <summary>
		/// The list of shooters.
		/// </summary>
		private List<Transform> ShooterList;
		/// <summary>
		/// The list of lasers shot.
		/// </summary>
		private List<LaserController> LaserList;

		/// <summary>
		/// The shooters spaceship.
		/// </summary>
		public SpaceshipController Spaceship;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Initialize the timer
			this.ShooterTimer = new Timer(TimerMode.Countdown, ShooterDelay);
			this.ShooterTimer.Start();

			// Initialize the shooter list
			this.ShooterList = new List<Transform>
			{
				this.transform.Find("Shooter 1"),
				this.transform.Find("Shooter 2")
			};

			// Initialize the laser list
			this.LaserList = new List<LaserController>();
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Update the timer
			this.ShooterTimer.Update();

			// Update the lasers
			this.LaserList.RemoveAll(laser => laser == null);
			foreach(LaserController laser in this.LaserList)
				laser.ObjectUpdate();
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			// Destroy the lasers
			this.LaserList.RemoveAll(laser => laser == null);
			foreach(LaserController laser in this.LaserList)
				laser.ObjectDestroy();

			Destroy(this.gameObject);
		}

		/// <summary>
		/// Shoots projectiles from each shooter.
		/// </summary>
		public void Shoot()
		{
			// If the delay hasn't ended yet
			if(this.ShooterTimer.Finished == false)
				return;

			float randomDistance = Random.Range(0.0f, 2.0f);

			foreach(Transform shooter in ShooterList)
			{
				// Instantiate the laser
				LaserController laser = ResourceManager.LoadGameObject<LaserController>("Objects/Projectiles/Laser");
				laser.transform.position = shooter.position - this.transform.forward * randomDistance;
				laser.transform.rotation = shooter.parent.rotation;
				laser.Spaceship = this.Spaceship;
				laser.ObjectCreate();

				// Store the laser
				this.LaserList.Add(laser);
			}

			// Start the delay again
			this.ShooterTimer.Start();
		}
		#endregion
	}
}