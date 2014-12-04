using UnityEngine;

public class SmokescreenPowerUp : PowerUp {

	public float radius
	{ get; protected set; }
	
	public override void Awake() {
		
		// Initialize the Abilitys Name.
		this.powerUpName = "Smokescreen";
		// Initialize the Abilitys Lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 30.0f;
		
		// Initialize the Shields Health.
		this.radius = 50.0f;
	}
	
	public override void Activate() {
		
		Debug.Log("Smokescreen - Activate(" + this.transform.parent.name + ")");

		// Instantiate the Smokescreens GameObject
		GameObject smokescreen = GameObject.Instantiate(Resources.Load("Prefabs/PowerUps/" + this.powerUpName)) as GameObject;
		// Set the Parent transform to null
		smokescreen.transform.parent = null;
		// Set the Rotation so that it matches the Spaceships.
		smokescreen.transform.rotation = this.transform.parent.rotation;
		// Set the Position  so that it matches the Spaceships Shooter Position.
		smokescreen.transform.position = this.transform.parent.position;

		// Initialize the Shields Controller
		SmokescreenPowerUpController smokescreenController = smokescreen.GetComponent<SmokescreenPowerUpController>();
		smokescreenController.parent = this.transform.parent;
		smokescreenController.radius = this.radius;
		smokescreenController.lifetime = this.lifetime;

		Destroy(this);
	}
}
