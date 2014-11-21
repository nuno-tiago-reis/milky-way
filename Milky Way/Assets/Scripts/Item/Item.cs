using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

	public string itemName
	{ get; protected set; }

	// When the game starts
	public virtual void Awake() {

		this.itemName = "Uninitialized Item";
	}
	
	// Update is called once per frame
	public virtual void Update () {
	}

	public void OnTriggerEnter(Collider collider) {

		if(collider.transform.gameObject.layer == LayerMask.NameToLayer("Spaceships")) {

			Spaceship spaceship = collider.transform.GetComponent<Spaceship>();

			if(AddItem(spaceship) == true)
				Destroy(gameObject);
		}
	}

	public abstract bool AddItem(Spaceship spaceship);
}
