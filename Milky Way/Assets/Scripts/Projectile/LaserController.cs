using UnityEngine;

public class LaserController : ProjectileController {
	
	public float force
	{ get; set; }
	
	// When the game starts
	public override void Awake() {
		
		// Initialize the Lasers damage.
		this.damage = 5.0f;
		// Initialize the Lasers lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 2.5f;
		// Initialize the Lasers Force - Bigger force means faster laser
		this.force = 15000.0f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate() {

		base.FixedUpdate();
	}
	
	public override void Activate() {
		
		// Apply the Initial Force
		Rigidbody rigidBody = this.transform.GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidBody.AddForce(this.transform.forward * this.force);
	}
}
