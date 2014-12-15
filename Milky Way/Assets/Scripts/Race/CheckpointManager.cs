using UnityEngine;

public class CheckpointManager : MonoBehaviour {

	public int checkpointTotal
	{ get; protected set; }
	
	public void Start () {

		for(int i=1; i<this.transform.childCount+1; i++) {

			Transform checkpoint = this.transform.FindChild("Checkpoint " + i);
			
			CheckpointController checkpointController = checkpoint.GetComponent<CheckpointController>();

			if(checkpointController != null) {

				checkpointTotal++;

				checkpointController.id = checkpointTotal;
			}
		}
	}

	public Transform GetCheckpoint(int index) {

		Transform checkpoint = this.transform.FindChild("Checkpoint " + index);

		return checkpoint;
	}
}
