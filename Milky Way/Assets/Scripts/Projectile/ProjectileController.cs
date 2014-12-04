using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public Transform parent
	{ get; set; }

	public float damage
	{ get; set; }
	public float lifetime
	{ get; set; }
	
	// When the game starts
	public virtual void Awake() {
		
		// Initialize the Projectiles damage.
		this.damage = 0.0f;
		// Initialize the Projectiles lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 0.0f;
	}
	
	// Update is called once per frame
	public virtual void FixedUpdate() {
		
		this.lifetime -= Time.deltaTime;
		
		// If the lifetime ends.
		if(this.lifetime < 0.0f)
			Destroy(this.gameObject);
	}
	
	public virtual void Activate() {
		
		Debug.Log("ProjectileController - Activate()");
	}
	
	public virtual void OnTriggerEnter(Collider collider) {

		Debug.Log("ProjectileController - OnTriggerEnter(" + collider.name +")");

		// Collision with other Abilities
		if(collider.gameObject.layer == LayerMask.NameToLayer("Abilities")) {
			
			ProjectileController abilityController = collider.transform.GetComponent<ProjectileController>();
			
			if(abilityController == null)
				return;

			// Collision with friendly Abilities
			if(abilityController.parent == this.parent) {
				
				Debug.Log("Ability Collision - Friendly Fire!");
				
				return;
			}
			
			// Collision with hostile Abilities
			if(abilityController.parent != this.parent) {
				
				Debug.Log("Ability Collision - Damage to " + collider.transform.name + "!");
				
				Destroy(this.gameObject);
				
				return;
			}
		}
			
			// Collision with other Spaceships
		if(collider.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {
				
			SpaceshipController spaceshipController = collider.transform.GetComponent<SpaceshipController>();

			// Collision with friendly Spaceships
			if(collider.transform == this.parent) {

				Debug.Log("Spaceship Collision - Friendly Fire!");

				return;
			}

			// Collision with hostile Spaceships
			if(collider.transform != this.parent) {
				
				Debug.Log("Spaceship Collision - Damage to " + collider.transform.name + "!");

				spaceshipController.InflictDamage(this.damage);

				return;
			}
		}
	}
}
