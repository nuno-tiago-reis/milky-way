using UnityEngine;
using System.Collections;

public class ShieldAbility : Ability {
	
	// When the game starts
	public override void Awake() {
		
		this.abilityName = "Shield";
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override void Activate(Spaceship spaceship) {
	}
}
