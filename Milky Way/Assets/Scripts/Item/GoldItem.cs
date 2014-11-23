using UnityEngine;

public class GoldItem: Item {

	public int value;
	
	// When the game starts
	public override void Awake() {
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override bool AddItem(SpaceshipController spaceship) {

		if(spaceship != null)
			return spaceship.AddGold(value);

		return false;
	}
}
