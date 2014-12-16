using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour {

	public enum RaceMode { Arena, TimeAttack, SingleRace };

	public RaceMode mode;

	public int lapTotal
	{ get; protected set; }

	public CheckpointManager checkpointManager
	{ get; protected set; }

	public int spaceshipTotal
	{ get; protected set; }
	public List<Transform> spaceshipList
	{ get; protected set; }

	public void Start () {

		this.spaceshipTotal = 0;
		this.spaceshipList = new List<Transform>();

		if(this.mode == RaceMode.SingleRace) {
			
			this.lapTotal = 3;
			
			this.checkpointManager = this.transform.FindChild("Checkpoint Manager").GetComponent<CheckpointManager>();

			// Initialize the Spaceships
			for(int i=0; i<this.transform.childCount; i++)
				InitializeSpaceship(this.transform.GetChild(i));
		}

		if(this.mode == RaceMode.TimeAttack) {
			
			this.lapTotal = 3;
			
			this.checkpointManager = this.transform.FindChild("Checkpoint Manager").GetComponent<CheckpointManager>();

			// Initialize the Spaceship
			InitializeSpaceship(this.transform.FindChild("Player"));
		}
		
		if(this.mode == RaceMode.Arena) {

			this.lapTotal = 0;
			
			this.checkpointManager = null;

			// Initialize the Spaceships
			for(int i=0; i<this.transform.childCount; i++)
				InitializeSpaceship(this.transform.GetChild(i));
		}
	}

	public void InitializeSpaceship(Transform player) {

		if(player.name.StartsWith("Player")) {

			// Initialize the Spaceship
			Transform spaceshipTransform = player.FindChild("Spaceship");
			
				SpaceshipController spaceshipController = spaceshipTransform.GetComponent<SpaceshipController>();
				
				// Initialize the Spaceship Controller
				if(spaceshipController != null) {
					
					this.spaceshipTotal++;

					spaceshipController.Initialize(this.spaceshipTotal);
					
					this.spaceshipList.Add(spaceshipTransform);
				}

			// Initialize the Spaceships Camera 
			Transform cameraTransform = player.FindChild("Camera");
			
				CameraController cameraController = cameraTransform.GetComponent<CameraController>();
				
				// Initialize the Spaceships Camera Controller
				if(cameraController != null)
					cameraController.Initialize();

			// Initialize the Spaceships HUD 
			Transform hudTransform = player.FindChild("HUD");
			
				HUD hudController = hudTransform.GetComponent<HUD>();
				
				// Initialize the Spaceships HUD Controller
				if(hudController != null)					
					hudController.Initialize();
				
				HUDMinimap hudMinimapController = hudTransform.FindChild("Camera").GetComponent<HUDMinimap>();
				
				// Initialize the Spaceships HUD Minimap Controller
				if(hudMinimapController != null)
					hudMinimapController.Initialize();

			// Initialize the Spaceships Joystick
			Transform joystickTransform = player.FindChild("Spaceship");
			
				JoystickController joystickController = joystickTransform.GetComponent<JoystickController>();

				// Initialize the Spaceships Joystick Controller
				if(joystickController != null)
					joystickController.Initialize();
		}
	}
	
	public void FixedUpdate() {

		if(this.mode == RaceMode.Arena) {

			foreach(Transform spaceship in spaceshipList) {
				
				SpaceshipController spaceshipController = spaceship.GetComponent<SpaceshipController>();

				// Time Increment
				spaceshipController.raceRecord.currentLapTime += Time.fixedDeltaTime;
				spaceshipController.raceRecord.totalLapTime += Time.fixedDeltaTime;
			}
		}

		if(this.mode == RaceMode.TimeAttack || this.mode == RaceMode.SingleRace) {

			// Lap Check
			foreach(Transform spaceship in spaceshipList) {

				SpaceshipController spaceshipController = spaceship.GetComponent<SpaceshipController>();

				// Time Increment
				spaceshipController.raceRecord.currentLapTime += Time.fixedDeltaTime;
				spaceshipController.raceRecord.totalLapTime += Time.fixedDeltaTime;

				int currentCheckpoint = spaceshipController.raceRecord.currentCheckpoint;

				if(currentCheckpoint == this.checkpointManager.checkpointTotal) {

					spaceshipController.raceRecord.currentLap++;
					spaceshipController.raceRecord.currentCheckpoint = 0;

					if(spaceshipController.raceRecord.currentLapTime < spaceshipController.raceRecord.bestLapTime || spaceshipController.raceRecord.bestLapTime == 0.0f) {

						spaceshipController.raceRecord.bestLapTime = spaceshipController.raceRecord.currentLapTime;

						spaceshipController.raceRecord.currentLapTime = 0.0f;
					}
				}

				int currentLap = spaceshipController.raceRecord.currentLap;

				if(currentLap == this.lapTotal) {

					Debug.Log("Winrar!");
				}
			}

			// Standings Check
			List<Transform> spaceshipStandings = new List<Transform>();

			foreach(Transform spaceship in spaceshipList) {

				SpaceshipController spaceshipController = spaceship.GetComponent<SpaceshipController>();

				if(spaceshipStandings.Count == 0) {
					
					spaceshipStandings.Add(spaceship);
				}
				else {

					int currentLap = spaceshipController.raceRecord.currentLap;
					int currentCheckpoint =  spaceshipController.raceRecord.currentCheckpoint;

					int currentStanding = 0;

					foreach(Transform auxiliarySpaceship in spaceshipStandings) {

						SpaceshipController auxiliarySpaceshipController = auxiliarySpaceship.GetComponent<SpaceshipController>();

						if(currentLap > auxiliarySpaceshipController.raceRecord.currentLap) {

							break;
						}

						if(currentLap == auxiliarySpaceshipController.raceRecord.currentLap && 
						   currentCheckpoint > auxiliarySpaceshipController.raceRecord.currentCheckpoint) {

							break;
						}

						if(currentLap == auxiliarySpaceshipController.raceRecord.currentLap && 
						   currentCheckpoint == auxiliarySpaceshipController.raceRecord.currentCheckpoint) {

							Transform checkpoint = this.checkpointManager.GetCheckpoint(currentCheckpoint + 1);

							float distance1 = Vector3.Distance(checkpoint.position, spaceship.position);
							float distance2 = Vector3.Distance(checkpoint.position, auxiliarySpaceship.position);

							if(distance1 < distance2) {

								break;
							}
						}

						currentStanding++;
					}

					spaceshipStandings.Insert(currentStanding, spaceship);
				}
			}

			int standing = 0;
			
			foreach(Transform spaceship in spaceshipStandings) {

				standing++;

				SpaceshipController spaceshipController = spaceship.GetComponent<SpaceshipController>();
				spaceshipController.raceRecord.currentStanding = standing;
			}
		}
	}
}
