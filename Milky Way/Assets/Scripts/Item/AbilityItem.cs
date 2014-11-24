using UnityEngine;

public class AbilityItem : Item {

	public string abilityName;
	
	public override bool AddItem(SpaceshipController spaceship) {
		
		if(spaceship != null)
			return spaceship.AddAbility(abilityName);

		return false;
	}
}
