// Unity
using UnityEngine;

// System
using System;
using System.Linq;
using System.Collections.Generic;

// MilkyWay
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Managers
{
	/// <summary>
	/// The available race modes.
	/// </summary>
	public enum RaceMode
	{
		/// <summary>
		/// The multiplayer arena race mode.
		/// An arena that allows 1v1 face-offs.
		/// </summary>
		Arena,
		/// <summary>
		/// The multiplayer track race mode.
		/// A track that allows multiplayer racing.
		/// </summary>
		Track,
		/// <summary>
		/// The single-player track race mode.
		/// A track that allows single-player racing to test your times.
		/// </summary>
		TimeAttack
	}

	/// <summary>
	/// Manages the entire game.
	/// </summary>
	public sealed class GameManager : SingletonManager<GameManager>
	{
		#region [Constants and Statics]
		/// <summary>
		/// The name of the item container game-object.
		/// </summary>
		public const string ItemContainerName = "Items";
		/// <summary>
		/// The name of the power-up container game-object.
		/// </summary>
		public const string PowerUpContainerName = "PowerUps";
		/// <summary>
		/// The name of the iterface container game-object.
		/// </summary>
		public const string InterfaceContainerName = "Interface";
		/// <summary>
		/// The name of the spaceship container game-object.
		/// </summary>
		public const string SpaceshipContainerName = "Spaceships";
		#endregion

		#region [Attributes]
		/// <summary>
		/// The currently selected race mode.
		/// </summary>
		public RaceMode RaceMode;
		/// <summary>
		/// The current lap total.
		/// </summary>
		public int LapTotal { get; private set; }

		/// <summary>
		/// The item manager.
		/// </summary>
		public ItemManager ItemManager { get; private set; }
		/// <summary>
		/// The power-up manager.
		/// </summary>
		public PowerUpManager PowerUpManager { get; private set; }
		/// <summary>
		/// The projectile manager.
		/// </summary>
		public ProjectileManager ProjectileManager { get; private set; }
		/// <summary>
		/// The interface manager.
		/// </summary>
		public InterfaceManager InterfaceManager { get; private set; }

		/// <summary>
		/// The game managers list of spaceship controllers <see cref="SpaceshipController"/>.
		/// </summary>
		public List<SpaceshipController> SpaceshipList { get; private set; }
		/// <summary>
		/// The game managers list of checkpoint controllers <see cref="CheckpointController"/>.
		/// </summary>
		public List<CheckpointController> CheckpointList { get; private set; }
		#endregion

		#region [Methods]
		/// <summary>
		/// Starts the game by initializing all the other managers (including the player and the cameras).
		/// </summary>
		public void Start()
		{
			switch(RaceMode)
			{
				case RaceMode.Arena:
					this.LapTotal = 0;
					break;

				case RaceMode.Track:
					this.LapTotal = 3;
					break;

				case RaceMode.TimeAttack:
					this.LapTotal = 3;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			// Find the spaceships in the arena/track
			this.SpaceshipList = this.transform.GetComponentsInChildren<SpaceshipController>().ToList();
			// Initialize the spaceships
			foreach(SpaceshipController spaceship in this.SpaceshipList)
				spaceship.ObjectCreate();
			// Find the checkpoints in the arena/track
			this.CheckpointList = this.transform.GetComponentsInChildren<CheckpointController>().ToList();
			// Initialize the checkpoints
			foreach(CheckpointController checkpoint in this.CheckpointList)
				checkpoint.ObjectCreate();

			// Find the item manager
			this.ItemManager = GetComponentInChildren<ItemManager>();
			this.ItemManager.ObjectCreate();
			// Find the power-up manager
			this.PowerUpManager = GetComponentInChildren<PowerUpManager>();
			this.PowerUpManager.ObjectCreate();
			// Find the projectile manager
			this.ProjectileManager = GetComponentInChildren<ProjectileManager>();
			this.ProjectileManager.ObjectCreate();
			// Find the interface manager
			this.InterfaceManager = GetComponentInChildren<InterfaceManager>();
			this.InterfaceManager.ObjectCreate();
		}

		/// <summary>
		/// Updates the instance.
		/// </summary>
		public void Update()
		{
			switch(RaceMode)
			{
				case RaceMode.Track:
				case RaceMode.TimeAttack:

					// Laps
					foreach(SpaceshipController spaceship in this.SpaceshipList)
					{
						// Check the current checkpoint
						if(spaceship.Record.CurrentCheckpoint == this.CheckpointList.Count)
						{
							// Update the lap
							spaceship.Record.CurrentLap++;
							// Update the checkpoint
							spaceship.Record.CurrentCheckpoint = 0;

							// Update the lap times
							if(spaceship.Record.CurrentLapTime < spaceship.Record.BestLapTime || spaceship.Record.BestLapTime == 0.0f)
							{
								spaceship.Record.BestLapTime = spaceship.Record.CurrentLapTime;
								spaceship.Record.CurrentLapTime = 0.0f;
							}
						}

						// Check the current lap
						if(spaceship.Record.CurrentLap == this.LapTotal)
						{
							Debug.Log("Winrar!");
						}
					}

					// Standings
					List<SpaceshipController> orderedSpaceships = new List<SpaceshipController>();

					// Calculate the standings
					foreach(SpaceshipController spaceship in this.SpaceshipList)
					{
						int currentStanding = 0;
						int currentLap = spaceship.Record.CurrentLap;
						int currentCheckpoint = spaceship.Record.CurrentCheckpoint;

						// Compare the current spaceship with the previously ordered ones
						foreach(SpaceshipController orderedSpaceship in orderedSpaceships)
						{
							// The spaceship is ahead on laps, maintain the standing
							if(currentLap > orderedSpaceship.Record.CurrentLap)
							{
								break;
							}

							// The spaceship is ahead on checkpoints, maintain the standing
							if(currentLap == orderedSpaceship.Record.CurrentLap &&
								currentCheckpoint > orderedSpaceship.Record.CurrentCheckpoint)
							{
								break;
							}

							// The spaceship is ahead in distance, maintain the standing
							if(currentLap == orderedSpaceship.Record.CurrentLap &&
								currentCheckpoint == orderedSpaceship.Record.CurrentCheckpoint)
							{
								// Select the next checkpoint
								CheckpointController checkpoint;
								if(currentCheckpoint == this.CheckpointList.Count)
									checkpoint = this.CheckpointList.FirstOrDefault(existingCheckpoint => existingCheckpoint.ID == 1);
								else
									checkpoint = this.CheckpointList.FirstOrDefault(existingCheckpoint => existingCheckpoint.ID == currentCheckpoint + 1);

								if (checkpoint != null)
								{
									float distance1 = Vector3.Distance(checkpoint.transform.position, spaceship.transform.position);
									float distance2 = Vector3.Distance(checkpoint.transform.position, orderedSpaceship.transform.position);

									if(distance1 < distance2)
									{
										break;
									}
								}
							}

							currentStanding++;
						}

						orderedSpaceships.Insert(currentStanding, spaceship);
					}

					// Update the standings
					foreach (SpaceshipController spaceship in this.SpaceshipList)
						spaceship.Record.CurrentStanding = orderedSpaceships.IndexOf(spaceship) + 1;

					break;
			}

			// Update the item manager
			this.ItemManager.ObjectUpdate();
			// Update the power-up manager
			this.PowerUpManager.ObjectUpdate();
			// Update the projectile manager
			this.ProjectileManager.ObjectUpdate();
			// Update the interface manager
			this.InterfaceManager.ObjectUpdate();
			// Update the spaceships
			foreach(SpaceshipController spaceship in this.SpaceshipList)
				spaceship.ObjectUpdate();
		}

		/// <summary>
		/// Destroys this instance.
		/// </summary>
		public void Destroy()
		{
			// Destroy the item manager
			this.ItemManager.ObjectDestroy();
			// Destroy the power-up manager
			this.PowerUpManager.ObjectDestroy();
			// Destroy the projectile manager
			this.ProjectileManager.ObjectDestroy();
			// Destroy the interface manager
			this.InterfaceManager.ObjectDestroy();
			// Destroy the spaceships
			foreach(SpaceshipController spaceship in this.SpaceshipList)
				spaceship.ObjectDestroy();

			Destroy(this.gameObject);
		}
		#endregion
	}
}