using UnityEngine;

public class HomingRocketPowerUpController : PowerUpController {

	// Defines the HomingRocketPowerUpControllers Target.
	public Transform target
	{ get; set; }

	// Defines the HomingRocketPowerUpControllers Damage.
	public float damage
	{ get; set; }
	// Defines the HomingRocketPowerUpControllers Force.
	public float force
	{ get; set; }
	
	// When the HomingRocketPowerUpController is Created
	public override void Awake() {
	}
	
	// FixedUpdate is called once per fixed frame
	public override void FixedUpdate() {
		
		base.FixedUpdate();

		// Rotate the HomingRocket towards the target.
		this.transform.LookAt(this.target, Vector3.up);

		// Increase the Push Force
		this.force = this.force + 0.025f;

		Vector3 direction = this.target.position - this.transform.position;
		direction.Normalize();

		this.transform.Translate(direction * this.force, Space.World);
	}

	public void FindTarget() {

		// Search for the nearest Spaceship.
		float nearestSpaceshipDistance = float.MaxValue;
		GameObject nearestSpaceship = null;
		
		GameObject[] spaceshipList = GameObject.FindGameObjectsWithTag("Spaceship");
		
		foreach(GameObject spaceship in spaceshipList) {
			
			float distance = Vector3.Distance(spaceship.transform.position, this.transform.position);
			
			if(distance < nearestSpaceshipDistance && this.parent != spaceship.transform) {
				
				nearestSpaceship = spaceship;
				nearestSpaceshipDistance = distance;
			}
		}
		
		// Initialize the HomingRocketPowerUpControllers target.
		this.target = nearestSpaceship.transform;
	}

	public virtual void OnTriggerEnter(Collider collider) {
		
		// Collision with other Abilities
		if(collider.gameObject.layer == LayerMask.NameToLayer("PowerUps")) {
			
			// Collision with the Shield PowerUp
			if(collider.gameObject.tag == "Shield") {

				// Remove the PowerUp from the Parent
				SpaceshipController parentSpaceshipController = this.parent.GetComponent<SpaceshipController>();
				parentSpaceshipController.RemovePowerUp(this.powerUpName);

				// Instantiate the HomingRockets GameObject
				GameObject explosion = GameObject.Instantiate(Resources.Load("Prefabs/Explosions/Green Explosion")) as GameObject;
				// Set the Parent transform to null so that it doesn't follow the parent.
				explosion.transform.parent = collider.transform;
				// Set the Position so that it matches the Contact Point Position.
				explosion.transform.position = collider.transform.position;

				Destroy(this.gameObject);
				
				return;
			}
		}
		
		// Collision with other Spaceships
		if(collider.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {
			
			// Collision with hostile Spaceships
			if(collider.transform != this.parent) {
				
				Debug.Log("Spaceship Collision - Damage to " + collider.transform.name + "!");

				// Inflict Damage to the targeted Spaceship
				SpaceshipController colliderSpaceshipController = collider.transform.GetComponent<SpaceshipController>();
				colliderSpaceshipController.InflictDamage(this.damage);

				// Remove the PowerUp from the Parent
				SpaceshipController parentSpaceshipController = this.parent.GetComponent<SpaceshipController>();
				parentSpaceshipController.RemovePowerUp(this.powerUpName);

				// Instantiate the HomingRockets GameObject
				GameObject explosion = GameObject.Instantiate(Resources.Load("Prefabs/Explosions/Red Explosion")) as GameObject;
				// Set the Parent transform to null so that it doesn't follow the parent.
				explosion.transform.parent = collider.transform;
				// Set the Position so that it matches the Contact Point Position.
				explosion.transform.position = collider.transform.position;

				Destroy(this.gameObject);
				
				return;
			}
		}
	}
}
