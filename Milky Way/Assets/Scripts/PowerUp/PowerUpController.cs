using UnityEngine;

public class PowerUpController : MonoBehaviour {

	public Transform parent
	{ get; set; }

	public float lifetime
	{ get; set; }
	
	// When the game starts
	public virtual void Awake() {
		
		// Initialize the Abilities parent.
		this.parent = null;

		// Initialize the Abilities lifetime - It will be destroyed when the lifetime ends.
		this.lifetime = 0.0f;
	}
	
	// Update is called once per frame
	public virtual void FixedUpdate() {
		
		this.lifetime -= Time.deltaTime;
		
		// If the lifetime ends.
		if(this.lifetime < 0.0f)
			Destroy(this.gameObject);
	}
}
