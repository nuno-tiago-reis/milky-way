using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private float forwardSpeed;
	private float steeringAngle;

	private Transform body;

	private Transform backLeftCorner;
	private Transform backRightCorner;
	private Transform frontLeftCorner;
	private Transform frontRightCorner;
	
	// Use this for initialization
	public void Start () {
		
		this.forwardSpeed = 2;
		this.steeringAngle = 30;

		this.body = this.transform.FindChild("Body");

		this.backLeftCorner = this.transform.FindChild("Back Left Corner");
		this.backRightCorner = this.transform.FindChild("Back Right Corner");
		this.frontLeftCorner = this.transform.FindChild("Front Left Corner");
		this.frontRightCorner = this.transform.FindChild("Front Right Corner");
	}

	// Update is called once per frame
	public void Update () {

	}

	public void FixedUpdate() {

		// Spacecraft Rotation Ajustments
		RaycastHit backLeftHit;
		RaycastHit backRightHit;
		RaycastHit frontLeftHit;
		RaycastHit frontRightHit;

		// Cast Rays from each of the Spacecrafts Corners heading towards the Track
		bool backLeftRay = Physics.Raycast(backLeftCorner.position, Vector3.down, out backLeftHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));
		bool backRightRay = Physics.Raycast(backRightCorner.position, Vector3.down, out backRightHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));
		bool frontLeftRay = Physics.Raycast(frontLeftCorner.position, Vector3.down, out frontLeftHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));
		bool frontRightRay = Physics.Raycast(frontRightCorner.position, Vector3.down, out frontRightHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));

		if(backLeftRay == true && backRightRay == true && frontLeftRay == true && frontRightRay == true) {

			// If there is a Collision, adjust the Spacecrafts Rotation
			if(backLeftHit.collider.tag == "Track" && backRightHit.collider.tag == "Track" && frontLeftHit.collider.tag == "Track"  && frontRightHit.collider.tag == "Track" ) {

				Vector3 up = Vector3.Cross(backRightHit.point  - Vector3.up, backLeftHit.point   - Vector3.up) +
							 Vector3.Cross(backLeftHit.point   - Vector3.up, frontLeftHit.point  - Vector3.up) +
							 Vector3.Cross(frontLeftHit.point  - Vector3.up, frontRightHit.point - Vector3.up) +
							 Vector3.Cross(frontRightHit.point - Vector3.up, backRightHit.point  - Vector3.up);
				up.Normalize();

				Vector3 right = this.transform.right;
				right.Normalize();

				Vector3 forward = Vector3.Cross(right, up);
				forward.Normalize();

				// Adjust the Spacecrafts Rotation so that it's parallel to the Track
				this.transform.LookAt(this.transform.position + forward * 5.0f, up);
			}
		}

		// Spacecrafts Position Adjustments
		RaycastHit centerHit;
		
		// Cast a Ray from he Spacecrafts Center heading towards the Track
		if(Physics.Raycast(this.transform.position, -this.transform.up, out centerHit, 3.0f, ~(1<<LayerMask.NameToLayer("Spacecraft"))) == true) {
			
			// If there is a Collision, adjust the Spacecrafts Position
			if(centerHit.collider.tag == "Track") {
				
				// Adjust the Spacecrafts Position so that it's slightly above the Track.
				this.transform.position = centerHit.point + this.transform.up * 2.0f;
			}
		}

		// User Input
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxis = Input.GetAxis("Vertical");

		// Update the Spacecrafts Acceleration
		this.rigidbody.velocity += this.transform.forward * verticalAxis * forwardSpeed;

		// Update the Spacecrafts Steering
		this.rigidbody.AddTorque(this.transform.up * horizontalAxis * steeringAngle, ForceMode.Acceleration);
	}

	public void OnDrawGizmos() {	

		if(backLeftCorner != null && backRightCorner != null && frontLeftCorner != null && frontRightCorner != null) {

			RaycastHit backLeftHit;
			RaycastHit backRightHit;
			RaycastHit frontLeftHit;
			RaycastHit frontRightHit;

			bool backLeftRay = Physics.Raycast(backLeftCorner.position, Vector3.down, out backLeftHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));
			bool backRightRay = Physics.Raycast(backRightCorner.position, Vector3.down, out backRightHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));
			bool frontLeftRay = Physics.Raycast(frontLeftCorner.position, Vector3.down, out frontLeftHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));
			bool frontRightRay = Physics.Raycast(frontRightCorner.position, Vector3.down, out frontRightHit, 5.0f, ~(1<<LayerMask.NameToLayer("Spacecraft")));

			if(backLeftRay == true && backRightRay == true && frontLeftRay == true && frontRightRay == true) {

				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(backLeftCorner.position, backLeftHit.point);
				Gizmos.DrawLine(backRightCorner.position, backRightHit.point);
				Gizmos.DrawLine(frontLeftCorner.position, frontLeftHit.point);
				Gizmos.DrawLine(frontRightCorner.position, frontRightHit.point);

				Gizmos.color = Color.cyan;
				Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.up * 2.0f);

				Gizmos.color = Color.red;
				Gizmos.DrawSphere(backLeftHit.point, 1.0f);
				Gizmos.DrawSphere(backRightHit.point, 1.0f);
				Gizmos.DrawSphere(frontLeftHit.point, 1.0f);
				Gizmos.DrawSphere(frontRightHit.point, 1.0f);

				Debug.Log("Looking at " + (this.transform.position + this.transform.forward * 5.0f));

				Gizmos.DrawSphere(this.transform.position + this.transform.forward * 5.0f, 1.0f);
			}
		}
	}
}