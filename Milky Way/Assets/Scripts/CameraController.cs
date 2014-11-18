using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform spaceship
	{ get; protected set; }
	
	// Use this for initialization
	public void Start () {

		this.spaceship = GameObject.Find("Spaceship").transform;
	}

	// Update is called once per frame
	public void Update () {

	}

	public void FixedUpdate() {

		// Calculate the Destination from the Spaceships position and place it behind it.
		Vector3 destination = this.spaceship.transform.position - this.spaceship.transform.forward * 5.0f + this.spaceship.transform.up * 7.5f;

		Vector3 velocity = Vector3.zero;
		// Dampen time - Higher values means the Camera takes more time to adjust.
		float dampTime = 0.01f;

		// Adjust the Cameras Position taking into account the dampen time required to catch up to the Spaceship.
		this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
		// Adjust the Cameras LookAt point to that it matches the Spaceship
		this.transform.LookAt(this.spaceship.transform.position + this.spaceship.transform.forward * 15.0f);
	}
}