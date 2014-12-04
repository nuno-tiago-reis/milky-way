using UnityEngine;

public class PowerUpItem: Item {

	public string powerUpName;

	// When the game starts
	public override void Awake() {
		
		this.itemName = "Power Up Item - " + powerUpName;
	}
	
	public override bool AddItem(SpaceshipController spaceship) {
		
		if(spaceship != null)
			return spaceship.AddPowerUp(powerUpName);

		return false;
	}
}
