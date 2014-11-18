using UnityEngine;
using System.Collections;

public class ShieldPowerUp : PowerUp {

	// Use this for initialization
	public override void Awake () {
		
		base.Awake();
		
		powerUpName = "Shield";
	
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}
	
	public override void OnPickUp(GenericCharacter character) {
	
	}
}
