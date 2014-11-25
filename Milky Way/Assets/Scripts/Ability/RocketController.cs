using UnityEngine;

public class RocketController : AbilityController {

	public float damage
	{ get; protected set; }

	public float lifetime
	{ get; protected set; }

	public static Texture2D abilityTexture
	{ get; protected set; }

	// When the game starts
	public override void Awake() {

		// Initialize the Rockets damage.
		this.damage = 5.0f;
		// Initialize the Rockets lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 2.0f;

		// Load the HUD texture if it hasn't been loaded yet.
		if(RocketController.abilityTexture == null)
			RocketController.abilityTexture = (Texture2D)Resources.Load("Textures/HUD/Rocket", typeof(Texture2D)) as Texture2D;

		this.abilityName = "Rocket";
	}
	
	// Update is called once per frame
	public void Update () {

		if(this.gameObject.activeSelf == false)
			return;

		this.lifetime -= Time.deltaTime;

		// If the lifetime ends.
		if(this.lifetime < 0.0f)
			Destroy(this.gameObject);
	}
	
	public override void Activate(Transform spaceshipTransform) {

		Debug.Log("Rocket activated");

		this.gameObject.SetActive(true);

		Rigidbody rigidBody = this.gameObject.AddComponent<Rigidbody>();

		rigidBody.constraints =
			RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | 
			RigidbodyConstraints.FreezePositionY;

		rigidBody.AddForce(this.transform.parent.forward * 7500.0f);
	}

	public void OnTriggerEnter(Collider collider) {

		if(this.gameObject.activeSelf == false)
			return;

		// Collision with other Abilities
		if(collider.gameObject.layer == LayerMask.NameToLayer("Abilities")) {

			// Collision with friendly Abilities
			if(collider.transform.parent == this.transform.parent) {

				//Debug.Log("Ability Collision - No Damage");

				return;
			}

			// Collision with hostile Abilities
			if(collider.transform.parent != this.transform.parent) {
				
				Debug.Log("Ability Collision - No Damage");
				
				Destroy(this.gameObject);
				
				return;
			}
		}

		// Collision with other Spaceships
		if(collider.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {

			// Collision with friendly Spaceships
			if(collider.transform == this.transform.parent) {

				//Debug.Log("Spaceship Collision - No Damage (Parent Hit)");

				return;
			}

			// Collision with hostile Spaceships
			if(collider.transform != this.transform.parent) {
				
				Debug.Log("Spaceship Collision - Damage to " + collider.transform.name);
				
				//Destroy(this.gameObject);

				SpaceshipController spaceshipController = collider.transform.GetComponent<SpaceshipController>();
				spaceshipController.InflictDamage(this.damage);

				MeshRenderer meshRendereder = this.GetComponent<MeshRenderer>();
				meshRendereder.material = (Material)Resources.Load("Materials/Green Particle Bullet", typeof(Material));
				
				return;
			}
		}
	}

	public override Texture2D getTexture() {

		return abilityTexture;
	}
}
