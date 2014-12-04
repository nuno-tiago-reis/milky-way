using UnityEngine;

public class SmokescreenPowerUpController : PowerUpController {

	public float radius
	{ get; set; }

	// When the game starts
	public override void Awake() {

		base.Awake();
		
		// Initialize the Smokescreens radius.
		this.radius = 0.0f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate() {
		
		base.FixedUpdate();
	}
}
