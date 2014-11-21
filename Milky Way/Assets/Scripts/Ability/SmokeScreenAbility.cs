using UnityEngine;
using System.Collections;

public class SmokeScreenAbility : Ability {
	
	// When the game starts
	public override void Awake() {
		
		this.abilityName = "SmokeScreen";
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override void Activate(Spaceship spaceship) {
	}
}
