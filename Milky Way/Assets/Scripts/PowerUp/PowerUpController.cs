using UnityEngine;

public class PowerUpController : MonoBehaviour {

	// Defines the PowerUpControllers parent.
	public Transform parent
	{ get; set; }

	// Defines the PowerUpControllers Name.
	public string powerUpName
	{ get; set; }
	// Defines the PowerUpControllers lifetime, so that when its lifetime ends it is destroyed.
	public float lifetime
	{ get; set; }
	
	// When the PowerUpController is Created
	public virtual void Awake() {

		// Initialize the PowerUpControllers Parent.
		this.parent = null;
		// Initialize the PowerUpControllers Name.
		this.powerUpName = "Generic PowerUp";
		// Initialize the PowerUpControllers Lifetime.
		this.lifetime = float.MinValue;
	}
	
	// FixedUpdate is called once per fixed frame
	public virtual void FixedUpdate() {
		
		this.lifetime -= Time.deltaTime;
		
		// If the lifetime ends.
		if(this.lifetime < 0.0f) {

			SpaceshipController spaceshipController = this.parent.GetComponent<SpaceshipController>();
			spaceshipController.RemovePowerUp(this.powerUpName);

			Destroy(this.gameObject);
		}
	}
}