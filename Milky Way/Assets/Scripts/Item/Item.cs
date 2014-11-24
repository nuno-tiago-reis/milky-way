using UnityEngine;

public abstract class Item : MonoBehaviour {

	public string itemName
	{ get; protected set; }

	public float rotationSpeed;

	// When the game starts
	public virtual void Awake() {

		this.itemName = "Uninitialized Item";
	}
	
	// Update is called once per frame
	public virtual void Update () {
	}

	public virtual void FixedUpdate() {
		
		Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
		
		eulerAngles.y += rotationSpeed;
		
		this.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
	}

	public void OnTriggerEnter(Collider collider) {

		if(collider.transform.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {

			SpaceshipController spaceship = collider.transform.GetComponent<SpaceshipController>();

			if(AddItem(spaceship) == true)
				Destroy(gameObject);
		}
	}

	public abstract bool AddItem(SpaceshipController spaceship);
}
