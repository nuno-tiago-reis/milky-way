using UnityEngine;

public class GoldItem: Item {

	public int value;
	
	public override bool AddItem(SpaceshipController spaceship) {

		if(spaceship != null)
			return spaceship.AddGold(value);

		return false;
	}
}
