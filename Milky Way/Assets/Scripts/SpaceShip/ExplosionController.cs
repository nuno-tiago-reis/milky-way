using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Reference to the ExplosionControllers Flame
	public ParticleSystem flame
	{ get; protected set; }
	// Reference to the ExplosionControllers Smoke	
	public ParticleSystem smoke
	{ get; protected set; }

	// When the ExplosionController is Created
	public void Awake() {
		
		// Initialize the ExplosionControllers Flame.
		this.flame = this.transform.FindChild("Flame").GetComponent<ParticleSystem>();

		// Initialize the ExplosionControllers Smoke.
		this.smoke = this.transform.FindChild("Smoke").GetComponent<ParticleSystem>();
	}
	
	// FixedUpdate is called once per fixed frame
	public void FixedUpdate() {
		
		if(this.flame.isStopped == true && this.smoke.isStopped == true)
			Destroy(this.gameObject);
	}
}
