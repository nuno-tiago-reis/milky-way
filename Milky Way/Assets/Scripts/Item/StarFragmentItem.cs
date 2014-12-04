using UnityEngine;

public class StarFragmentItem: Item {

	public int value;

	// When the game starts
	public override void Awake() {
		
		this.itemName = "Star Fragment Item - " + value;
	}
	
	public override bool AddItem(SpaceshipController spaceship) {

		if(spaceship != null)
			return spaceship.AddGold(value);

		return false;
	}
}
