using UnityEngine;

public class ShieldPowerUp : PowerUp {

	public float health
	{ get; protected set; }

	public override void Awake() {

		// Initialize the Abilitys Name.
		this.powerUpName = "Shield";
		// Initialize the Abilitys Lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 15.0f;
		
		// Initialize the Shields Health.
		this.health = 500.0f;
	}

	public override void Activate() {
		
		Debug.Log("Shield - Activate(" + this.transform.parent.name + ")");

		// Instantiate the Shields GameObject
		GameObject shield = GameObject.Instantiate(Resources.Load("Prefabs/PowerUps/" + this.powerUpName)) as GameObject;
		// Set the Parent transform to null
		shield.transform.parent = this.transform;
		// Set the Rotation so that it matches the Spaceships.
		shield.transform.rotation = this.transform.rotation;
		// Set the Position  so that it matches the Spaceships Shooter Position.
		shield.transform.position = this.transform.position;

		// Initialize the Shields Controller
		ShieldPowerUpController shieldController = shield.GetComponent<ShieldPowerUpController>();
		shieldController.parent = this.transform;
		shieldController.health = this.health;
		shieldController.lifetime = this.lifetime;

		Destroy(this);
	}
}
