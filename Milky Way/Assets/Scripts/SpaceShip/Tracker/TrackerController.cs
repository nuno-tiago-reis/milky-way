using UnityEngine;

public class TrackerController : MonoBehaviour {

	
	public float rotationSpeed;
	
	// When the game starts
	public void Awake() {
	}
	
	public void FixedUpdate() {
		
		// Items Rotation Adjustments
		Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
		
		eulerAngles.y += rotationSpeed;
		
		this.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
	}
}
