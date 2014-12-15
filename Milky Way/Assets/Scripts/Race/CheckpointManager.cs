using UnityEngine;

public class CheckpointManager : MonoBehaviour {

	public int checkpointTotal
	{ get; protected set; }
	
	public void Start () {

		for(int i=0; i<this.transform.childCount; i++) {

			Transform checkpoint = this.transform.FindChild("Checkpoint");
			
			CheckpointController checkpointController = checkpoint.GetComponent<CheckpointController>();

			if(checkpointController != null) {

				checkpointTotal++;

				checkpointController.id = checkpointTotal;
			}
		}

		Debug.Log (checkpointTotal);
	}

	public Transform GetCheckpoint(int index) {


		Transform checkpoint = this.transform.FindChild("Checkpoint");

		return checkpoint;
	}
}
