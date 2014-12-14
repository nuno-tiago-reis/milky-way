using UnityEngine;

public class JoystickController : MonoBehaviour {

	// Joysticks ID
	public int joystickID
	{ get; protected set; }

	// Key Mappings
	public KeyCode triangle
	{ get; protected set; }
	public KeyCode circle
	{ get; protected set; }
	public KeyCode cross
	{ get; protected set; }
	public KeyCode square
	{ get; protected set; }

	public KeyCode L1
	{ get; protected set; }
	public KeyCode L2
	{ get; protected set; }
	public KeyCode L3
	{ get; protected set; }
	public KeyCode R1
	{ get; protected set; }
	public KeyCode R2
	{ get; protected set; }
	public KeyCode R3
	{ get; protected set; }

	public KeyCode start
	{ get; protected set; }
	public KeyCode select
	{ get; protected set; }
	public KeyCode playstation
	{ get; protected set; }

	public string verticalAxis
	{ get; protected set; }
	public string horizontalAxis
	{ get; protected set; }
	
	// Use this for initialization
	public void Initialize () {
		
		SpaceshipController spaceshipController = this.transform.GetComponent<SpaceshipController>();
		
		// Set the Joystick ID according to the Spaceship
		this.joystickID = spaceshipController.raceRecord.id;

		if(this.joystickID != 1 && this.joystickID != 2)
			this.joystickID = 1;

		// Set the Key Mappings according the the Joystick ID
		switch(this.joystickID) {

			case 1:	// Shape Buttons
					this.triangle = KeyCode.Joystick1Button0;
					this.circle = KeyCode.Joystick1Button1;
					this.cross = KeyCode.Joystick1Button2;
					this.square = KeyCode.Joystick1Button3;

					// Bumpers
					this.L1 = KeyCode.Joystick1Button4;
					this.L2 = KeyCode.Joystick1Button6;
					this.L3 = KeyCode.Joystick1Button9;
					this.R1 = KeyCode.Joystick1Button5;
					this.R2 = KeyCode.Joystick1Button7;
					this.R3 = KeyCode.Joystick1Button10;

					// Miscelaneous
					this.start = KeyCode.Joystick1Button11;
					this.select = KeyCode.Joystick1Button9;
					this.playstation = KeyCode.Joystick1Button12;

					break;

			case 2:	// Shape Buttons
					this.triangle = KeyCode.Joystick2Button0;
					this.circle = KeyCode.Joystick2Button1;
					this.cross = KeyCode.Joystick2Button2;
					this.square = KeyCode.Joystick2Button3;
						
					// Bumpers
					this.L1 = KeyCode.Joystick2Button4;
					this.L2 = KeyCode.Joystick2Button6;
					this.L3 = KeyCode.Joystick2Button9;
					this.R1 = KeyCode.Joystick2Button5;
					this.R2 = KeyCode.Joystick2Button7;
					this.R3 = KeyCode.Joystick2Button10;
					
					// Miscelaneous
					this.start = KeyCode.Joystick2Button11;
					this.select = KeyCode.Joystick2Button9;
					this.playstation = KeyCode.Joystick2Button12;
					
					break;
		}

		this.verticalAxis = "Vertical Axis Joystick " + this.joystickID;
		this.horizontalAxis = "Horizontal Axis Joystick " + this.joystickID;
	}

	public void Update() {

	}
}
