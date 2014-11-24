using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpaceshipController : MonoBehaviour {

	// Spaceships ID
	public int id;

	// Spaceships Health
	public float health
	{ get; protected set; }
	public float maximumHealth
	{ get; protected set; }
	public float minimumHealth
	{ get; protected set; }

	// Spaceships Gold
	public int gold
	{ get; protected set; }
	
	// Spaceships Ability Map
	public Dictionary<string, GameObject> abilityInventory;

	// Spaceships Controller Attributes
	public float speed
	{ get; protected set; }
	public float steeringAngle
	{ get; protected set; }

	public Vector3 up
	{ get; protected set; }
	public Vector3 right
	{ get; protected set; }
	public Vector3 forward
	{ get; protected set; }

	public float directionInterpolator
	{ get; protected set; }

	// Spaceships Children Transforms
	public Transform model
	{ get; protected set; }
	
	public Transform backLeftCorner
	{ get; protected set; }
	public Transform backRightCorner
	{ get; protected set; }
	public Transform frontLeftCorner
	{ get; protected set; }
	public Transform frontRightCorner
	{ get; protected set; }

	// Spaceships Controller
	public JoystickController joystick
	{ get; protected set; }

	#region Initialization
	public void Start () {
	
		// Spaceships starting Health
		this.health = 100.0f;
		this.maximumHealth = 100.0f;
		this.minimumHealth = 0.0f;

		// Spaceships starting Abilities
		this.abilityInventory = new Dictionary<string, GameObject>();

		// Spaceships steering values
		this.speed = 2;
		this.steeringAngle = 15;

		// Spaceships reference to its Children
		this.model = this.transform.FindChild("Model");

		this.backLeftCorner = this.transform.FindChild("Back Left Corner");
		this.backRightCorner = this.transform.FindChild("Back Right Corner");
		this.frontLeftCorner = this.transform.FindChild("Front Left Corner");
		this.frontRightCorner = this.transform.FindChild("Front Right Corner");

		// Spaceships reference to its Joystick
		this.joystick = this.transform.GetComponent<JoystickController>();
	}
	#endregion

	#region GameLogic
	public void FixedUpdate() {

		/*********************************************************************** Road-sticking Logic ***********************************************************************/
		
		// Spaceship Rotation Ajustments
		RaycastHit backLeftHit;
		RaycastHit backRightHit;
		RaycastHit frontLeftHit;
		RaycastHit frontRightHit;
		
		// Cast Rays from each of the Spaceships Corners heading towards the Track
		bool backLeftRay = Physics.Raycast(backLeftCorner.position, -this.transform.up, out backLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool backRightRay = Physics.Raycast(backRightCorner.position, -this.transform.up, out backRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool frontLeftRay = Physics.Raycast(frontLeftCorner.position, -this.transform.up, out frontLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool frontRightRay = Physics.Raycast(frontRightCorner.position, -this.transform.up, out frontRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		
		if(backLeftRay == true && backRightRay == true && frontLeftRay == true && frontRightRay == true) {
			
			// If there is a Collision, adjust the Spaceships Rotation
			if(backLeftHit.collider.tag == "Road" && backRightHit.collider.tag == "Road" && frontLeftHit.collider.tag == "Road"  && frontRightHit.collider.tag == "Road" ) {
				
				this.up = 
					Vector3.Cross(backRightHit.point - backRightHit.normal, backLeftHit.point - backLeftHit.normal) +
					Vector3.Cross(backLeftHit.point - backLeftHit.normal, frontLeftHit.point - frontLeftHit.normal) +
					Vector3.Cross(frontLeftHit.point - frontLeftHit.normal, frontRightHit.point - frontRightHit.normal) +
					Vector3.Cross(frontRightHit.point - frontRightHit.normal, backRightHit.point  - backRightHit.normal);
				this.up.Normalize();
				
				this.right = this.transform.right;
				this.right.Normalize();
				
				this.forward = Vector3.Cross(right, up);
				this.forward.Normalize();

				this.directionInterpolator = 0.0f;
			}
		}

		this.directionInterpolator += Time.deltaTime * 2.0f;

		Vector3 interpolatedUp = Vector3.Slerp(this.transform.up, this.up, directionInterpolator);
		Vector3 interpolatedRight = Vector3.Slerp(this.transform.right, this.right, directionInterpolator);
		Vector3 interpolatedForward = Vector3.Slerp(this.transform.forward, this.forward, directionInterpolator);

		// Adjust the Spaceships Rotation so that it's parallel to the Track
		this.transform.LookAt(this.transform.position + interpolatedForward * 5.0f, interpolatedUp);
		
		// Spaceships Position Adjustments
		RaycastHit centerHit;
		
		// Cast a Ray from he Spaceships Center heading towards the Track
		if(Physics.Raycast(this.transform.position, -this.transform.up, out centerHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks")) == true) {
			
			// If there is a Collision, adjust the Spaceships Position
			if(centerHit.collider.tag == "Road") {
				
				// Adjust the Spaceships Position so that it's slightly above the Track.
				this.transform.position = centerHit.point + this.transform.up * 2.5f;
			}
		}

		/************************************************************************** Input **************************************************************************/

		// Accelerator = Cross & Brake = Square
		if(Input.GetKey(this.joystick.cross) == true && Input.GetKey(this.joystick.square) == false) {
		//if((Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.B) == false) {

			// "Shifts"
			if(this.speed < 10.0f)
				this.speed += 0.05f + (0.10f * 10.0f/(this.speed+1.0f));
		}
		// Reverse = Triangle & Brake = Square
		else if(Input.GetKey(this.joystick.triangle) == true && Input.GetKey(this.joystick.square) == false) {
		//if((Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.B) == false) {
			
			// "Shifts"
			if(this.speed > -2.0f)
				this.speed -= 0.05f;
		}
		else {
			
			this.speed = this.speed * 0.95f;
		}

		// Accelerator = Cross & Brake = Square
		if((Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.UpArrow)) && Input.GetKey(KeyCode.B) == false) {
			
			// "Shifts"
			if(this.speed < 10.0f)
				this.speed += 0.05f + (0.10f * 10.0f/(this.speed+1.0f));
		}
		// Reverse = Triangle & Brake = Square
		else if((Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.DownArrow)) && Input.GetKey(KeyCode.B) == false) {
			
			// "Shifts"
			if(this.speed > -2.0f)
				this.speed -= 0.05f;
		}
		else {
			
			this.speed = this.speed * 0.95f;
		}
		
		// Brake = Square
		if(Input.GetKey(this.joystick.square) == true) {
		//if(Input.GetKey(KeyCode.B) == true) {

			// Reduce the Spaceships Velocity (Acceleration)
			this.rigidbody.velocity = this.rigidbody.velocity * 0.99f;
			// Reduce the Spaceships Angular Velocity (Steering)
			this.rigidbody.angularVelocity = this.rigidbody.angularVelocity * 0.99f;
		}

		// Brake = Square
		if(Input.GetKey(KeyCode.B) == true) {
			
			// Reduce the Spaceships Velocity (Acceleration)
			this.rigidbody.velocity = this.rigidbody.velocity * 0.99f;
			// Reduce the Spaceships Angular Velocity (Steering)
			this.rigidbody.angularVelocity = this.rigidbody.angularVelocity * 0.99f;
		}

		// Steering = Stick and Directional Pad
		float horizontalAxis = Input.GetAxis(this.joystick.horizontalAxis);
		//float horizontalAxis = Input.GetAxis("Horizontal");

		if(horizontalAxis != 0) {

			float angle = this.model.localRotation.eulerAngles.z;

			if(angle > 180.0f)
				angle -= 360.0f;

			if(angle <  45.0f && horizontalAxis < 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);

			if(angle > -45.0f && horizontalAxis > 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);

			this.speed *= 0.95f;
		}

		// Steering = Stick and Directional Pad
		horizontalAxis = Input.GetAxis("Horizontal Axis PC");
		
		if(horizontalAxis != 0) {
			
			float angle = this.model.localRotation.eulerAngles.z;
			
			if(angle > 180.0f)
				angle -= 360.0f;
			
			if(angle <  45.0f && horizontalAxis < 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);
			
			if(angle > -45.0f && horizontalAxis > 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);
			
			this.speed *= 0.95f;
		}

		// Increment the Spaceships Velocity
		this.rigidbody.velocity += this.transform.forward * speed;

		// Increment the Spaceships Angular Velocity
		this.rigidbody.AddTorque(this.transform.up * horizontalAxis * steeringAngle, ForceMode.Impulse);
		
		// Rockets - L1
		if(Input.GetKey(this.joystick.L1) == true) {
			
			GameObject ability = null;
			
			if(abilityInventory.TryGetValue("Rocket", out ability)) {
				
				AbilityController abilityController = ability.GetComponent<RocketController>();
				
				if(abilityController != null) {
					
					abilityController.Activate(this.transform);
					
					abilityInventory.Remove(abilityController.abilityName);
					
					AddAbility("Rocket");
				}
			}
		}

		// Shield - R1
		if(Input.GetKey(this.joystick.R1) == true) {
			
			GameObject ability = null;
			
			if(abilityInventory.TryGetValue("Shield", out ability)) {
				
				AbilityController abilityController = ability.GetComponent<ShieldController>();
				
				if(abilityController != null) {
					
					abilityController.Activate(this.transform);
					
					abilityInventory.Remove(abilityController.abilityName);
				}
			}
		}
	}

	public void InflictDamage(float damage) {

		this.health = Mathf.Clamp(this.health - damage, this.minimumHealth, this.maximumHealth);

		this.speed *= 0.75f;

		Debug.Log("Took Damage (" + damage + ")! Remaining Health is " + this.health + "!");
	}

	public void OnCollisionEnter(Collision collision) {
		
		// If colliding with a track boundary
		if(collision.collider.transform.tag == "Boundary") {

			this.speed *= 0.5f;

			Debug.Log("Boundary Hit!");

			foreach(ContactPoint contactPoint in collision.contacts) {

				Vector3 contactNormal = contactPoint.normal;

				Debug.Log("Repulsion!");

				this.rigidbody.velocity = Vector3.Reflect(this.rigidbody.velocity.normalized, contactNormal.normalized) * this.rigidbody.velocity.magnitude * 0.75f; 
			}
		}
	}

	#endregion

	#region Items
	public bool AddGold(int value) {

		this.gold += value;

		//Debug.Log("Gained " + value + " gold!");

		return true;
	}
	
	public bool AddAbility(string abilityName) {

		// If the Spaceship already has this Ability, don't add it.
		if(this.abilityInventory.ContainsKey(abilityName) == true)
			return false;

		// Instantiate the Ability.
		GameObject ability = GameObject.Instantiate(Resources.Load("Prefabs/Abilities/" + abilityName)) as GameObject;

		// Set the Parent transform so that it matches the Spaceships Transform
		ability.transform.parent = this.transform;
		// Set the Rotation so that it matches the Spaceships Rotation
		ability.transform.rotation = this.transform.rotation;
		// Set the Position so that it matches the Spaceships Position
		ability.transform.position = this.transform.position + this.transform.forward * 1.0f + this.transform.up * 2.0f;
		// Set the Active state to false so that it doesn't get used
		ability.SetActive(false);
		
		// Add the Ability to the inventory	
		this.abilityInventory.Add(abilityName, ability);
		
		//Debug.Log("Gained the ability " + abilityName + "!");
		
		return true;
	}
	#endregion

	#region Gizmos
	public void OnDrawGizmos() {	
		
		if(backLeftCorner != null && backRightCorner != null && frontLeftCorner != null && frontRightCorner != null) {
			
			// Spaceship Rotation Ajustments
			RaycastHit backLeftHit;
			RaycastHit backRightHit;
			RaycastHit frontLeftHit;
			RaycastHit frontRightHit;
			
			// Cast Rays from each of the Spaceships Corners heading towards the Track
			bool backLeftRay = Physics.Raycast(backLeftCorner.position, -this.transform.up, out backLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
			bool backRightRay = Physics.Raycast(backRightCorner.position, -this.transform.up, out backRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
			bool frontLeftRay = Physics.Raycast(frontLeftCorner.position, -this.transform.up, out frontLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
			bool frontRightRay = Physics.Raycast(frontRightCorner.position, -this.transform.up, out frontRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
			
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
				
				Gizmos.DrawSphere(this.transform.position + this.transform.forward * 5.0f, 1.0f);
			}
		}
	}
	#endregion
}
