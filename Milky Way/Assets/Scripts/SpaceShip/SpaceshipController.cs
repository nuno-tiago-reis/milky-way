using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpaceshipController : MonoBehaviour {

	/* Spaceships Health */ 
	public float health
	{ get; protected set; }
	public float maximumHealth
	{ get; protected set; }
	public float minimumHealth
	{ get; protected set; }

	/* Spaceships Gold */ 
	public int gold
	{ get; protected set; }
	
	/* Spaceships Ability Map */
	public Dictionary<string, GameObject> abilityInventory;

	/* Spaceships Controller Attributes */
	public float speed
	{ get; protected set; }
	public float steeringAngle
	{ get; protected set; }
	
	public Transform backLeftCorner
	{ get; protected set; }
	public Transform backRightCorner
	{ get; protected set; }
	public Transform frontLeftCorner
	{ get; protected set; }
	public Transform frontRightCorner
	{ get; protected set; }

	#region Initialization
	// Use this for initialization
	public void Start () {
	
		// Spaceships starting Health
		this.health = 100.0f;
		this.maximumHealth = 100.0f;
		this.minimumHealth = 0.0f;

		// Spaceships starting Abilities
		this.abilityInventory = new Dictionary<string, GameObject>();

		// Spaceships steering values
		this.speed = 2;
		this.steeringAngle = 10;

		// Spaceships reference to the hitboxes corners
		this.backLeftCorner = this.transform.FindChild("Back Left Corner");
		this.backRightCorner = this.transform.FindChild("Back Right Corner");
		this.frontLeftCorner = this.transform.FindChild("Front Left Corner");
		this.frontRightCorner = this.transform.FindChild("Front Right Corner");

		if(this.name == "Spaceship 1") {

			AddAbility("Shield");

			GameObject ability = null;
			
			if(abilityInventory.TryGetValue("Shield", out ability)) {
				
				AbilityController abilityController = ability.GetComponent<RocketController>();
				
				if(abilityController != null) {
					
					abilityController.Activate(this.transform);
					
					abilityInventory.Remove(abilityController.abilityName);
				}
			}
		}
			
	}
	#endregion

	#region GameLogic
	// Update is called once per frame
	public void Update () {

		if(Input.GetKey(KeyCode.Z) == true) {
				
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
	}
	#endregion

	#region Controller
	public void FixedUpdate() {
		
		// Spaceship Rotation Ajustments
		RaycastHit backLeftHit;
		RaycastHit backRightHit;
		RaycastHit frontLeftHit;
		RaycastHit frontRightHit;
		
		// Cast Rays from each of the Spaceships Corners heading towards the Track
		bool backLeftRay = Physics.Raycast(backLeftCorner.position, Vector3.down, out backLeftHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool backRightRay = Physics.Raycast(backRightCorner.position, Vector3.down, out backRightHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool frontLeftRay = Physics.Raycast(frontLeftCorner.position, Vector3.down, out frontLeftHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool frontRightRay = Physics.Raycast(frontRightCorner.position, Vector3.down, out frontRightHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
		
		if(backLeftRay == true && backRightRay == true && frontLeftRay == true && frontRightRay == true) {
			
			// If there is a Collision, adjust the Spaceships Rotation
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
				
				// Adjust the Spaceships Rotation so that it's parallel to the Track
				this.transform.LookAt(this.transform.position + forward * 5.0f, up);
			}
		}
		
		// Spaceships Position Adjustments
		RaycastHit centerHit;
		
		// Cast a Ray from he Spaceships Center heading towards the Track
		if(Physics.Raycast(this.transform.position, -this.transform.up, out centerHit, 3.0f, 1 << LayerMask.NameToLayer("Tracks")) == true) {
			
			// If there is a Collision, adjust the Spaceships Position
			if(centerHit.collider.tag == "Track") {
				
				// Adjust the Spaceships Position so that it's slightly above the Track.
				this.transform.position = centerHit.point + this.transform.up * 2.0f;
			}
		}

		if(this.name == "Spaceship 0") {
		
			// User Input
			float horizontalAxis = Input.GetAxis("Horizontal");
			float verticalAxis = Input.GetAxis("Vertical");
			
			// Update the Spaceships Acceleration
			
			// Accelerator
			if((Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) == true) && Input.GetKey(KeyCode.B) == false) {
				
				if(this.speed < 2.5f)
					this.speed += 0.05f;
				
				this.rigidbody.velocity += this.transform.forward * verticalAxis * speed;
			}
			else {
				
				this.speed = 0.0f;
			}
			
			// Brake
			if(Input.GetKey(KeyCode.B) == true) {
				
				this.rigidbody.velocity = this.rigidbody.velocity * 0.9f;
				this.rigidbody.angularVelocity = this.rigidbody.angularVelocity * 0.9f;
			}
			
			if(horizontalAxis != 0) {
				
				this.transform.Rotate(this.transform.forward, -horizontalAxis * 0.25f);
				// Update the Spaceships Steering
				//this.rigidbody.AddTorque(this.transform.up * horizontalAxis * steeringAngle, ForceMode.VelocityChange);
				this.rigidbody.AddTorque(this.transform.up * horizontalAxis * steeringAngle, ForceMode.Acceleration);
			}
		}
	}
	public void InflictDamage(float damage) {

		this.health = Mathf.Clamp(this.health - damage, this.minimumHealth, this.maximumHealth);

		Debug.Log("Took Damage (" + damage + ")! Remaining Health is " + this.health + "!");
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

		if(abilityName != "Shield")
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
			
			RaycastHit backLeftHit;
			RaycastHit backRightHit;
			RaycastHit frontLeftHit;
			RaycastHit frontRightHit;
			
			bool backLeftRay = Physics.Raycast(backLeftCorner.position, Vector3.down, out backLeftHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
			bool backRightRay = Physics.Raycast(backRightCorner.position, Vector3.down, out backRightHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
			bool frontLeftRay = Physics.Raycast(frontLeftCorner.position, Vector3.down, out frontLeftHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
			bool frontRightRay = Physics.Raycast(frontRightCorner.position, Vector3.down, out frontRightHit, 5.0f, 1 << LayerMask.NameToLayer("Tracks"));
			
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
