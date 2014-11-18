using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Spaceship : MonoBehaviour {

	/* Spaceships Health */ 
	public float health
	{ get; protected set; }
	public float maximumHealth
	{ get; protected set; }
	public float minimumHealth
	{ get; protected set; }

	/* Spaceships Gold */ 
	public int gold
	{ get; protected set; }
	
	/* Spaceships Ability Map */
	public Dictionary<string, Ability> abilityMap;

	// Use this for initialization
	public void Start () {
	
		/* Players starting Health */
		this.health = 100.0f;
		this.maximumHealth = 100.0f;
		this.minimumHealth = 0.0f;
		
		this.abilityMap = new Dictionary<string, Ability>();
	}
	
	// Update is called once per frame
	public void Update () {
	}

	public bool AddGold(int gold) {

		this.gold += gold;

		Debug.Log("Gained " + gold + " gold!");

		return true;
	}
	
	public bool AddAbility(Ability ability) {

		this.abilityMap.Add(ability.abilityName, ability);

		Debug.Log("Gained the ability " + ability.abilityName + "!");

		return true;
	}
}
