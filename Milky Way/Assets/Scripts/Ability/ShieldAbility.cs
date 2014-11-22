using UnityEngine;
using System.Collections;

public class ShieldAbility : Ability {

	public static Texture2D abilityTexture
	{ get; set; }

	// When the game starts
	public override void Awake() {
		
		this.abilityName = "Shield";

		abilityTexture = (Texture2D)Resources.Load ("Textures/HUD/Shield", typeof(Texture2D)) as Texture2D;
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
