using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

	// Defines the PowerUps Name.
	public string powerUpName
	{ get; protected set;}
    // Define the PowerUps Key
    public string powerUpKey
    { get; protected set; }
	// Defines the PowerUps Controller.
	public PowerUpController powerUpController
	{ get; protected set;}

	// When the PowerUp is Created
	public virtual void Awake() {
		
		// Initialize the PowerUps Name.
		this.powerUpName = "Generic PowerUp";
	}

	// FixedUpdate is called once per fixed frame
	public virtual bool Activate() {

		if(this.powerUpController != null)
			return false;
			
		return true;
	}
}
