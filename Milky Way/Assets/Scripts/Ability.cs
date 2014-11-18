using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	
	public string abilityName
	{ get; protected set;}
	
	// When the game starts
	public virtual void Awake() {
		
		this.abilityName = "Uninitialized";
	}
	
	// Update is called once per frame
	public virtual void Update () {
	}
	
	public abstract void Activate(Spaceship spaceship);
}
