using UnityEngine;

public class SmokescreenController : AbilityController {
	
	public float lifetime
	{ get; protected set; }

	public static Texture2D abilityTexture
	{ get; set; }
	
	// When the game starts
	public override void Awake() {

		// Initialize the Smokescreen lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 15.0f;

		// Load the HUD texture if it hasn't been loaded yet
		if(SmokescreenController.abilityTexture == null)
			SmokescreenController.abilityTexture = (Texture2D)Resources.Load("Textures/HUD/SmokeScreen", typeof(Texture2D)) as Texture2D;
		
		this.abilityName = "Smokescreen";
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

		Debug.Log("Smokescreen activated");
		
		this.gameObject.SetActive(true);

		this.transform.parent = null;
	}

	public override Texture2D getTexture() {
		
		return abilityTexture;
	}
}
