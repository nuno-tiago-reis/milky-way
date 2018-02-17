// Unity
using System;
using UnityEngine;

// MilkyWay
using MilkyWay.Managers;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements a spaceship configuration.
	/// </summary>
	public sealed class SpaceshipConfiguration
	{
		#region [Constants and Statics]
		/// <summary>
		/// The minimum health points.
		/// </summary>
		private const int MinimumHealthPoints = 0;
		/// <summary>
		/// The maximum health points.
		/// </summary>
		private const int MaximumHealthPoints = 5;

		/// <summary>
		/// The minimum handling points.
		/// </summary>
		private const int MinimumHandlingPoints = 0;
		/// <summary>
		/// The maximum handling points.
		/// </summary>
		private const int MaximumHandlingPoints = 5;

		/// <summary>
		/// The minimum weapon power points.
		/// </summary>
		private const int MinimumWeaponPowerPoints = 0;
		/// <summary>
		/// The maximum weapon power points.
		/// </summary>
		private const int MaximumWeaponPowerPoints = 5;

		/// <summary>
		/// The minimum acceleration points.
		/// </summary>
		private const int MinimumAccelerationPoints = 0;
		/// <summary>
		/// The maximum acceleration points.
		/// </summary>
		private const int MaximumAccelerationPoints = 5;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The spaceship identifier.
		/// </summary>
		public int SpaceshipID { get; private set; }

		/// <summary>
		/// The currently selected health points.
		/// </summary>
		public int Health { get; set; }
		/// <summary>
		/// The currently selected handling points.
		/// </summary>
		public int Handling { get; set; }
		/// <summary>
		/// The currently selected weapon power points.
		/// </summary>
		public int WeaponPower { get; set; }
		/// <summary>
		/// The currently selected acceleration points.
		/// </summary>
		public int Acceleration { get; set; }
		#endregion

		#region [Methods]
		/// <summary>
		/// Initializes a new instance of the <see cref="SpaceshipConfiguration"/> class.
		/// </summary>
		/// 
		/// <param name="spaceship">The spaceships.</param>
		/// <param name="health">The health.</param>
		/// <param name="handling">The handling.</param>
		/// <param name="weaponPower">The weapon power.</param>
		/// <param name="acceleration">The acceleration.</param>
		public SpaceshipConfiguration(SpaceshipController spaceship, int health, int handling, int weaponPower, int acceleration)
		{
			this.SpaceshipID = spaceship != null ? spaceship.ID : 0;

			this.Health = Mathf.Clamp(health, MinimumHealthPoints, MaximumHealthPoints);
			this.Handling = Mathf.Clamp(handling, MinimumHandlingPoints, MaximumHandlingPoints);
			this.WeaponPower = Mathf.Clamp(weaponPower, MinimumWeaponPowerPoints, MaximumWeaponPowerPoints);
			this.Acceleration = Mathf.Clamp(acceleration, MinimumAccelerationPoints, MaximumAccelerationPoints);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpaceshipConfiguration"/> class.
		/// </summary>
		public SpaceshipConfiguration(int spaceshipID) : this(null, 0, 0, 0, 0)
		{
			this.SpaceshipID = spaceshipID;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpaceshipConfiguration"/> class.
		/// </summary>
		public SpaceshipConfiguration() : this(null, 0,0,0,0)
		{
			// Nothing to do here.
		}

		#region [Methods] Serialization
		/// <summary>
		/// Loads the configuration from a JSON file.
		/// </summary>
		/// 
		/// <param name="spaceshipID">The spaceship ID to load the configuration for.</param>
		public static SpaceshipConfiguration LoadConfiguration(int spaceshipID)
		{
			// Create the file name for this spaceship
			string fileName = string.Format(@"SpaceshipConfiguration-{0}.json", spaceshipID);

			// Read the configuration
			try
			{
				return SerializationManager.Deserialize<SpaceshipConfiguration>(fileName);
			}
			catch (Exception)
			{
				return new SpaceshipConfiguration(spaceshipID);
			}
		}

		/// <summary>
		/// Saves the configuration to a JSON file.
		/// </summary>
		/// 
		/// <param name="configuration">The configuration to save.</param>
		public static void SaveConfiguration(SpaceshipConfiguration configuration)
		{
			// Create the file name for this spaceship
			string fileName = string.Format(@"SpaceshipConfiguration-{0}.json", configuration.SpaceshipID);

			// Read the configuration
			SerializationManager.Serialize(configuration, fileName);
		}
		#endregion

		#endregion
	}
}