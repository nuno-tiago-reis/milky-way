using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

	public Transform spaceship
	{ get; private set; }
	public SpaceshipController spaceshipController
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

	public List<Texture2D> standingsTextureList
	{ get; private set; }


	// Current Lap Attributes
	public Vector2 lapPosition
	{ get; private set; }

	public float lapWidth
	{ get; private set; }
	public float lapHeight
	{ get; private set; }

	public List<Texture2D> lapTextureList
	{ get; private set; }

	// Health Attributes
	private HUDBar healthBar
	{ get; set; }

	// Repair Attributes
	private HUDBar repairBar
	{ get; set; }

	// Gold Attributes
	private HUDBar goldBar
	{ get; set; }

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

	// PowerUp Attributes
	public Vector2 powerUpPosition
	{ get; private set; }

	public float powerUpWidth
	{ get; private set; }
	public float powerUpHeight
	{ get; private set; }

	public Dictionary<string, Texture2D> powerUpTextureMap
	{ get; private set; }

	// Use this for initialization
	public void Start () {

		// HUDs reference to its Spaceship 
		this.spaceship = this.transform.parent.FindChild("Spaceship");
		// HUDs reference to its Spaceship Controller
		this.spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		// Set the ScreensID according to the Spaceships ID
		this.screenID = spaceshipController.id - 1;

		// Health Attributes
		this.healthBar = new HUDBar();
		this.healthBar.text = "Health";

		// Repair Attributes
		this.repairBar = new HUDBar();
		this.repairBar.text = "Repair Time";

		// Gold Attributes
		this.goldBar = new HUDBar();
		this.goldBar.text = "Gold";

		// Standings Attributes
		this.standingsTextureList = new List<Texture2D>();

		this.standingsTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Standings/1stPlace",typeof(Texture2D)) as Texture2D);
		this.standingsTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Standings/2ndPlace",typeof(Texture2D)) as Texture2D);
		this.standingsTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Standings/3rdPlace",typeof(Texture2D)) as Texture2D);
		this.standingsTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Standings/4thPlace",typeof(Texture2D)) as Texture2D);

		// Current Lap Attributes
		this.lapTextureList = new List<Texture2D>();

		this.lapTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Laps/1stLap",typeof(Texture2D)) as Texture2D);
		this.lapTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Laps/2ndLap",typeof(Texture2D)) as Texture2D);
		this.lapTextureList.Add((Texture2D)Resources.Load("Textures/HUD/Laps/3rdLap",typeof(Texture2D)) as Texture2D);

		// Speedometer Attributes
		this.pointerTexture = (Texture2D)Resources.Load("Textures/HUD/Speedometer/Pointer",typeof(Texture2D)) as Texture2D;
		this.speedometerTexture = (Texture2D)Resources.Load("Textures/HUD/Speedometer/Speedometer",typeof(Texture2D)) as Texture2D;

		// PowerUp Attributes
		this.powerUpTextureMap = new Dictionary<string, Texture2D>();
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

		// Update the Health Bars Position and Dimensions according to the Screens Resolution
		this.healthBar.position = new Vector2(Screen.width * 0.01f, Screen.height * 0.01f + this.standingHeight * 1.1f + this.lapHeight * 2.0f) + screenOffset;
		this.healthBar.width = Screen.width * 0.15f;
		this.healthBar.height = Screen.height * 0.05f;
	
		this.healthBar.amount = this.spaceshipController.health;
		this.healthBar.minimumAmount = spaceshipController.minimumHealth;
		this.healthBar.maximumAmount = spaceshipController.maximumHealth;
	
		// Update the Repair Bars Position and Dimensions according to the Screens Resolution
		this.repairBar.position = new Vector2(Screen.width * 0.01f, Screen.height * 0.01f + this.standingHeight * 1.1f + this.lapHeight * 2.0f + this.healthBar.height * 2.0f) + screenOffset;
		this.repairBar.width = Screen.width * 0.15f;
		this.repairBar.height = Screen.height * 0.05f;

		this.repairBar.amount = this.spaceshipController.repairTime;
		this.repairBar.minimumAmount = spaceshipController.minimumRepairTime;
		this.repairBar.maximumAmount = spaceshipController.maximumRepairTime;

		// Update the Gold Bars Position and Dimensions according to the Screens Resolution
		this.goldBar.position = new Vector2(Screen.width * 0.01f, Screen.height * 0.01f + this.standingHeight * 1.1f + this.lapHeight * 2.0f + this.healthBar.height * 2.0f + this.repairBar.height * 2.0f) + screenOffset;
		this.goldBar.width = Screen.width * 0.15f;
		this.goldBar.height = Screen.height * 0.05f;
		
		this.goldBar.amount = this.spaceshipController.gold;
		this.goldBar.minimumAmount = 0.0f;
		this.goldBar.maximumAmount = 0.0f;

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

		// Update the Power Up size according to the Screens Resolution
		this.powerUpPosition = new Vector2(Screen.width * 0.01f, Screen.height - this.powerUpHeight) + screenOffset;

		this.powerUpWidth = Screen.height * 0.10f;
		this.powerUpHeight = Screen.height * 0.10f;
	}
			
	public void OnGUI() {

		// Draw the Health Bar
		this.healthBar.Draw();
		// Draw the Repair Bar
		this.repairBar.Draw();
		// Draw the Gold Bar
		this.goldBar.Draw();

		// Draw the Standings
		int standing = this.spaceshipController.raceRecord.currentStanding;

		GUI.DrawTexture(new Rect(this.standingPosition.x, this.standingPosition.y, this.standingWidth, this.standingHeight), this.standingsTextureList[standing-1]);

		// Draw the Laps
		int lap = this.spaceshipController.raceRecord.currentLap;

		GUI.DrawTexture(new Rect(this.lapPosition.x, this.lapPosition.y, this.lapWidth, this.lapHeight), this.lapTextureList[lap]);

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

		// Draw the Power Ups
		float powerUpOffset = 0.0f;

		SpaceshipController spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		foreach(string powerUpName in spaceshipController.powerUpList) {

			Texture2D powerUpTexture;

			if(this.powerUpTextureMap.TryGetValue(powerUpName, out powerUpTexture) == false) {

				powerUpTexture = (Texture2D)Resources.Load("Textures/HUD/PowerUps/" + powerUpName, typeof(Texture2D)) as Texture2D;

				this.powerUpTextureMap.Add(powerUpName,powerUpTexture);
			}

			Vector2 powerUpPosition = this.powerUpPosition + new Vector2(powerUpOffset, 0.0f);

			GUI.DrawTexture (new Rect(powerUpPosition.x, powerUpPosition.y, this.powerUpWidth, this.powerUpHeight), powerUpTexture);

			powerUpOffset += powerUpWidth * 1.1f;
		}
	}
}
