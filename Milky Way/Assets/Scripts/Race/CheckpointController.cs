using UnityEngine;

public class CheckpointController : MonoBehaviour {

	// Checkpoints ID
	public int id
	{ get; set; }

	public void Start () {
	}

	public void FixedUpdate() {
	}

	public void OnTriggerEnter(Collider collider) {
		
		if(collider.transform.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {
			
			SpaceshipController spaceshipController = collider.transform.GetComponent<SpaceshipController>();

			if(spaceshipController.raceRecord.currentCheckpoint == this.id - 1)
				spaceshipController.raceRecord.currentCheckpoint = this.id;
		}

		Debug.Log (this.id);
	}
}
