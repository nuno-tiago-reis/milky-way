using UnityEngine;

public class SmokescreenController : AbilityController {
	
	public float lifetime
	{ get; protected set; }

	public static Texture2D abilityTexture
	{ get; set; }
	
	// When the game starts
	public override void Awake() {

		// Load the HUD texture if it hasn't been loaded yet
		if(SmokescreenController.abilityTexture == null)
			SmokescreenController.abilityTexture = (Texture2D)Resources.Load("Textures/HUD/SmokeScreen", typeof(Texture2D)) as Texture2D;
		
		this.abilityName = "Smokescreen";
	}
	
	// Update is called once per frame
	public void Update () {
	}
	
	public override void Activate(Transform spaceshipTransform) {

	}

	public override Texture2D getTexture() {
		
		return abilityTexture;
	}
}
