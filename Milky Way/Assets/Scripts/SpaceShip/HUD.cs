using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {
	
	public Transform spaceship
	{ get; private set; }

	// HUD Screen offset
	public int screenID
	{ get; private set; }
	public Vector2 screenOffset
	{ get; private set; }
	public Vector2 speedometerOffset
	{ get; private set; }

	// Standings Attributes
	public Vector2 standingPosition
	{ get; private set; }

	public float standingWidth
	{ get; private set; }
	public float standingHeight
	{ get; private set; }

	public Texture2D standingsTexture
	{ get; private set; }

	// Current Lap Attributes
	public Vector2 lapPosition
	{ get; private set; }

	public float lapWidth
	{ get; private set; }
	public float lapHeight
	{ get; private set; }

	public Texture2D lapTexture
	{ get; private set; }

	// Speedometer Attributes
	public Vector2 speedometerPosition
	{ get; private set; }

	public float speedometerWidth
	{ get; private set; }
	public float speedometerHeight
	{ get; private set; }

	public Texture2D speedometerTexture
	{ get; private set; }

	// Pointer Attributes
	public float pointerWidth
	{ get; private set; }
	public float pointerHeight
	{ get; private set; }
	public Vector2 pointerStart
	{ get; private set; }
	public Vector2 pointerFinish
	{ get; private set; }

	public Texture2D pointerTexture
	{ get; private set; }

	// Ability Attributes
	public Vector2 abilityListPosition
	{ get; private set; }

	public float abilityWidth
	{ get; private set; }
	public float abilityHeight
	{ get; private set; }

	// Use this for initialization
	public void Start () {

		// HUDs reference to its Spaceship
		this.spaceship = this.transform.parent.FindChild("Spaceship").transform;

		// Set the ScreensID according to the Spaceships ID
		SpaceshipController spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		this.screenID = spaceshipController.id - 1;

		// Standings Attributes
		this.standingsTexture = (Texture2D)Resources.Load("Textures/HUD/1stPlace",typeof(Texture2D)) as Texture2D;
		// Current Lap Attributes
		this.lapTexture = (Texture2D)Resources.Load("Textures/HUD/1Lap",typeof(Texture2D)) as Texture2D;

		// Speedometer Attributes
		this.pointerTexture = (Texture2D)Resources.Load("Textures/HUD/Pointer",typeof(Texture2D)) as Texture2D;
		this.speedometerTexture = (Texture2D)Resources.Load("Textures/HUD/Speedometer",typeof(Texture2D)) as Texture2D;
	}
	
	// Update is called once per frame
	public void Update () {
			
		if(this.screenID != -1) {
			this.screenOffset = new Vector2(Screen.width * (0.5f * (float)this.screenID), 0.0f);
			this.speedometerOffset = new Vector2(Screen.width * (0.5f * (float)(this.screenID - 1)), 0.0f);
		}

		// Update the Standings Position and Dimensions according to the Screens Resolution
		this.standingPosition = new Vector2(Screen.width * 0.01f, Screen.height * 0.01f) + screenOffset;

		this.standingWidth = Screen.height * 0.15f;
		this.standingHeight = Screen.height * 0.05f;

		// Update the Laps Position and Dimensions according to the Screens Resolution
		this.lapPosition = new Vector2(Screen.width * 0.01f, Screen.height * 0.01f + this.standingHeight * 1.1f) + screenOffset;

		this.lapWidth = Screen.height * 0.15f;
		this.lapHeight = Screen.height * 0.05f;

		// Update the Speedometers size according to the Screens Resolution
		this.speedometerPosition = new Vector2(Screen.width - this.speedometerWidth, Screen.height - this.speedometerHeight) + speedometerOffset;

		this.speedometerWidth = Screen.height * 0.35f * 1.5f;
		this.speedometerHeight = Screen.height * 0.35f;

		// Update the Pointers size according to the Screens Resolution
		this.pointerWidth = 10.0f;
		this.pointerHeight = speedometerWidth * 0.35f;

		// Update the Pointers position according to the Screens Resolution and to the Spaceships Velocity
		float pointerAngle = (Mathf.Abs(this.spaceship.rigidbody.velocity.magnitude) / 150.0f  + 0.75f) * Mathf.PI;

		Vector2 pointerDistance = new Vector3(
			this.pointerHeight * Mathf.Cos (pointerAngle) - this.pointerHeight * Mathf.Sin (pointerAngle),
			this.pointerHeight * Mathf.Sin (pointerAngle) + this.pointerHeight * Mathf.Cos (pointerAngle));

		this.pointerStart = new Vector2(Screen.width - speedometerWidth * 0.5f, Screen.height - speedometerHeight * 0.15f) + speedometerOffset;
		this.pointerFinish = this.pointerStart + pointerDistance;

		// Update the Abilities size according to the Screens Resolution
		this.abilityListPosition = new Vector2(Screen.width * 0.01f, Screen.height - this.abilityHeight) + screenOffset;

		this.abilityWidth = Screen.height * 0.10f;
		this.abilityHeight = Screen.height * 0.10f;
	}
			
	public void OnGUI() {

		// Draw the Standings
		GUI.DrawTexture(new Rect(this.standingPosition.x, this.standingPosition.y, this.standingWidth, this.standingHeight), this.standingsTexture);
		// Draw the Laps
		GUI.DrawTexture(new Rect(this.lapPosition.x, this.lapPosition.y, this.lapWidth, this.lapHeight), this.lapTexture);

		// Draw the Speedometer
		GUI.DrawTexture(new Rect(this.speedometerPosition.x, this.speedometerPosition.y, this.speedometerWidth, this.speedometerHeight), this.speedometerTexture);
		// Draw the Pointer
		Vector2 distance = this.pointerFinish - this.pointerStart;
		float a = Mathf.Rad2Deg * Mathf.Atan(distance.y / distance.x);

		if (distance.x < 0)
			a += 180;
		
		int width = (int)Mathf.Ceil(this.pointerWidth / 2);
		
		GUIUtility.RotateAroundPivot(a, this.pointerStart);
			GUI.DrawTexture(new Rect(this.pointerStart.x, this.pointerStart.y - width, this.pointerHeight, width), this.pointerTexture);
		GUIUtility.RotateAroundPivot(-a, this.pointerStart);

		// Draw the Abilities
		float abilityOffset = 0.0f;

		SpaceshipController spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		if(spaceshipController != null) {

			foreach(KeyValuePair<string, GameObject> abilityEntry in spaceshipController.abilityInventory) { 
			
				AbilityController abilityController = abilityEntry.Value.GetComponent<AbilityController>();

				if(abilityController != null) {

					Vector2 abilityPosition = this.abilityListPosition + new Vector2(abilityOffset, 0.0f);

					GUI.DrawTexture (new Rect(abilityPosition.x, abilityPosition.y, this.abilityWidth, this.abilityHeight), abilityController.getTexture());

					abilityOffset += abilityWidth * 1.1f;
				}
			}
		}
	}
}
