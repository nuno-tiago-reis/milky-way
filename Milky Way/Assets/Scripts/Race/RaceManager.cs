using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour {

	public int lapTotal
	{ get; protected set; }

	public CheckpointManager checkpointManager
	{ get; protected set; }

	public int spaceshipTotal
	{ get; protected set; }
	public List<Transform> spaceshipList
	{ get; protected set; }

	public void Start () {

		this.lapTotal = 3;

		this.checkpointManager = this.transform.FindChild("Checkpoint Manager").GetComponent<CheckpointManager>();

		this.spaceshipTotal = 0;
		this.spaceshipList = new List<Transform>();

		for(int i=0; i<this.transform.childCount; i++) {

			Transform player = this.transform.GetChild(i);

			if(player.name.StartsWith("Player")) {

				Transform spaceship = player.FindChild("Spaceship");
				
				SpaceshipController spaceshipController = spaceship.GetComponent<SpaceshipController>();
				
				if(spaceshipController != null) {

					this.spaceshipTotal++;
					
					spaceshipController.id = this.spaceshipTotal;
					
					spaceshipController.raceRecord.currentCheckpoint = 0;
					spaceshipController.raceRecord.currentLap = 0;
					spaceshipController.raceRecord.currentStanding = this.spaceshipTotal;
					
					this.spaceshipList.Add(spaceship);
				}
			}
		}
	}
	
	public void FixedUpdate() {

		// Lap Check
		foreach(Transform spaceship in spaceshipList) {

			SpaceshipController spaceshipController = spaceship.GetComponent<SpaceshipController>();

			int currentCheckpoint = spaceshipController.raceRecord.currentCheckpoint;

			if(currentCheckpoint == this.checkpointManager.checkpointTotal) {

				spaceshipController.raceRecord.currentLap++;
				spaceshipController.raceRecord.currentCheckpoint = 0;
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
