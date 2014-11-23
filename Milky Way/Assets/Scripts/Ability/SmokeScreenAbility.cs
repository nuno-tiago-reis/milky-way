using UnityEngine;
using System.Collections;

public class SmokeScreenAbility : Ability {

	public static Texture2D abilityTexture
	{ get; set; }
	
	// When the game starts
	public override void Awake() {
		
		this.abilityName = "SmokeScreen";

		abilityTexture = (Texture2D)Resources.Load ("Textures/HUD/SmokeScreen", typeof(Texture2D)) as Texture2D;
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override void Activate(Spaceship spaceship) {

	}

	public override Texture2D getTexture() {
		
		return abilityTexture;
	}
}
