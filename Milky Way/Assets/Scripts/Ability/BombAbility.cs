using UnityEngine;
using System.Collections;

public class BombAbility : Ability {
	
	// When the game starts
	public override void Awake() {
		
		this.abilityName = "Bomb";
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override void Activate(Spaceship spaceship) {
	}
}
