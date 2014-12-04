using UnityEngine;

public class HomingRocketPowerUp : PowerUp {

	public float damage
	{ get; protected set; }

	public override void Awake() {

		// Initialize the Abilitys Name.
		this.powerUpName = "HomingRocket";
		// Initialize the Abilitys Lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 5.0f;

		// Initialize the HomingRockets Damage.
		this.damage = 50.0f;
	}
	
	public override void Activate() {

		Debug.Log("HomingRocket - Activate(" + this.transform.parent.name + ")");

		// Instantiate the HomingRockets GameObject
		GameObject homingRocket = GameObject.Instantiate(Resources.Load("Prefabs/PowerUps/" + this.powerUpName)) as GameObject;
		// Set the Parent transform to null
		homingRocket.transform.parent = null;
		// Set the Rotation so that it matches the Spaceships.
		homingRocket.transform.rotation = this.transform.parent.rotation;
		// Set the Position  so that it matches the Spaceships Shooter Position.
		homingRocket.transform.position = this.transform.parent.position;
		
		// Initialize the HomingRockets Controller
		HomingRocketPowerUpController homingRocketController = homingRocket.GetComponent<HomingRocketPowerUpController>();
		homingRocketController.parent = this.transform.parent;
		homingRocketController.damage = this.damage;
		homingRocketController.lifetime = this.lifetime;

		Destroy(this);
	}
}
