using UnityEngine;

public class AbilityItem : Item {

	public string abilityName;
	
	// When the game starts
	public override void Awake() {
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override bool AddItem(SpaceshipController spaceship) {
		
		if(spaceship != null)
			return spaceship.AddAbility(abilityName);

		return false;
	}
}
