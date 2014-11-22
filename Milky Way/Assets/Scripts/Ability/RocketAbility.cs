using UnityEngine;
using System.Collections;

public class RocketAbility : Ability {

	public static Texture2D abilityTexture
	{ get; set; }

	public Rigidbody rocket;

	// When the game starts
	public override void Awake() {
		
		this.abilityName = "Rocket";
		
		abilityTexture = (Texture2D)Resources.Load ("Textures/HUD/Rocket", typeof(Texture2D)) as Texture2D;

		rocket = null;
	}
	
	// Update is called once per frame
	public override void Update () {

	}
	
	public override void Activate(Spaceship spaceship) {

		Debug.Log ("Rocket launched");

		/*Rigidbody rocketBody = (Rigidbody)Instantiate (rocket, transform.position, transform.rotation);
		rocketBody.velocity = transform.forward * 10.0f;
*/

		/*GameObject rocket = GameObject.Instantiate(Resources.Load("Prefabs/Rocket"), transform.position, transform.rotation) as GameObject;
		rocket.transform.position = this.transform.position + transform.forward * 1.0f;
		
		Debug.Log("this.transform.position = " + this.transform.position);
		Debug.Log("this.transform.parent.transform.position = " + this.transform.parent.transform.position);

		rocket.rigidbody.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * 2.0f);*/
	}

	public override Texture2D getTexture() {

		return abilityTexture;
	}
}
