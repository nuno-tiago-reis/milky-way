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

	public const float healthZero = 100.0f;
	public const float healthIncrement = 50.0f;

	// Spaceships Weapon Power
	public float power
	{ get; protected set; }
	public float maximumPower
	{ get; protected set; }
	public float minimumPower
	{ get; protected set; }

	public const float powerZero = 5.0f;
	public const float powerIncrement = 5.0f;

	// Spaceships Controller Attributes
	public float acceleration
	{ get; protected set; }
	public float maximumAcceleration
	{ get; protected set; }
	public float minimumAcceleration
	{ get; protected set; }

	public const float accelerationStep = 5.0f;

	public const float accelerationZero = 2.5f;
	public const float accelerationIncrement = 1.25f;
	
	public float handling
	{ get; protected set; }
	public float maximumHandling
	{ get; protected set; }
	public float minimumHandling
	{ get; protected set; }

	public const float handlingZero = 10.0f;
	public const float handlingIncrement = 5.0f;

	// Spaceships Timeout counter
	public float repairTime
	{ get; protected set; }
	public float maximumRepairTime
	{ get; protected set; }
	public float minimumRepairTime
	{ get; protected set; }

	// Spaceships Gold
	public int gold
	{ get; protected set; }
	
	// Spaceships PowerUp List
	public List<string> powerUpList;

	// Spaceships Direction Vectors
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

	// Spaceships Shooter Controller
	public ShooterController shooter
	{ get; protected set; }
	// Spaceships Joystick Controller
	public JoystickController joystick
	{ get; protected set; }

	// Spaceships Race Record
	public RaceRecord raceRecord
	{ get; protected set; }

	public void Awake () {
	
		// Spaceships starting Health
		this.health = 0.0f;
		this.maximumHealth = 0.0f;
		this.minimumHealth = 0.0f;
		
		// Spaceships starting Power
		this.power = 0.0f;
		this.maximumPower = 0.0f;
		this.minimumPower = 0.0f;
		
		// Spaceships starting Speed
		this.acceleration = 0.0f;
		this.maximumAcceleration = 0.0f;
		this.minimumAcceleration = 0.0f;
		
		// Spaceships starting Handling
		this.handling = 0.0f;
		this.maximumHandling = 0.0f;
		this.minimumHandling = 0.0f;

		// Spaceships Repair Time
		this.repairTime = 0.0f;
		this.maximumRepairTime = 2.5f;
		this.minimumRepairTime = 1.0f;

		// Spaceships starting Abilities
		this.powerUpList = new List<string>();

		// Spaceships reference to its Children
		this.model = this.transform.FindChild("Model");

		this.backLeftCorner = this.transform.FindChild("Back Left Corner");
		this.backRightCorner = this.transform.FindChild("Back Right Corner");
		this.frontLeftCorner = this.transform.FindChild("Front Left Corner");
		this.frontRightCorner = this.transform.FindChild("Front Right Corner");

		// Spaceships reference to its Rocket
		this.shooter = this.transform.GetComponent<ShooterController>();

		// Spaceships reference to its Joystick
		this.joystick = this.transform.GetComponent<JoystickController>();

		// Spaceships Race Record
		this.raceRecord = new RaceRecord();

		Initialize(new SpaceshipConfiguration(5,5,5,5));
	}

	public void Initialize(SpaceshipConfiguration spaceshipConfiguration) {

		// Health = starting value + a increment for each point spent
		this.minimumHealth = 0.0f;
		this.maximumHealth = SpaceshipController.healthZero + SpaceshipController.healthIncrement * spaceshipConfiguration.health;

		this.health = SpaceshipController.healthZero + SpaceshipController.healthIncrement * spaceshipConfiguration.health;

		// Power = starting value + a increment for each point spent
		this.minimumPower = SpaceshipController.powerZero;
		this.maximumPower = SpaceshipController.powerZero + SpaceshipController.powerIncrement * 5.0f;

		this.power = SpaceshipController.powerZero + SpaceshipController.powerIncrement * spaceshipConfiguration.power;

		// Speed = starting value + a increment for each point spent
		this.minimumAcceleration = 0.0f;
		this.maximumAcceleration = SpaceshipController.accelerationZero + SpaceshipController.accelerationIncrement * 5.0f;

		this.acceleration = 0.0f;

		// Handling = starting value + a increment for each point spent
		this.minimumHandling = SpaceshipController.handlingZero;
		this.maximumHandling = SpaceshipController.handlingZero + SpaceshipController.handlingIncrement * 5.0f;

		this.handling = SpaceshipController.handlingZero + SpaceshipController.handlingIncrement * spaceshipConfiguration.handling;

		Debug.Log("SpaceshipController = " + spaceshipConfiguration.handling);
		Debug.Log("Handling = " + this.handling);
	}

	public void FixedUpdate() {

		// Road-sticking
		bool positionStatus = CheckPosition();

		if(positionStatus == false)
			return;

		// Health Test
		bool healthStatus = CheckHealth();

		if(healthStatus == false)
			return;

		// Movement Input
		bool movementStatus = CheckMovement();

		if(movementStatus == false)
			return;

		// Ability Input
		bool abilityStatus = CheckAbilities();
		
		if(abilityStatus == false)
			return;
	}

	public bool CheckPosition() {

		// Spaceship Rotation Ajustments
		RaycastHit backLeftHit;
		RaycastHit backRightHit;
		RaycastHit frontLeftHit;
		RaycastHit frontRightHit;
		
		// Cast Rays from each of the Spaceships Corners heading towards the Track
		bool backLeftRay = Physics.Raycast(backLeftCorner.position + this.transform.up * 5.0f, -this.transform.up, out backLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool backRightRay = Physics.Raycast(backRightCorner.position + this.transform.up * 5.0f, -this.transform.up, out backRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool frontLeftRay = Physics.Raycast(frontLeftCorner.position + this.transform.up * 5.0f, -this.transform.up, out frontLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		bool frontRightRay = Physics.Raycast(frontRightCorner.position + this.transform.up * 5.0f, -this.transform.up, out frontRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
		
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
		if(Physics.Raycast(this.transform.position + this.transform.up * 5.0f, -this.transform.up, out centerHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks")) == true) {
			
			// If there is a Collision, adjust the Spaceships Position
			if(centerHit.collider.tag == "Road") {
				
				// Adjust the Spaceships Position so that it's slightly above the Track.
				this.transform.position = centerHit.point + this.transform.up * 2.5f;
			}
		}

		return true;
	}

	public bool CheckHealth() {

		if(this.health > 0.0f)
			return true;
			
		this.repairTime = Mathf.Clamp(this.repairTime + Time.deltaTime, 0.0f, this.maximumRepairTime);

		bool repair = Input.GetKey(this.joystick.circle) == true || (Input.GetKey(KeyCode.E) && this.id == 1) || (Input.GetKey(KeyCode.R) && this.id == 2);

		// If the Player wants to move before the repairs are complete
		if(repair == true && this.repairTime <= this.maximumRepairTime && this.repairTime >= minimumRepairTime) {

			this.health = this.maximumHealth * (this.repairTime / this.maximumRepairTime);
			
			this.repairTime = 0.0f;

			return true;
		}

		// If the repairs are complete
		if(this.repairTime == maximumRepairTime) {
			
			this.health = this.maximumHealth;
			
			this.repairTime = 0.0f;

			return true;
		}
			
		Debug.Log("Repair Time = " + this.repairTime + " seconds");
			
		return false;
	}

	public bool CheckMovement() {

		this.acceleration = this.acceleration * 0.99f;

		// Accelerator = Cross & Brake = Square
		bool accelerator = 
			(Input.GetKey(this.joystick.cross) == true && Input.GetKey(this.joystick.square) == false) ||			// Joystick
			(Input.GetKey(KeyCode.W) == true && Input.GetKey(KeyCode.Q) == false && this.id == 1) ||		// PC Player 1
			(Input.GetKey(KeyCode.UpArrow) == true && Input.GetKey(KeyCode.B) == false && this.id == 2);	// PC Player 2

		if(accelerator == true) {
			
			// "Shifts"
			this.acceleration = 
				Mathf.Clamp(this.acceleration + 
				            SpaceshipController.accelerationStep * (this.maximumAcceleration / (this.acceleration + 2.5f)), this.minimumAcceleration, this.maximumAcceleration);
		}

		// Reverse = Triangle & Brake = Square
		bool reverse = 
			(Input.GetKey(this.joystick.triangle) == true && Input.GetKey(this.joystick.square) == false) ||			//Joystick
			(Input.GetKey(KeyCode.S) == true && Input.GetKey(KeyCode.Q) == false && this.id == 1) ||		// PC Player 1
			(Input.GetKey(KeyCode.DownArrow) == true && Input.GetKey(KeyCode.B) == false && this.id == 2);	// PC Player 2

		if(reverse == true) {
			
			// "Shifts"
			this.acceleration = -this.maximumAcceleration * 0.50f;
		}

		// Brake = Square
		bool brake = 
			(Input.GetKey(this.joystick.square) == true) ||			// Joystick
			(Input.GetKey(KeyCode.Q) == true && this.id == 1) ||	// PC Player 1
			(Input.GetKey(KeyCode.B) == true && this.id == 2);		// PC Player 2

		if(brake == true) {
			
			// Reduce the Spaceships Velocity (Acceleration)
			this.rigidbody.velocity = this.rigidbody.velocity * 0.99f;

			// Reduce the Spaceships Angular Velocity (Steering)
			this.rigidbody.angularVelocity = this.rigidbody.angularVelocity * 0.99f;
		}

		if(Mathf.Abs(this.acceleration) < 0.05f)
			this.acceleration = 0.0f;
		
		// Steering = Stick and Directional Pad
		float horizontalAxis = 
			Input.GetAxis(this.joystick.horizontalAxis);
	
		if(horizontalAxis == 0.0f && this.id == 1)
			horizontalAxis = Input.GetAxis("Horizontal Axis PC 1");
		else if(horizontalAxis == 0.0f && this.id == 2)
			horizontalAxis = Input.GetAxis("Horizontal Axis PC 2");

		if(horizontalAxis != 0.0f) {
			
			float angle = this.model.localRotation.eulerAngles.z;
			
			if(angle > 180.0f)
				angle -= 360.0f;
			
			if(angle <  45.0f && horizontalAxis < 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);
			
			if(angle > -45.0f && horizontalAxis > 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);
			
			this.acceleration *= 0.85f;

			horizontalAxis *= Mathf.Sign(this.acceleration);
			
			// Increment the Spaceships Angular Velocity
			this.rigidbody.AddTorque(this.transform.up * horizontalAxis * handling, ForceMode.Impulse);
		}
		
		// Increment the Spaceships Velocity
		this.rigidbody.velocity += this.transform.forward * acceleration;

		return true;
	}

	public bool CheckAbilities() {

		// Rockets - L1
		bool shoot = 
			(Input.GetKey(this.joystick.L1) == true) || 
			(Input.GetKey(KeyCode.F1) == true && this.id == 1) ||
			(Input.GetKey(KeyCode.Alpha1) == true && this.id == 2);

		if(shoot == true) {

			this.shooter.Shoot();
		}
		
		// Shield - R1
		bool shield = 
			(Input.GetKey(this.joystick.R1) == true) || 
			(Input.GetKey(KeyCode.F2) == true && this.id == 1) ||
			(Input.GetKey(KeyCode.Alpha2) == true && this.id == 2);

		if(shield == true && powerUpList.Contains("Shield")) {
				
			PowerUp powerUp = this.transform.gameObject.GetComponent<ShieldPowerUp>();

			powerUp.Activate();
					
			powerUpList.Remove(powerUp.powerUpName);
		}
		
		// Smokescreen - L2
		bool smokescreen = 
			(Input.GetKey(this.joystick.L2) == true) || 
			(Input.GetKey(KeyCode.F3) == true && this.id == 1) ||
			(Input.GetKey(KeyCode.Alpha3) == true && this.id == 2);

		if(smokescreen == true && powerUpList.Contains("Smokescreen")) {
			
			PowerUp powerUp = this.transform.gameObject.GetComponent<SmokescreenPowerUp>();
			
			powerUp.Activate();
				
			powerUpList.Remove(powerUp.powerUpName);
		}

		return true;
	}

	public void InflictDamage(float damage) {

		this.health = Mathf.Clamp(this.health - damage, this.minimumHealth, this.maximumHealth);

		this.acceleration *= 0.75f;

		Debug.Log("Took Damage (" + damage + ")! Remaining Health is " + this.health + "!");
	}

	public void OnCollisionEnter(Collision collision) {
		
		// If colliding with a track boundary
		if(collision.collider.transform.tag == "Boundary") {

			this.acceleration *= 0.5f;

			Debug.Log("Boundary Hit!");

			foreach(ContactPoint contactPoint in collision.contacts) {

				Vector3 contactNormal = contactPoint.normal;

				Debug.Log("Repulsion!");

				this.rigidbody.velocity = Vector3.Reflect(this.rigidbody.velocity.normalized, contactNormal.normalized) * this.rigidbody.velocity.magnitude * 0.75f; 
			}
		}
	}

	#region Items
	public bool AddGold(int value) {

		//Debug.Log("AddGold(" + value + ")");

		this.gold += value;

		return true;
	}
	
	public bool AddPowerUp(string powerUpName) {

		//Debug.Log("AddPowerUp(" + powerUpName + ")");

		// If the Spaceship already has this Ability, don't add it.
		if(this.powerUpList.Contains(powerUpName) == true)
			return false;

		this.transform.gameObject.AddComponent(powerUpName + "PowerUp");
		
		// Add the PowerUp to the inventory	
		this.powerUpList.Add(powerUpName);
		
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
