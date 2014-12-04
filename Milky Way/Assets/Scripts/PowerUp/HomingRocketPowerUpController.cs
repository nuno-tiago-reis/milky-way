using UnityEngine;

public class HomingRocketPowerUpController : ProjectileController {

	public Transform target
	{ get; set; }
	
	// When the game starts
	public override void Awake() {
		
		// Initialize the Lasers damage.
		this.damage = 50.0f;
		// Initialize the Lasers lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 10.0f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate() {
		
		base.FixedUpdate();
	}
	
	public override void Activate() {
	}
}
