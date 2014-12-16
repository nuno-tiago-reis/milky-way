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
	
	public void Awake () {
	}
	
	public void Initialize() {

		// Initialize the Cameras reference to the RaceManager
		this.raceManager = GameObject.Find("Race Manager").transform.GetComponent<RaceManager>();

		// Initialize the Cameras reference to its Spaceship
		this.spaceship = this.transform.parent.parent.FindChild("Spaceship");
		// Initialize the Cameras reference to its SpaceshipController
		this.spaceshipController = this.spaceship.GetComponent<SpaceshipController>();
	}
	
	public void Update() {

		Camera camera = this.transform.GetComponent<Camera>();

		// Store the Viewports Aspect Ratio
		float aspectRatio = Screen.width / Screen.height;

		// Set the Aspect Ratio back to the Minimaps Ratio
		camera.aspect = 1.0f;

			
		// Update the Screens Offset so that it matches the Spaceships ID
		if(this.raceManager.spaceshipTotal != 1) {
			
			// Resize the Rendering Viewport
			camera.rect = new Rect(0.35f + (float)(this.spaceshipController.raceRecord.id - 1) * 0.5f, 1.0f - 0.15f * aspectRatio, 0.15f, 0.15f * aspectRatio);
		}
		else {
			
			// Resize the Rendering Viewport
			camera.rect = new Rect(0.80f,1.0f - 0.20f * aspectRatio, 0.20f, 0.20f * aspectRatio);
		}
	}
}