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
		this.force = 17500.0f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate() {

		base.FixedUpdate();

		// Spaceships Position Adjustments
		RaycastHit centerHit;
		
		// Cast a Ray from he Spaceships Center heading towards the Track
		if(Physics.Raycast(this.transform.position + this.transform.up * 5.0f, -this.transform.up, out centerHit, 25.0f, 1 << LayerMask.NameToLayer("Tracks")) == true) {
			
			// If there is a Collision, adjust the Spaceships Position
			if(centerHit.collider.tag == "Road") {
				
				// Adjust the Spaceships Position so that it's slightly above the Track.
				this.transform.position = centerHit.point + this.transform.up * 4.25f;
			}
		}
	}
	
	public override void Activate() {
		
		// Apply the Initial Force
		Rigidbody rigidBody = this.transform.GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidBody.AddForce(this.transform.forward * this.force);
	}
}
