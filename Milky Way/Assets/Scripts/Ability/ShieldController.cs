using UnityEngine;

public class ShieldController : AbilityController {
	
	public float lifetime
	{ get; protected set; }

	public static Texture2D abilityTexture
	{ get; set; }

	// When the game starts
	public override void Awake() {

		// Initialize the Shields lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 15.0f;

		// Load the HUD texture if it hasn't been loaded yet
		if(ShieldController.abilityTexture == null)
			ShieldController.abilityTexture = (Texture2D)Resources.Load("Textures/HUD/Shield", typeof(Texture2D)) as Texture2D;

		this.abilityName = "Shield";
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

	public void FixedUpdate() {

		Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
		
		eulerAngles.y += 5.0f;
		
		this.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
	}
	
	public override void Activate(Transform spaceshipTransform) {

		Debug.Log("Shield activated");

		this.gameObject.SetActive(true);
	}

	public void OnTriggerEnter(Collider collider) {

		if(this.gameObject.activeSelf == false)
			return;

		// Collision with other Abilities
		if(collider.gameObject.layer == LayerMask.NameToLayer("Abilities")) {
			
			//Debug.Log("Ability Collision - Blocked Damage");
			
			return;
		}

		// Collision with Spaceships
		if(collider.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {
			
			//Debug.Log("Spaceship Collision - Blocked Nothing");
				
			return;
		}
	}

	public override Texture2D getTexture() {
		
		return abilityTexture;
	}
}
