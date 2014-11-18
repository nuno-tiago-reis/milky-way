using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GenericCharacter : MonoBehaviour {

	/* CharacterController Health Attributes */ 
	public float health
	{ get; protected set; }
	public float maximumHealth
	{ get; protected set; }
	public float minimumHealth
	{ get; protected set; }
	
	bool isAlive;
	
	/* Power Up Map */
	public Dictionary<string, PowerUp> powerUpMap;

	// Use this for initialization
	void Start () {
	
		/* Players starting Health */
		this.health = 100.0f;
		this.maximumHealth = 100.0f;
		this.minimumHealth = 0.0f;
		
		powerUpMap = new Dictionary<string, PowerUp>();
		
		isAlive = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		/* Check HP */
		if(health == minimumHealth)
			isAlive = false;
			
		/* Check if got any Power Ups */
	
	}
	
	public bool ReceivePowerUp(PowerUp power) {
	
	
		return true;
	}
}
