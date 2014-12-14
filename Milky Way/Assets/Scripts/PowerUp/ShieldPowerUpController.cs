using UnityEngine;

public class ShieldPowerUpController : PowerUpController {

	// Defines the ShieldPowerUpControllers Health.
	public float health
	{ get; set; }

	// When the ShieldPowerUpController is Created
	public override void Awake() {
	}
	
	// Update is called once per frame
	public override void FixedUpdate() {
		
		base.FixedUpdate();

		// Calculate the Shields Rotation
		Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
		eulerAngles.y += 2.5f;

		// Rotate the Shield around the Parents Up Vector
		this.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
	}
}
