using UnityEngine;

public class TrailController : MonoBehaviour {
	
	public Transform spaceship
	{ get; private set; }
	
	// Use this for initialization
	public void Start () {
		
		this.spaceship = this.transform.parent.parent;
	}
	
	// Update is called once per frame
	public void Update () {
		
		float velocity = this.spaceship.GetComponent<Rigidbody>().velocity.magnitude;

		this.GetComponent<ParticleSystem>().startSpeed = (velocity / 100.0f) * 10.0f;
	}
}
