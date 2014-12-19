using UnityEngine;

public class CameraController : MonoBehaviour {

	// Cameras reference to the RaceManager
	public RaceManager raceManager
	{ get; protected set; }

	// Cameras reference to its Spaceship
	public Transform spaceship
	{ get; protected set; }
	// Cameras reference to its Spaceship Controller
	public SpaceshipController spaceshipController
	{ get; protected set; }

	public void Awake() {
	}

	public void Initialize() {

		// Initialize the Cameras reference to the RaceManager
		this.raceManager = this.transform.parent.parent.GetComponent<RaceManager>();

		// Initialize the Cameras reference to its Spaceship
		this.spaceship = this.transform.parent.FindChild("Spaceship");
		// Initialize the Cameras reference to its SpaceshipController
		this.spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		// Set the Cameras Offset according to the Spaceships ID
		if(this.raceManager.mode == RaceManager.RaceMode.Arena || this.raceManager.mode == RaceManager.RaceMode.SingleRace) {

			Camera camera = this.transform.GetComponent<Camera>();

			// Create the Viewport Coordinates acoording the the Spaceships ID
			Rect viewport = new Rect(0.5f * (float)(spaceshipController.raceRecord.id - 1), 0.0f, 0.5f, 1.0f);

			// Update the Cameras Viewport
			camera.rect = viewport;
		}
	}

	public void FixedUpdate() {

		// Calculate the Destination from the Spaceships position and place it behind it.
		Vector3 destination = this.spaceship.transform.position - this.spaceship.transform.forward * 15.0f + this.spaceship.transform.up * 12.5f;

		Vector3 velocity = Vector3.zero;
		// Dampen time - Higher values means the Camera takes more time to adjust.
		float dampTime = 0.05f;

		// Adjust the Cameras Position taking into account the dampen time required to catch up to the Spaceship.
		this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
		// Adjust the Cameras LookAt point to that it matches the Spaceship
		this.transform.LookAt(this.spaceship.transform.position + this.spaceship.transform.forward * 15.0f, this.spaceship.transform.up);
	}
}