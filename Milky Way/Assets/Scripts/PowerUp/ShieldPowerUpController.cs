using UnityEngine;

public class ShieldPowerUpController : PowerUpController {

	public float health
	{ get; set; }
	
	// When the game starts
	public override void Awake() {

		base.Awake();

		// Initialize the Shields health.
		this.health = 0.0f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate() {
		
		base.FixedUpdate();

		// Shield Animation
		Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
		eulerAngles.y += 5.0f;
		
		this.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
	}
}
