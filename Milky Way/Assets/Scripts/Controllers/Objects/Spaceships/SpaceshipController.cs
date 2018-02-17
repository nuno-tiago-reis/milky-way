// Unity
using UnityEngine;

// System
using System.Linq;
using System.Collections.Generic;

// JetBrains
using JetBrains.Annotations;

// MilkyWay
using MilkyWay.Utility;
using MilkyWay.Managers;
using MilkyWay.Objects.PowerUps;

// ReSharper disable RedundantJumpStatement
namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements a generic Spaceship.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	public sealed class SpaceshipController : MonoBehaviour, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The base health value.
		/// </summary>
		public const float BaseHealth = 100.0f;
		/// <summary>
		/// The base health increment for each point spent in the configuration.
		/// </summary>
		public const float BaseHealthIncrement = 50.0f;

		/// <summary>
		/// The base handling value.
		/// </summary>
		public const float BaseHandling = 10.0f;
		/// <summary>
		/// The base handling increment for each point spent in the configuration.
		/// </summary>
		public const float BaseHandlingIncrement = 5.0f;

		/// <summary>
		/// The base weapon power value.
		/// </summary>
		public const float BaseWeaponPower = 5.0f;
		/// <summary>
		/// The base weapon power increment for each point spent in the configuration.
		/// </summary>
		public const float BaseWeaponPowerIncrement = 5.0f;

		/// <summary>
		/// The base acceleration value.
		/// </summary>
		public const float BaseAcceleration = 2.5f;
		/// <summary>
		/// The base acceleration increment for each point spent in the configuration.
		/// </summary>
		public const float BaseAccelerationIncrement = 1.25f;

		/// <summary>
		/// The default repair time.
		/// </summary>
		public const float DefaultRepairTime = 2.5f;

		/// <summary>
		/// The marker materials.
		/// </summary>
		private static Material[] MarkerMaterials { get; set; }

		#region [Loading]
		/// <summary>
		/// Loads the necessary attributes.
		/// </summary>
		[UsedImplicitly] [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void LoadStaticAttributes()
		{
			// Load the marker materials
			MarkerMaterials = new Material[]
			{
				ResourceManager.LoadMaterial("Objects/Spaceships/Markers/Marker 1"),
				ResourceManager.LoadMaterial("Objects/Spaceships/Markers/Marker 2"),
				ResourceManager.LoadMaterial("Objects/Spaceships/Markers/Marker 3"),
				ResourceManager.LoadMaterial("Objects/Spaceships/Markers/Marker 4")
			};
		}
		#endregion

		#endregion

		#region [Attributes]
		/// <summary>
		/// The spaceships identifier.
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// The spaceships race record.
		/// Contains the current standings, laps and lap times.
		/// </summary>
		public SpaceshipRecord Record { get; private set; }
		/// <summary>
		/// The spaceships race configurations.
		/// Contains the upgrade points used for this spaceship.
		/// </summary>
		public SpaceshipConfiguration Configuration { get; private set; }

		/// <summary>
		/// The current health value.
		/// Determines how much damage the spaceships can take before needing repairs.
		/// </summary>
		public float Health { get; private set; }
		/// <summary>
		/// The maximum health value.
		/// </summary>
		public float MaximumHealth { get; private set; }

		/// <summary>
		/// The current power value.
		/// Determines how much damage the spaceships basic attacks deal.
		/// </summary>
		public float WeaponPower { get; private set; }
		/// <summary>
		/// The maximum power value.
		/// </summary>
		public float MaximumWeaponPower { get; private set; }

		/// <summary>
		/// The current acceleration value.
		/// Determines how fast the spaceship can gain speed.
		/// </summary>
		public float Acceleration { get; private set; }
		/// <summary>
		/// The maximum acceleration value.
		/// </summary>
		public float MaximumAcceleration { get; private set; }

		/// <summary>
		/// The current handling value.
		/// Determines how well the spaceship can turn.
		/// </summary>
		public float Handling { get; private set; }
		/// <summary>
		/// The maximum handling value.
		/// </summary>
		public float MaximumHandling { get; private set; }

		/// <summary>
		/// The repair timer.
		/// Determines how fast the spaceship can repair itself.
		/// </summary>
		public Timer RepairTimer { get; private set; }
		
		/// <summary>
		/// The current star-dust value.
		/// Holds the amount of star-dust accumulated during a race.
		/// </summary>
		public int StarDust { get; private set; }
		/// <summary>
		/// The currently available power-ups.
		/// Holds the available power-ups accumulated during a race.
		/// </summary>
		public List<IPowerUp> PowerUpList { get; private set; }
		/// <summary>
		/// The added power-ups.
		/// Holds the added power-ups that weren't yet made available.
		/// </summary>
		public List<IPowerUp> AddedPowerUpList { get; private set; }
		/// <summary>
		/// The removed power-ups.
		/// Holds the removed power-ups that weren't yet made unavailable.
		/// </summary>
		public List<IPowerUp> RemovedPowerUpList { get; private set; }

		/// <summary>
		/// The spaceships up vector.
		/// </summary>
		public Vector3 SpaceshipUp { get; private set; }
		/// <summary>
		/// The spaceships right vector.
		/// </summary>
		public Vector3 SpaceshipRight { get; private set; }
		/// <summary>
		/// The spaceships forward vector.
		/// </summary>
		public Vector3 SpaceshipForward { get; private set; }

		/// <summary>
		/// The spaceships model container.
		/// Contains all the visual components.
		/// </summary>
		public Transform BodyContainer { get; private set; }
		/// <summary>
		/// The spaceships tracker container.
		/// Contains the tracking visual components.
		/// </summary>
		public Transform MarkerContainer { get; private set; }
		/// <summary>
		/// The spaceships corner container.
		/// Contains all the corners used for road sticking.
		/// </summary>
		public Transform CornerContainer { get; private set; }
		/// <summary>
		/// The spaceships shooter container.
		/// Contains all the shooters used for firing lasers.
		/// </summary>
		public Transform ShooterContainer { get; private set; }

		/// <summary>
		/// The spaceships back-left corner anchor.
		/// </summary>
		private Transform BackLeftCorner { get; set; }
		/// <summary>
		/// The spaceships back-SpaceshipRight corner anchor.
		/// </summary>
		private Transform BackRightCorner { get; set; }
		/// <summary>
		/// The spaceships front-left corner anchor.
		/// </summary>
		private Transform FrontLeftCorner { get; set; }
		/// <summary>
		/// The spaceships front-SpaceshipRight corner anchor.
		/// </summary>
		private Transform FrontRightCorner { get; set; }

		/// <summary>
		/// The model controller.
		/// Handles the spaceships model animations.
		/// </summary>
		private ModelController Model { get; set; }
		/// <summary>
		/// The shooter controller.
		/// Handles the spaceships trail animations.
		/// </summary>
		private TrailController Trail { get; set; }
		/// <summary>
		/// The marker controller.
		/// Handles the spaceships marker animations.
		/// </summary>
		private MarkerController Marker { get; set; }
		/// <summary>
		/// The shooter controller.
		/// Handles the spaceships basic attacks.
		/// </summary>
		private ShooterController Shooter { get; set; }
		/// <summary>
		/// The joystick controller.
		/// Handles input conversions from joysticks.
		/// </summary>
		private JoystickController Joystick { get; set; }

		/// <summary>
		/// The spaceships rigidbody.
		/// </summary>
		private Rigidbody Rigidbody { get; set; }

		/// <summary>
		/// The camera controller.
		/// Handles the spaceship-following.
		/// </summary>
		private CameraController Camera { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Initialize the identifier
			this.ID = GameManager.Instance.SpaceshipList.IndexOf(this) + 1;

			// Initialize the race-record
			this.Record = new SpaceshipRecord(this);
			// Initialize the configuration
			this.Configuration = SpaceshipConfiguration.LoadConfiguration(this.ID);

			// Initialize the health based on the configuration
			this.Health = this.MaximumHealth = BaseHealth + BaseHealthIncrement * this.Configuration.Health;
			// Initialize the handling based on the configuration
			this.Handling = this.MaximumHandling = BaseHandling + BaseHandlingIncrement * this.Configuration.Handling;
			// Initialize the weapon power based on the configuration
			this.WeaponPower = this.MaximumWeaponPower = BaseWeaponPower + BaseWeaponPowerIncrement * this.Configuration.WeaponPower;
			// Initialize the acceleration based on the configuration
			this.Acceleration = this.MaximumAcceleration = BaseAcceleration + BaseAccelerationIncrement * this.Configuration.Acceleration;

			// Reset the acceleration
			this.Acceleration = 0.0f;

			// Initialize the repair timer
			this.RepairTimer = new Timer(TimerMode.Countdown, DefaultRepairTime);

			// Initialize the star-dust
			this.StarDust = 0;
			// Initialize the power-up list
			this.PowerUpList = new List<IPowerUp>();
			this.AddedPowerUpList = new List<IPowerUp>();
			this.RemovedPowerUpList = new List<IPowerUp>();

			// Initialize the containers
			this.BodyContainer = this.transform.Find("Body");
			this.MarkerContainer = this.transform.Find("Markers");
			this.CornerContainer = this.transform.Find("Corners");
			this.ShooterContainer = this.transform.Find("Shooters");
			// Initialize the anchors
			this.BackLeftCorner = this.CornerContainer.Find("Back Left");
			this.BackRightCorner = this.CornerContainer.Find("Back Right");
			this.FrontLeftCorner = this.CornerContainer.Find("Front Left");
			this.FrontRightCorner = this.CornerContainer.Find("Front Right");

			// Initialize the models material
			MeshRenderer[] modelMeshRenderers = this.BodyContainer.GetComponentsInChildren<MeshRenderer>();
			foreach(MeshRenderer meshRenderer in modelMeshRenderers)
				meshRenderer.material = MarkerMaterials[this.ID];
			// Initialize the trackers material
			MeshRenderer[] markerMeshRenderers = this.MarkerContainer.GetComponentsInChildren<MeshRenderer>();
			foreach(MeshRenderer meshRenderer in markerMeshRenderers)
				meshRenderer.material = MarkerMaterials[this.ID];

			// Initialize the rigidbody
			this.Rigidbody = GetComponent<Rigidbody>();

			// Initialize the model controller
			this.Model = this.BodyContainer.GetComponentInChildren<ModelController>();
			this.Model.ObjectCreate();
			// Initialize the trail controller
			this.Trail = this.BodyContainer.GetComponentInChildren<TrailController>();
			this.Trail.ObjectCreate();
			// Initialize the marker controller
			this.Marker = this.MarkerContainer.GetComponentInChildren<MarkerController>();
			this.Marker.ObjectCreate();

			// Initialize the shooter controller
			this.Shooter = GetComponentInChildren<ShooterController>();
			this.Shooter.Spaceship = this;
			this.Shooter.ObjectCreate();
			// Initialize the joystick controller
			this.Joystick = new JoystickController();
			this.Joystick.Spaceship = this;
			this.Joystick.ObjectCreate();

			// Initialize the camera controller
			this.Camera = this.transform.parent.GetComponentInChildren<CameraController>();
			this.Camera.Spaceship = this;
			this.Camera.Initialize();
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Update the timers
			this.Record.TotalLapTime += Time.fixedDeltaTime;
			this.Record.CurrentLapTime += Time.fixedDeltaTime;

			// Update the repair timer
			this.RepairTimer.Update();

			// Update the controllers
			this.Model.ObjectUpdate();
			this.Trail.ObjectUpdate();
			this.Marker.ObjectUpdate();
			this.Shooter.ObjectUpdate();
			this.Joystick.ObjectUpdate();

			// Update the spaceships health
			if (UpdateHealth())
			{
				// Update the spaceships sticking to the track
				UpdateSticking();
				// Update the spaceships laser shooters and power-ups
				UpdateAbilities();
			}
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			// Update the controllers
			this.Model.ObjectDestroy();
			this.Trail.ObjectDestroy();
			this.Marker.ObjectDestroy();
			this.Shooter.ObjectDestroy();
			this.Joystick.ObjectDestroy();

			Destroy(this.gameObject);
		}

		#region [Methods] Update
		/// <summary>
		/// Checks the spaceships health.
		/// If the repair timer is finished, restores the spaceships health before returning.
		/// Returns true if the spaceship has more than 0 health remaining.
		/// </summary>
		public bool UpdateHealth()
		{
			// If the repairs are complete
			if(this.Health == 0.0f && this.RepairTimer.Finished)
				this.Health = this.MaximumHealth;

			return this.Health > 0.0f;
		}

		/// <summary>
		/// Updates the sticking.
		/// </summary>
		/// <returns></returns>
		public void UpdateSticking()
		{
			// Spaceship rotation ajustments
			RaycastHit backLeftHit;
			RaycastHit backRightHit;
			RaycastHit frontLeftHit;
			RaycastHit frontRightHit;

			// Cast rays from each of the spaceships corners heading towards the track
			bool backLeftRay = Physics.Raycast(BackLeftCorner.position + this.transform.up * 5.0f, -this.transform.up,
				out backLeftHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks));
			bool backRightRay = Physics.Raycast(BackRightCorner.position + this.transform.up * 5.0f, -this.transform.up,
				out backRightHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks));
			bool frontLeftRay = Physics.Raycast(FrontLeftCorner.position + this.transform.up * 5.0f, -this.transform.up,
				out frontLeftHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks));
			bool frontRightRay = Physics.Raycast(FrontRightCorner.position + this.transform.up * 5.0f, -this.transform.up,
				out frontRightHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks));

			if(backLeftRay && backRightRay && frontLeftRay && frontRightRay)
			{
				// If there is a hit, adjust the spaceships rotation
				if(backLeftHit.collider.tag == LayerManager.GetTagName(Tag.Road) && backRightHit.collider.tag == LayerManager.GetTagName(Tag.Road) &&
					frontLeftHit.collider.tag == LayerManager.GetTagName(Tag.Road) && frontRightHit.collider.tag == LayerManager.GetTagName(Tag.Road))
				{
					this.SpaceshipUp =
						Vector3.Cross(backRightHit.point - backRightHit.normal, backLeftHit.point - backLeftHit.normal) +
						Vector3.Cross(backLeftHit.point - backLeftHit.normal, frontLeftHit.point - frontLeftHit.normal) +
						Vector3.Cross(frontLeftHit.point - frontLeftHit.normal, frontRightHit.point - frontRightHit.normal) +
						Vector3.Cross(frontRightHit.point - frontRightHit.normal, backRightHit.point - backRightHit.normal);
					this.SpaceshipUp.Normalize();

					this.SpaceshipRight = this.transform.right;
					this.SpaceshipRight.Normalize();

					this.SpaceshipForward = Vector3.Cross(SpaceshipRight, SpaceshipUp);
					this.SpaceshipForward.Normalize();
				}
			}

			// Adjust the spaceships rotation so that it's parallel to the tracl
			this.transform.LookAt(this.transform.position + this.SpaceshipForward * 5.0f, this.SpaceshipUp);

			// Spaceships position adjustments
			RaycastHit centerHit;

			// Cast a Ray from he spaceships center heading towards the track
			if(Physics.Raycast(this.transform.position + this.transform.up * 5.0f, -this.transform.up, out centerHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks)))
			{
				// If there is a hit, adjust the spaceships position so that it's slightly above the track
				if(centerHit.collider.tag == LayerManager.GetTagName(Tag.Road))
					this.transform.position = centerHit.point + this.transform.up * 2.5f;
			}
		}

		/// <summary>
		/// Updates the abilities.
		/// </summary>
		public void UpdateAbilities()
		{
			// Clean the powerUp list
			this.PowerUpList.AddRange(this.AddedPowerUpList);
			this.PowerUpList.RemoveAll(powerUp => powerUp == null);
			this.PowerUpList.RemoveAll(powerUp => this.RemovedPowerUpList.Exists(existingPowerUp => existingPowerUp.Name == powerUp.Name));

			// Clean the temporary lists
			this.AddedPowerUpList.Clear();
			this.RemovedPowerUpList.Clear();

			// Update the power-ups
			this.PowerUpList.RemoveAll(powerUp => powerUp == null);
			foreach(IPowerUp powerUp in this.PowerUpList)
				powerUp.PowerUpUpdate();

			// Lasers
			if((Input.GetKey(this.Joystick.L1)) ||
				(this.Record.SpaceshipID == 1 && Input.GetKey(KeyCode.F1)) ||
				(this.Record.SpaceshipID == 2 && Input.GetKey(KeyCode.Alpha1)))
			{
				// Shoot the lasers
				this.Shooter.Shoot();
			}

			// Shield
			if((Input.GetKey(this.Joystick.R1)) ||
				(this.Record.SpaceshipID == 1 && Input.GetKey(KeyCode.F3)) ||
				(this.Record.SpaceshipID == 2 && Input.GetKey(KeyCode.Alpha3)))
			{
				// Retrieve the shield power-up
				IPowerUp shield = this.PowerUpList.FirstOrDefault(powerUp => powerUp is Shield);
				if(shield != null)
					shield.Activate(this);
			}

			// Smokescreen
			if((Input.GetKey(this.Joystick.R2)) ||
				(this.Record.SpaceshipID == 1 && Input.GetKey(KeyCode.F4)) ||
				(this.Record.SpaceshipID == 2 && Input.GetKey(KeyCode.Alpha4)))
			{
				// Retrieve the smokescreen power-up
				IPowerUp smokescreen = this.PowerUpList.FirstOrDefault(powerUp => powerUp is Smokescreen);
				if(smokescreen != null)
					smokescreen.Activate(this);
			}

			// Homing-Rocket
			if((Input.GetKey(this.Joystick.L2)) ||
			   (this.Record.SpaceshipID == 1 && Input.GetKey(KeyCode.F2)) ||
			   (this.Record.SpaceshipID == 2 && Input.GetKey(KeyCode.Alpha2)))
			{
				// Retrieve the homing-rocket power-up
				IPowerUp homingRocket = this.PowerUpList.FirstOrDefault(powerUp => powerUp is HomingRocket);
				if(homingRocket != null)
					homingRocket.Activate(this);
			}
		}

		/// <summary>
		/// Updates the movement. TODO
		/// </summary>
		/// <returns></returns>
		public void FixedUpdate()
		{
			this.Acceleration = this.Acceleration * 0.95f;

			// Accelerator = Cross & Brake = Square
			bool accelerator =
				(Input.GetKey(this.Joystick.Cross) && Input.GetKey(this.Joystick.Square) == false) || // Joystick
				(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Q) == false && this.Record.SpaceshipID == 1) || // PC Player 1
				(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.B) == false &&
				 this.Record.SpaceshipID == 2); // PC Player 2

			if(accelerator)
			{
				// "Shifts"
				this.Acceleration =
					Mathf.Clamp(
						this.Acceleration + 5.0f * (this.MaximumAcceleration / (this.Acceleration + 2.5f)), 0.0f, this.MaximumAcceleration);

				if(this.Record.SpaceshipID == 1)
					this.Acceleration *= 1.325f;

				if(this.Record.CurrentStanding > 1)
					this.Acceleration *= 1.15f;
			}

			// Reverse = Triangle & Brake = Square
			bool reverse =
				(Input.GetKey(this.Joystick.Triangle) && Input.GetKey(this.Joystick.Square) == false) || //Joystick
				(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Q) == false && this.Record.SpaceshipID == 1) || // PC Player 1
				(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.B) == false &&
				 this.Record.SpaceshipID == 2); // PC Player 2

			if(reverse)
			{
				// "Shifts"
				this.Acceleration = -this.MaximumAcceleration * 0.50f;
			}

			// Brake = Square
			bool brake =
				(Input.GetKey(this.Joystick.Square)) || // Joystick
				(Input.GetKey(KeyCode.Q) && this.Record.SpaceshipID == 1) || // PC Player 1
				(Input.GetKey(KeyCode.B) && this.Record.SpaceshipID == 2); // PC Player 2

			if(brake)
			{
				this.Acceleration = 0.0f;

				// Reduce the Spaceships Velocity (Acceleration)
				this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity * 0.95f;

				// Reduce the Spaceships Angular Velocity (Steering)
				this.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity * 0.95f;
			}

			if(Mathf.Abs(this.Acceleration) < 0.05f)
				this.Acceleration = 0.0f;

			// Steering = Stick and Directional Pad
			float horizontalAxis =
				Input.GetAxis(this.Joystick.HorizontalAxis);

			if(horizontalAxis == 0.0f && this.Record.SpaceshipID == 1)
				horizontalAxis = Input.GetAxis("Horizontal Axis PC 1");
			else if(horizontalAxis == 0.0f && this.Record.SpaceshipID == 2)
				horizontalAxis = Input.GetAxis("Horizontal Axis PC 2");

			if(horizontalAxis != 0.0f)
			{
				float angle = this.Model.transform.localRotation.eulerAngles.z;

				if(angle > 180.0f)
					angle -= 360.0f;

				if(angle < 45.0f && horizontalAxis < 0.0f)
					this.Model.transform.RotateAround(this.Model.transform.position, this.Model.transform.forward, -horizontalAxis * 0.50f);

				if(angle > -45.0f && horizontalAxis > 0.0f)
					this.Model.transform.RotateAround(this.Model.transform.position, this.Model.transform.forward, -horizontalAxis * 0.50f);

				this.Acceleration *= 0.75f;

				horizontalAxis *= Mathf.Sign(this.Acceleration);

				// Increment the spaceships angular velocity
				this.Rigidbody.AddTorque(this.transform.up * horizontalAxis * Handling, ForceMode.Acceleration);
			}

			// Increment the spaceships velocity
			this.Rigidbody.AddForce(this.transform.forward * Acceleration * 50.0f, ForceMode.Acceleration);
		}
		#endregion

		#region [Methods] Damage
		/// <summary>
		/// Inflicts damage to the spaceship.
		/// </summary>
		/// <param name="damage">The damage.</param>
		public void InflictDamage(float damage)
		{
			// Update the health
			this.Health = Mathf.Clamp(this.Health - damage, 0.0f, this.MaximumHealth);

			// Decrease the acceleration
			this.Acceleration *= 0.75f;

			// Start the repairs if the health ran out
			if (this.Health == 0.0f && this.RepairTimer.Running == false)
			{
				this.RepairTimer.Start();

				// Instantiate the explosion and make it follow the target
				GameObject explosion = ResourceManager.LoadGameObject("Objects/Explosions/Big Explosion");
				explosion.transform.parent = this.transform;
				explosion.transform.position = this.transform.position;
			}
		}
		#endregion

		#region [Methods] StarDust
		/// <summary>
		/// Adds the star-dust to the spaceship.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		public bool AddStarDust(int value)
		{
			this.StarDust += value;

			return true;
		}
		/// <summary>
		/// Removes the star-dust from the spaceship.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		public bool RemoveStarDust(int value)
		{
			this.StarDust -= value;

			return true;
		}
		#endregion

		#region [Methods] PowerUps
		/// <summary>
		/// Adds the power-up to the spaceship.
		/// </summary>
		/// 
		/// <param name="powerUp">The power up.</param>
		public bool AddPowerUp(IPowerUp powerUp)
		{
			// If the spaceship already has this power-up, don't add it.
			if(this.PowerUpList.Exists(existingPowerUp => existingPowerUp.Name == powerUp.Name) ||
			   this.AddedPowerUpList.Exists(existingPowerUp => existingPowerUp.Name == powerUp.Name))
				return false;

			// Add the power-up
			this.AddedPowerUpList.Add(powerUp);

			return true;
		}
		/// <summary>
		/// Removes the power-up from the spaceship.
		/// </summary>
		/// 
		/// <param name="powerUp">The power up.</param>
		public bool RemovePowerUp(IPowerUp powerUp)
		{
			// If the spaceship doesn't have this power-up, don't remove it.
			if(this.PowerUpList.Exists(existingPowerUp => existingPowerUp.Name == powerUp.Name) == false &&
			   this.RemovedPowerUpList.Exists(existingPowerUp => existingPowerUp.Name == powerUp.Name) == false)
				return false;

			// Remove the power-up
			this.RemovedPowerUpList.Add(powerUp);

			return true;
		}
		#endregion

		#region [Methods] Collision
		/// <summary>
		/// Called when there is a collision with the spaceship.
		/// </summary>
		/// 
		/// <param name="collision">The collision information.</param>
		public void OnCollisionEnter(Collision collision)
		{
			// Check if we're colliding with a boundary
			if(collision.collider.transform.tag == LayerManager.GetTagName(Tag.Boundary))
			{
				// Reduce the acceleration
				this.Acceleration *= 0.5f;

				foreach(ContactPoint contactPoint in collision.contacts)
				{
					RaycastHit centerHit;

					// Cast a ray from the saceships center heading towards the track
					if(Physics.Raycast(this.transform.position + this.transform.up * 5.0f, -this.transform.up, out centerHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks)))
					{
						// If there is a collision, adjust the Spaceships Position
						if(centerHit.collider.tag == LayerManager.GetTagName(Tag.Road))
						{
							Vector3 contactNormal = contactPoint.point - centerHit.point;
							contactNormal.Normalize();
							Vector3 contactDirection = this.transform.forward - Vector3.Project(this.transform.forward, contactNormal);
							contactDirection.Normalize();

							// Adjust the velocity to redirect the spaceship from the collider
							this.Rigidbody.velocity = -this.Rigidbody.velocity.magnitude * contactDirection * 0.25f;
							// Adjust the position to remove the spaceship from the collider
							this.transform.position -= contactDirection;
						}
					}
				}
			}
		}
		#endregion

		#endregion

		public void OnDrawGizmos()
		{
			if(BackLeftCorner != null && BackRightCorner != null && FrontLeftCorner != null && FrontRightCorner != null)
			{
				// Spaceship Rotation Ajustments
				RaycastHit backLeftHit;
				RaycastHit backRightHit;
				RaycastHit frontLeftHit;
				RaycastHit frontRightHit;

				// Cast Rays from each of the Spaceships Corners heading towards the Track
				bool backLeftRay = Physics.Raycast(BackLeftCorner.position + this.transform.up * 5.0f, -this.transform.up,
					out backLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
				bool backRightRay = Physics.Raycast(BackRightCorner.position + this.transform.up * 5.0f, -this.transform.up,
					out backRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
				bool frontLeftRay = Physics.Raycast(FrontLeftCorner.position + this.transform.up * 5.0f, -this.transform.up,
					out frontLeftHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));
				bool frontRightRay = Physics.Raycast(FrontRightCorner.position + this.transform.up * 5.0f, -this.transform.up,
					out frontRightHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks"));

				if(backLeftRay && backRightRay && frontLeftRay && frontRightRay)
				{
					// If there is a Collision, adjust the Spaceships Rotation
					if(backLeftHit.collider.tag == "Road" && backRightHit.collider.tag == "Road" &&
						frontLeftHit.collider.tag == "Road" && frontRightHit.collider.tag == "Road")
					{
						this.SpaceshipUp =
							Vector3.Cross(backRightHit.point - backRightHit.normal, backLeftHit.point - backLeftHit.normal) +
							Vector3.Cross(backLeftHit.point - backLeftHit.normal, frontLeftHit.point - frontLeftHit.normal) +
							Vector3.Cross(frontLeftHit.point - frontLeftHit.normal, frontRightHit.point - frontRightHit.normal) +
							Vector3.Cross(frontRightHit.point - frontRightHit.normal, backRightHit.point - backRightHit.normal);
						this.SpaceshipUp.Normalize();

						this.SpaceshipRight = this.transform.right;
						this.SpaceshipRight.Normalize();

						this.SpaceshipForward = Vector3.Cross(SpaceshipRight, SpaceshipUp);
						this.SpaceshipForward.Normalize();
					}
				}

				if(backLeftRay && backRightRay && frontLeftRay && frontRightRay)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawLine(BackLeftCorner.position, backLeftHit.point);
					Gizmos.DrawLine(BackRightCorner.position, backRightHit.point);
					Gizmos.DrawLine(FrontLeftCorner.position, frontLeftHit.point);
					Gizmos.DrawLine(FrontRightCorner.position, frontRightHit.point);

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
	}
}