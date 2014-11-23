using UnityEngine;
using System.Collections;

public class AbilityItem : Item {
	
	// When the game starts
	public override void Awake() {
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override bool AddItem(Spaceship spaceship) {

		Ability ability = GetComponent<Ability>();
		
		if(spaceship != null && ability != null) {

			spaceship.AddAbility(ability);

			return true;
		}

		return false;
	}
}
