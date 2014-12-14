using UnityEngine;

public class ShieldPowerUp : PowerUp {

	// Defines the ShieldPowerUp Health.
	public const float shieldHealth = 500.0f;
	// Defines the ShieldPowerUp Lifetime.
	public const float shieldLifetime = 15.0f;

	// When the ShieldPowerUp is Created
	public override void Awake() {

		// Initialize the Abilitys Name.
		this.powerUpName = "Shield";

        // Initilize the Abilitys Key
        this.powerUpKey = "R1";
	}

	public override bool Activate() {

		if(base.Activate() == false)
			return false;
		
		Debug.Log("Shield - Activate(" + this.transform.parent.name + ")");

		// Instantiate the Shields GameObject
		GameObject shield = GameObject.Instantiate(Resources.Load("Prefabs/PowerUps/" + this.powerUpName)) as GameObject;
		// Set the Parent transform so that it follows the parent
		shield.transform.parent = this.transform;
		// Set the Position and Rotation so that it matches the Spaceships Rotation and Position.
		shield.transform.rotation = this.transform.rotation;
		shield.transform.position = this.transform.position;

		// Initialize the Shields Controller
		ShieldPowerUpController shieldController = shield.GetComponent<ShieldPowerUpController>();
		// Set the PowerUps name.
		shieldController.powerUpName = this.powerUpName;
		// Store a reference to the Parent.
		shieldController.parent = this.transform;
		// Set the Health and Lifetime according to the PowerUps Constants.
		shieldController.health = ShieldPowerUp.shieldHealth;
		shieldController.lifetime = ShieldPowerUp.shieldLifetime;
		shieldController.maximumLifetime = ShieldPowerUp.shieldLifetime;

		// Store a Reference to the PowerUp Controller.
		this.powerUpController = shieldController;

		return true;
	}
}
