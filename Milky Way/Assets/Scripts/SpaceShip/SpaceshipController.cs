using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpaceshipController : MonoBehaviour {

	// Spaceships Race Record - Defines the current standings, laps adnd lap times.
	public RaceRecord raceRecord
	{ get; protected set; }

	// Spaceships Health Attributes

		// Defines how much damage the Spaceship can take before needing repairs.
		public float health
		{ get; protected set; }
		public float maximumHealth
		{ get; protected set; }
		public float minimumHealth
		{ get; protected set; }

		// Defines the Spaceships basic Health.
		public const float healthZero = 100.0f;
		// Defines the Spaceships extra Health for each point spent in the Spaceship Configuration.
		public const float healthIncrement = 50.0f;

	// Spaceships Weapon Power Attributes

		//Defines how much damage the Spaceship deals with its basic weapon.
		public float weaponPower
		{ get; protected set; }
		public float maximumWeaponPower
		{ get; protected set; }
		public float minimumWeaponPower
		{ get; protected set; }

		// Defines the Spaceships basic Weapon Power.
		public const float powerZero = 5.0f;
		// Defines the Spaceships extra Weapon Power for each point spent in the Spaceship Configuration.
		public const float powerIncrement = 5.0f;

	// Spaceships Acceleration Attributes

		// Defines how fast the Spaceship can move.
		public float acceleration
		{ get; protected set; }
		public float maximumAcceleration
		{ get; protected set; }
		public float minimumAcceleration
		{ get; protected set; }

		// Defines the Spaceships basic Acceleration step.
		public const float accelerationStep = 5.0f;

		// Defines the Spaceships basic Acceleration.
		public const float accelerationZero = 2.5f;
		// Defines the Spaceships extra Acceleration for each point spent in the Spaceship Configuration.
		public const float accelerationIncrement = 1.25f;

	// Spaceships Handling Attributes

		// Defines how fast the Spaceship can turn.
		public float handling
		{ get; protected set; }
		public float maximumHandling
		{ get; protected set; }
		public float minimumHandling
		{ get; protected set; }

		// Defines the Spaceships basic Handling.
		public const float handlingZero = 10.0f;
		// Defines the Spaceships extra Handling for each point spent in the Spaceship Configuration.
		public const float handlingIncrement = 5.0f;

	// Spaceships Reparation Attributes

		// Defines how much time the Spaceship needs to regain Health.
		public float repairTime
		{ get; protected set; }
		public float maximumRepairTime
		{ get; protected set; }
		public float minimumRepairTime
		{ get; protected set; }

	// Spaceships Gold - Defines the score accumulated from the collected Star Fragments during the Race.
	public int gold
	{ get; protected set; }

	// Spaceships PowerUp List - Defines the PowerUps collected during the Race.
	public List<string> powerUpList;

	// Spaceships Orientation Attributes

		// Defines the new Orientation Vectors used to align the Spaceship to the Track.
		public Vector3 up
		{ get; protected set; }
		public Vector3 right
		{ get; protected set; }
		public Vector3 forward
		{ get; protected set; }

		// Defines how fast the Spaceship adjusts to its new Orientation
		public float directionInterpolator
		{ get; protected set; }

	// Spaceships Children Transforms

		// Used to Align the Model according to the Track Orientation and the Spaceships Movement.
		public Transform model
		{ get; protected set; }

		// Used to Calculate the Orientation Vectors to Align the Model according to the Track Orientation.
		public Transform backLeftCorner
		{ get; protected set; }
		public Transform backRightCorner
		{ get; protected set; }
		public Transform frontLeftCorner
		{ get; protected set; }
		public Transform frontRightCorner
		{ get; protected set; }

	// Spaceships Shooter Controller - Controls the Spaceships Basic Weapon
	public ShooterController shooter
	{ get; protected set; }

	// Spaceships Joystick Controller - Controls the Spaceships Joystick Input
	public JoystickController joystick
	{ get; protected set; }

	public void Awake () {
	}

	public void Initialize(int id) {

		// Spaceships Race Record
		this.raceRecord = new RaceRecord(id);
		
		// Spaceships starting, maximum and minimum Health values are defined by the SpaceshipConfiguration.
		this.health = 0.0f;
		this.maximumHealth = 0.0f;
		this.minimumHealth = 0.0f;
		
		// Spaceships starting, maximum and minimum shooting Power values are defined by the SpaceshipConfiguration.
		this.weaponPower = 0.0f;
		this.maximumWeaponPower = 0.0f;
		this.minimumWeaponPower = 0.0f;
		
		// Spaceships starting, maximum and minimum acceleration values are defined by the SpaceshipConfiguration.
		this.acceleration = 0.0f;
		this.maximumAcceleration = 0.0f;
		this.minimumAcceleration = 0.0f;
		
		// Spaceships starting, maximum and minimum handling values are defined by the SpaceshipConfiguration.
		this.handling = 0.0f;
		this.maximumHandling = 0.0f;
		this.minimumHandling = 0.0f;
		
		// Initialize the Spaceships Repair Time.
		this.repairTime = 0.0f;
		this.maximumRepairTime = 2.5f;
		this.minimumRepairTime = 1.0f;
		
		// Initialize the Spaceships Gold.
		this.gold = 0;
		// Initialize the Spaceships PowerUp List.
		this.powerUpList = new List<string>();
		
		// Initialize the Spaceships reference to its Children.
		this.model = this.transform.FindChild("Model");
		
		this.backLeftCorner = this.transform.FindChild("Back Left Corner");
		this.backRightCorner = this.transform.FindChild("Back Right Corner");
		this.frontLeftCorner = this.transform.FindChild("Front Left Corner");
		this.frontRightCorner = this.transform.FindChild("Front Right Corner");
		
		// Initialize the Shooter Controller
		this.shooter = this.transform.GetComponent<ShooterController>();
		// Initialize the Joystick Controller
		this.joystick = this.transform.GetComponent<JoystickController>();

		// Initialize the Trackers Material
		Transform tracker = this.transform.FindChild("Tracker");

		Material trackerMaterial = Resources.Load("Materials/Spaceships/Trackers/Marker " + this.raceRecord.id) as Material;

		for(int index=0; index<tracker.childCount;index++) {

			Transform trackerChild = tracker.GetChild(index);

			MeshRenderer trackerChildRenderer = trackerChild.GetComponent<MeshRenderer>();
			trackerChildRenderer.material = trackerMaterial;
		}

		MeshRenderer spaceshipRenderer = this.transform.FindChild("Model").GetComponent<MeshRenderer>();
		spaceshipRenderer.material = trackerMaterial;

		/*SpaceshipConfiguration spaceshipConfiguration = loadFile(this.raceRecord.id);

		Debug.Log(this.raceRecord.id + " Health = " + spaceshipConfiguration.health);
		Debug.Log(this.raceRecord.id + " Power = " + spaceshipConfiguration.power);
		Debug.Log(this.raceRecord.id + " Speed = " + spaceshipConfiguration.acceleration);
		Debug.Log(this.raceRecord.id + " Handling = " + spaceshipConfiguration.handling);
		
		Initialize(spaceshipConfiguration);*/

		// Initialize the Health, Weapon Power, Acceleration and Handling Attributes according to the SpaceshipConfiguration.
		Initialize(new SpaceshipConfiguration(5,5,5,5));
	}

	public void Initialize(SpaceshipConfiguration spaceshipConfiguration) {

		// Health = starting value + a increment for each point spent
		this.minimumHealth = 0.0f;
		this.maximumHealth = SpaceshipController.healthZero + SpaceshipController.healthIncrement * spaceshipConfiguration.health;

		this.health = SpaceshipController.healthZero + SpaceshipController.healthIncrement * spaceshipConfiguration.health;

		// Power = starting value + a increment for each point spent
		this.minimumWeaponPower = SpaceshipController.powerZero;
		this.maximumWeaponPower = SpaceshipController.powerZero + SpaceshipController.powerIncrement * 5.0f;

		this.weaponPower = SpaceshipController.powerZero + SpaceshipController.powerIncrement * spaceshipConfiguration.power;

		// Speed = starting value + a increment for each point spent
		this.minimumAcceleration = 0.0f;
		this.maximumAcceleration = SpaceshipController.accelerationZero + SpaceshipController.accelerationIncrement * 5.0f;

		this.acceleration = 0.0f;

		// Handling = starting value + a increment for each point spent
		this.minimumHandling = SpaceshipController.handlingZero;
		this.maximumHandling = SpaceshipController.handlingZero + SpaceshipController.handlingIncrement * 5.0f;

		this.handling = SpaceshipController.handlingZero + SpaceshipController.handlingIncrement * spaceshipConfiguration.handling;
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

		// PowerUp Input
		bool powerUpStatus = CheckPowerUps();
		
		if(powerUpStatus == false)
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

		// Adjust the Spaceships Rotation so that it's parallel to the Track
		this.transform.LookAt(this.transform.position + this.forward * 5.0f, this.up);
		
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

		bool repair = Input.GetKey(this.joystick.circle) == true || (Input.GetKey(KeyCode.E) && this.raceRecord.id == 1) || (Input.GetKey(KeyCode.R) && this.raceRecord.id == 2);

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
			
		return false;
	}

	public bool CheckMovement() {

		this.acceleration = this.acceleration * 0.95f;

		// Accelerator = Cross & Brake = Square
		bool accelerator = 
			(Input.GetKey(this.joystick.cross) == true && Input.GetKey(this.joystick.square) == false) ||			// Joystick
			(Input.GetKey(KeyCode.W) == true && Input.GetKey(KeyCode.Q) == false && this.raceRecord.id == 1) ||		// PC Player 1
			(Input.GetKey(KeyCode.UpArrow) == true && Input.GetKey(KeyCode.B) == false && this.raceRecord.id == 2);	// PC Player 2

		if(accelerator == true) {
			
			// "Shifts"
			this.acceleration = 
				Mathf.Clamp(this.acceleration + SpaceshipController.accelerationStep * (this.maximumAcceleration / (this.acceleration + 2.5f)), 
				            this.minimumAcceleration, this.maximumAcceleration);

			if(this.raceRecord.id == 1)
				this.acceleration *= 1.325f;

			if(this.raceRecord.currentStanding > 1)
				this.acceleration *= 1.15f;
		}

		// Reverse = Triangle & Brake = Square
		bool reverse = 
			(Input.GetKey(this.joystick.triangle) == true && Input.GetKey(this.joystick.square) == false) ||			//Joystick
			(Input.GetKey(KeyCode.S) == true && Input.GetKey(KeyCode.Q) == false && this.raceRecord.id == 1) ||			// PC Player 1
			(Input.GetKey(KeyCode.DownArrow) == true && Input.GetKey(KeyCode.B) == false && this.raceRecord.id == 2);	// PC Player 2

		if(reverse == true) {
			
			// "Shifts"
			this.acceleration = -this.maximumAcceleration * 0.50f;
		}

		// Brake = Square
		bool brake = 
			(Input.GetKey(this.joystick.square) == true) ||					// Joystick
			(Input.GetKey(KeyCode.Q) == true && this.raceRecord.id == 1) ||	// PC Player 1
			(Input.GetKey(KeyCode.B) == true && this.raceRecord.id == 2);	// PC Player 2

		if(brake == true) {

			this.acceleration = 0.0f;
			
			// Reduce the Spaceships Velocity (Acceleration)
			this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity * 0.95f;

			// Reduce the Spaceships Angular Velocity (Steering)
			this.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity * 0.95f;
		}

		if(Mathf.Abs(this.acceleration) < 0.05f)
			this.acceleration = 0.0f;
		
		// Steering = Stick and Directional Pad
		float horizontalAxis = 
			Input.GetAxis(this.joystick.horizontalAxis);
	
		if(horizontalAxis == 0.0f && this.raceRecord.id == 1)
			horizontalAxis = Input.GetAxis("Horizontal Axis PC 1");
		else if(horizontalAxis == 0.0f && this.raceRecord.id == 2)
			horizontalAxis = Input.GetAxis("Horizontal Axis PC 2");

		if(horizontalAxis != 0.0f) {
			
			float angle = this.model.localRotation.eulerAngles.z;
			
			if(angle > 180.0f)
				angle -= 360.0f;
			
			if(angle <  45.0f && horizontalAxis < 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);
			
			if(angle > -45.0f && horizontalAxis > 0.0f)
				this.model.RotateAround(this.model.position, this.model.forward, -horizontalAxis * 0.50f);
			
			this.acceleration *= 0.75f;

			horizontalAxis *= Mathf.Sign(this.acceleration) * 50.0f;
			
			// Increment the Spaceships Angular Velocity
			this.GetComponent<Rigidbody>().AddTorque(this.transform.up * horizontalAxis * handling, ForceMode.Force);
		}
		
		// Increment the Spaceships Velocity
		this.GetComponent<Rigidbody>().AddForce(this.transform.forward * acceleration* 500.0f, ForceMode.Force);

		return true;
	}

	public bool CheckPowerUps() {

		// Lasers - L1
		bool laser = 
			(Input.GetKey(this.joystick.L1) == true) || 
			(Input.GetKey(KeyCode.F1) == true && this.raceRecord.id == 1) ||
			(Input.GetKey(KeyCode.Alpha1) == true && this.raceRecord.id == 2);

		if(laser == true) {

			this.shooter.Shoot();
		}

		// Rocket - L2
		bool homingRocket = 
			(Input.GetKey(this.joystick.L2) == true) || 
				(Input.GetKey(KeyCode.F2) == true && this.raceRecord.id == 1) ||
				(Input.GetKey(KeyCode.Alpha2) == true && this.raceRecord.id == 2);
		
		if(homingRocket == true && powerUpList.Contains("HomingRocket")) {
			
			PowerUp powerUp = this.transform.gameObject.GetComponent<HomingRocketPowerUp>();
			powerUp.Activate();
		}
		
		// Shield - R1
		bool shield = 
			(Input.GetKey(this.joystick.R1) == true) || 
			(Input.GetKey(KeyCode.F3) == true && this.raceRecord.id == 1) ||
			(Input.GetKey(KeyCode.Alpha3) == true && this.raceRecord.id == 2);

		if(shield == true && powerUpList.Contains("Shield")) {
				
			PowerUp powerUp = this.transform.gameObject.GetComponent<ShieldPowerUp>();

			powerUp.Activate();
		}
		
		// Smokescreen - R2
		bool smokescreen = 
			(Input.GetKey(this.joystick.R2) == true) || 
			(Input.GetKey(KeyCode.F4) == true && this.raceRecord.id == 1) ||
			(Input.GetKey(KeyCode.Alpha4) == true && this.raceRecord.id == 2) ||
            (Input.GetKey(KeyCode.C) == true) ;

		if(smokescreen == true && powerUpList.Contains("Smokescreen")) {
			
			PowerUp powerUp = this.transform.gameObject.GetComponent<SmokescreenPowerUp>();

			powerUp.Activate();
		}

		return true;
	}

	public void InflictDamage(float damage) {

		this.health = Mathf.Clamp(this.health - damage, this.minimumHealth, this.maximumHealth);

		this.acceleration *= 0.75f;
	}

	public void OnCollisionEnter(Collision collision) {
		
		// If colliding with a track boundary
		if(collision.collider.transform.tag == "Boundary") {

			this.acceleration *= 0.5f;

			foreach(ContactPoint contactPoint in collision.contacts) {

				// Spaceships Position Adjustments
				RaycastHit centerHit;
				
				// Cast a Ray from he Spaceships Center heading towards the Track
				if(Physics.Raycast(this.transform.position + this.transform.up * 5.0f, -this.transform.up, out centerHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks")) == true) {
					
					// If there is a Collision, adjust the Spaceships Position
					if(centerHit.collider.tag == "Road") {

						Vector3 contactNormal = contactPoint.point - centerHit.point;
						contactNormal.Normalize();
						
						Vector3 contactDirection = this.transform.forward - Vector3.Project(this.transform.forward, contactNormal);
						contactDirection.Normalize();

						
						this.GetComponent<Rigidbody>().velocity = -this.GetComponent<Rigidbody>().velocity.magnitude * contactDirection;
						this.GetComponent<Rigidbody>().velocity *= 0.25f;

						this.transform.position -= contactDirection * 1.0f;
						
						/*Debug.Log("Contact Normal = " + contactNormal);
						Debug.Log("Contact Direction = " + contactDirection);
						Debug.Log("Contact Position = " + contactPoint.point);*/
					}
				}
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

		// If the Spaceship already has this Ability, don't add it.
		if(this.powerUpList.Contains(powerUpName) == true)
			return false;

		UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(this.transform.gameObject, "Assets/Scripts/SpaceShip/SpaceshipController.cs (563,3)", powerUpName + "PowerUp");
		
		// Add the PowerUp to the inventory	
		this.powerUpList.Add(powerUpName);

		return true;
	}

	public bool RemovePowerUp(string powerUpName) {

		// If the Spaceship doesn't have this Ability, don't remove it.
		if(this.powerUpList.Contains(powerUpName) == false)
			return false;

		// Remove the PowerUp from the inventory	
		this.powerUpList.Remove(powerUpName);

		// Destroy the PowerUp
		PowerUp[] powerUpList = this.transform.GetComponents<PowerUp>();

		foreach(PowerUp powerUp in powerUpList)
			if(powerUp.name == powerUpName)
				Destroy(powerUp.gameObject);

		return true;
	}
	#endregion

    public SpaceshipConfiguration loadFile(int id) {

        string fileName = "upgradesShip" + (id -1) + ".txt";

		SpaceshipConfiguration spaceshipConfiguration = new SpaceshipConfiguration();

        if (File.Exists(fileName)) {

            try
            {

                string line;

                StreamReader streamReader = new StreamReader(fileName);

                using (streamReader)
                {

                    do
                    {

                        line = streamReader.ReadLine();

                        if (line != null)
                        {

                            string[] entries = line.Split(' ');

                            if (entries.Length > 0)
                            {

                                // Upgrade Type, value
                               // upgradesMap.Add(entries[0], entries[1]);
								if(entries[0] == "Health")
									spaceshipConfiguration.SetHealth(int.Parse(entries[1]));

								if(entries[0] == "Power")
									spaceshipConfiguration.SetPower(int.Parse(entries[1]));

								if(entries[0] == "Speed")
									spaceshipConfiguration.SetAcceleration(int.Parse(entries[1]));

								if(entries[0] == "Handling")
									spaceshipConfiguration.SetHandling(int.Parse(entries[1]));
                            }
                        }
                    }
                    while (line != null);

                    streamReader.Close();

					return spaceshipConfiguration;
                }
            }
            catch (System.Exception e)
            {

                Debug.Log("Exception when reading the file!" + e.ToString());

                return null;
            }
        }
		return null;
    }

	#region Gizmos
	public void OnDrawGizmos() {	
		
		if(backLeftCorner != null && backRightCorner != null && frontLeftCorner != null && frontRightCorner != null) {
			
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
