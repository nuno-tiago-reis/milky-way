using UnityEngine;

public class HomingRocketPowerUp : PowerUp {

	// Defines the HomingRocketPowerUps Damage.
	public const float homingRocketDamage = 150.0f;
	// Defines the HomingRocketPowerUps Force.
	public const float homingRocketForce = 1.0f;
	// Defines the HomingRocketPowerUps Lifetime.
	public const float homingRocketLifetime = 10.0f;
	// Defines the HomingRocketPowerUps Setup Time.
	public const float homingRocketSetupTime = 1.5f;

	// When the HomingRocketPowerUp is Created
	public override void Awake() {

		// Initialize the PowerUps Name.
		this.powerUpName = "HomingRocket";

        // Initilize the Abilitys Key
        this.powerUpKey = "L2";
	}
	
	public override bool Activate() {

		if(base.Activate() == false)
			return false;

		Debug.Log("HomingRocket - Activate(" + this.transform.parent.name + ")");

		// Instantiate the HomingRockets GameObject
		GameObject homingRocket = GameObject.Instantiate(Resources.Load("Prefabs/PowerUps/" + this.powerUpName)) as GameObject;
		// Set the Parent transform to null so that it doesn't follow the parent.
		homingRocket.transform.parent = this.transform;
		// Set the Position and Rotation so that it matches the Spaceships Rotation and Position.
		homingRocket.transform.rotation = this.transform.rotation;
		homingRocket.transform.position = this.transform.position + this.transform.up * 15.0f;
		
		// Initialize the HomingRockets Controller
		HomingRocketPowerUpController homingRocketController = homingRocket.GetComponent<HomingRocketPowerUpController>();
		// Set the PowerUps name.
		homingRocketController.powerUpName = this.powerUpName;
		// Store a reference to the Parent.
		homingRocketController.parent = this.transform;
		// Set the Damage, Force and Lifetime according to the PowerUps Constants.
		homingRocketController.damage = HomingRocketPowerUp.homingRocketDamage;
		homingRocketController.force = HomingRocketPowerUp.homingRocketForce;
		homingRocketController.lifetime = HomingRocketPowerUp.homingRocketLifetime;
		homingRocketController.maximumLifetime = HomingRocketPowerUp.homingRocketLifetime;
		homingRocketController.setupTime = HomingRocketPowerUp.homingRocketSetupTime;

		homingRocketController.FindTarget();

		// Store a Reference to the PowerUp Controller.
		this.powerUpController = homingRocketController;

		return true;
	}
}
