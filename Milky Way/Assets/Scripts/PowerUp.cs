using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {

	public string powerUpName;

	public Vector3 initialPosition
	{ get; protected set; }

	// When the game starts
	public virtual void Awake() {
	
		this.initialPosition = this.transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}
	
	public abstract void OnPickUp(GenericCharacter character);
}
