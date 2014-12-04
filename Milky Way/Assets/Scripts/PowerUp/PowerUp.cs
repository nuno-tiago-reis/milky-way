using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
	
	public string powerUpName
	{ get; protected set;}
	public float lifetime
	{ get; protected set; }

	public virtual void Awake() {
		
		// Initialize the Abilitys Name.
		this.powerUpName = "Generic PowerUp";
		// Initialize the Abilitys Lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 0.0f;
	}
	
	public abstract void Activate();
}
