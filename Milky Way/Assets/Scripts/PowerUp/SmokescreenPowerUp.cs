using UnityEngine;

public class SmokescreenPowerUp : PowerUp {

	// Defines the SmokescreenPowerUp Radius.
	public const float smokescreenRadius = 50.0f;
	// Defines the ShieldPowerUp Lifetime.
	public const float smokescreenLifetime = 15.0f;

	// When the SmokescreenPowerUp is Created
	public override void Awake() {
		
		// Initialize the Abilitys Name.
		this.powerUpName = "Smokescreen";

        // Initilize the Abilitys Key
        this.powerUpKey = "R2";
	}
	
	public override bool Activate() {

		if(base.Activate() == false)
			return false;
		
		Debug.Log("Smokescreen - Activate(" + this.transform.parent.name + ")");

		// Instantiate the Smokescreens GameObject
		GameObject smokescreen = GameObject.Instantiate(Resources.Load("Prefabs/PowerUps/" + this.powerUpName)) as GameObject;
		// Set the Parent transform to null so that it doesn't follow the parent
		smokescreen.transform.parent = null;
		// Set the Position and Rotation so that it matches the Spaceships Rotation and Position.
		smokescreen.transform.rotation = this.transform.rotation;
		smokescreen.transform.position = this.transform.position;

		// Initialize the Shields Controller
		SmokescreenPowerUpController smokescreenController = smokescreen.GetComponent<SmokescreenPowerUpController>();
		// Set the PowerUps name.
		smokescreenController.powerUpName = this.powerUpName;
		// Store a reference to the Parent.
		smokescreenController.parent = this.transform;
		// Set the Health and Lifetime according to the PowerUps Constants.
		smokescreenController.radius = SmokescreenPowerUp.smokescreenRadius;
		smokescreenController.lifetime = SmokescreenPowerUp.smokescreenLifetime;

		// Store a Reference to the PowerUp Controller.
		this.powerUpController = smokescreenController;

		return true;
	}
}
