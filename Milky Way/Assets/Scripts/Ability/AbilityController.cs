using UnityEngine;

public abstract class AbilityController : MonoBehaviour {
	
	public string abilityName
	{ get; protected set;}
	
	// When the game starts
	public virtual void Awake() {
		
		this.abilityName = "Uninitialized";
	}
	
	public abstract void Activate(Transform spaceshipTransform);

	public abstract Texture2D getTexture();
}
