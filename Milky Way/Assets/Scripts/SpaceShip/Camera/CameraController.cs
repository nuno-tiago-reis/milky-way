using UnityEngine;

public class CameraController : MonoBehaviour {

	// Cameras ID
	public int joystickID
	{ get; protected set; }

	// Cameras Spaceship
	public Transform spaceship
	{ get; protected set; }
	
	// Use this for initialization
	public void Start () {

		// Cameras reference to its Spaceships
		this.spaceship = this.transform.parent.FindChild("Spaceship");
	}

	public void Initialize() {

		// Set the Cameras Offset according to the Spaceships ID
		Camera camera = this.transform.GetComponent<Camera>();
		
		SpaceshipController spaceshipController = this.spaceship.GetComponent<SpaceshipController>();
		
		camera.rect = new Rect(0.5f * (float)(spaceshipController.id - 1), 0.0f, 0.5f, 1.0f);
	}

	public void FixedUpdate() {

		// Calculate the Destination from the Spaceships position and place it behind it.
		Vector3 destination = this.spaceship.transform.position - this.spaceship.transform.forward * 10.0f + this.spaceship.transform.up * 12.5f;

		Vector3 velocity = Vector3.zero;
		// Dampen time - Higher values means the Camera takes more time to adjust.
		float dampTime = 0.05f;

		// Adjust the Cameras Position taking into account the dampen time required to catch up to the Spaceship.
		this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
		// Adjust the Cameras LookAt point to that it matches the Spaceship
		this.transform.LookAt(this.spaceship.transform.position + this.spaceship.transform.forward * 15.0f, this.spaceship.transform.up);
	}
}