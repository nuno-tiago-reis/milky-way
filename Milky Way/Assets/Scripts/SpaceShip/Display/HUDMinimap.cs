using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class HUDMinimap : MonoBehaviour {

	// HUDs reference to the RaceManager
	public RaceManager raceManager
	{ get; protected set; }

	// HUDs reference to the Spaceship
	public Transform spaceship
	{ get; protected set; }
	// HUDs reference to the Spaceship Controller
	public SpaceshipController spaceshipController
	{ get; protected set; }

	// HUDs reference to the Spaceship List
	public List<Transform> spaceshipList
	{ get; protected set; }

	// MiniMap Attributes
	public Vector2 position
	{ get; private set; }
	
	public float width
	{ get; private set; }
	public float height
	{ get; private set; }
	
	public Texture2D mapTexture
	{ get; private set; }
	public Texture2D iconTexture
	{ get; private set; }

	// Minimaps Screen Offset
	public Vector2 screenOffset 
	{ get; private set; }
	
	public void Awake () {
	}
	
	public void Initialize() {

		/*// Initialize the Cameras reference to the RaceManager
		this.raceManager = GameObject.Find("Race Manager").transform.GetComponent<RaceManager>();

		// Initialize the Cameras reference to its Spaceship
		this.spaceship = this.transform.parent.FindChild("Spaceship");
		// Initialize the Cameras reference to its SpaceshipController
		this.spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		// Initialize the HUD Minimaps reference to the Spaceship List
		this.spaceshipList = this.raceManager.spaceshipList;

		//Initialize the HUD Minimaps reference to the Map and Icon Textures
		this.mapTexture = (Texture2D)Resources.Load("Textures/HUD/Minimap/WhiteBlue Map",typeof(Texture2D)) as Texture2D;
		this.iconTexture = (Texture2D)Resources.Load("Textures/HUD/Minimap/Red Icon",typeof(Texture2D)) as Texture2D;*/
	}
	
	public void Update() {

		Camera camera = this.transform.GetComponent<Camera>();

		// Store the Viewports Aspect Ratio
		float aspectRatio = Screen.width / Screen.height;

		// Set the Aspect Ratio back to the Minimaps Ratio
		camera.aspect = 1.0f;
		// Resize the Rendering Viewport
		camera.rect = new Rect(0.80f,1.0f - 0.20f * aspectRatio, 0.20f, 0.20f * aspectRatio);
			
		/*
		// Update the Screens Offset so that it matches the Spaceships ID
		if(this.raceManager.spaceshipTotal != 1) {
			
			this.screenOffset = new Vector2(Screen.width * (0.5f * (float)(this.spaceshipController.raceRecord.id - 2)), 0.0f);
		}
		else {
			
			this.screenOffset = new Vector2(0.0f, 0.0f);
		}
		
		// Update the Minimaps Texture Position and Dimensions according to the Screens Resolution
		this.position = new Vector2(Screen.width - this.width, 0.0f) + screenOffset;
		
		this.width = Screen.height * 0.30f;
		this.height = Screen.height * 0.30f;*/

	}

	public void OnGUI() {

		/*if(this.raceManager.mode == RaceManager.RaceMode.SingleRace || this.raceManager.mode == RaceManager.RaceMode.TimeAttack) {

			// Draw the Minimap
			GUI.DrawTexture(new Rect(this.position.x, this.position.y, this.width, this.height), this.mapTexture);

			// Draw the Spaceship Icons
			foreach(Transform spaceshipTransform in this.spaceshipList) {



				GUI.DrawTexture(new Rect(this.position.x, this.position.y, this.width * 0.10f, this.height * 0.10f), this.iconTexture);
			}
		}*/
	}
}