using UnityEngine;

public class CheckpointManager : MonoBehaviour {

	public int checkpointTotal
	{ get; protected set; }
	
	public void Start () {
		
		for(int i=0; i<this.transform.childCount; i++) {

			Transform track = this.transform.GetChild(i);
			Transform checkpoint = track.FindChild("Checkpoint");
			
			CheckpointController checkpointController = checkpoint.GetComponent<CheckpointController>();

			if(checkpointController != null) {

				checkpointTotal++;

				checkpointController.id = checkpointTotal;
			}
		}
	}

	public Transform GetCheckpoint(int index) {

		Transform track = this.transform.GetChild(index);
		Transform checkpoint = track.FindChild("Checkpoint");

		return checkpoint;
	}
}
