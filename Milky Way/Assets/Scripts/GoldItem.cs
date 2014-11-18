using UnityEngine;
using System.Collections;

public class GoldItem: Item {

	public int value;
	
	// When the game starts
	public override void Awake() {
	}
	
	// Update is called once per frame
	public override void Update () {
	}
	
	public override bool AddItem(Spaceship spaceship) {

		if(spaceship != null) {

			spaceship.AddGold(value);

			return true;
		}

		return false;
	}
}
