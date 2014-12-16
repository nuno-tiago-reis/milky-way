using UnityEngine;
using UnityEngine.UI;

using System;

using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

	// HUDs reference to the RaceManager
	public RaceManager raceManager
	{ get; protected set; }

	// HUDs reference to the Spaceship
	public Transform spaceship
	{ get; protected set; }
	// HUDs reference to the Spaceship Controller
	public SpaceshipController spaceshipController
	{ get; protected set; }

	// HUD Screen offset
	public Vector2 screenOffset
	{ get; protected set; }
	public Vector2 speedometerOffset
	{ get; protected set; }

	// Standings Attributes
	public Vector2 standingPosition
	{ get; protected set; }

	public const float standingHeightOffset = 10.0f;
	public const float standingHeight = 25.0f;
	public const float standingWidth = 100.0f;

	public List<Texture2D> standingsTextureList
	{ get; protected set; }

	// Current Lap Attributes
	public Vector2 lapPosition
	{ get; protected set; }

	public const float lapsHeightOffset = 25.0f;
	public const float lapsHeight = 25.0f;
	public const float lapsWidth = 100.0f;

	public List<Texture2D> lapTextureList
	{ get; protected set; }

	// Best Lap Time Attributes
	public HUDLabel bestLapTimeLabel
	{ get; protected set; }

	// Current Lap Time Attributes
	public HUDLabel currentLapTimeLabel
	{ get; protected set; }

	// Bar Attributes
	public const float barHeightOffset = 20.0f;
	public const float barHeight = 20.0f;
	public const float barWidth = 200.0f;
	
	// Health Attributes
	public HUDBar healthBar
	{ get; protected set; }

	// Repair Attributes
	public HUDBar repairBar
	{ get; protected set; }

	// Gold Attributes
	public HUDBar goldBar
	{ get; protected set; }

	// Speedometer Attributes
	public Vector2 speedometerPosition
	{ get; private set; }

	public const float speedometerSize = 200.0f;

	public float speedometerHeight
	{ get; private set; }
	public float speedometerWidth
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

    // PowerUpKey Attributes
    public Vector2 powerUpKeyPosition
    { get; private set; }

    public float powerUpKeyWidth
    { get; private set; }
    public float powerUpKeyHeight
    { get; private set; }

	public Dictionary<string, Texture2D> powerUpTextureMap
	{ get; private set; }

    public Dictionary<string, Texture2D> powerUpKeysMap
    { get; private set; }

	// Use this for initialization
	public void Awake () {
	}

	public void Initialize() {

		// Initialize the Cameras reference to the RaceManager
		this.raceManager = this.transform.parent.parent.GetComponent<RaceManager>();
		
		// Initialize the Cameras reference to its Spaceship
		this.spaceship = this.transform.parent.FindChild("Spaceship");
		// Initialize the Cameras reference to its SpaceshipController
		this.spaceshipController = this.spaceship.GetComponent<SpaceshipController>();

		// HUDs reference to its Spaceship 
		this.spaceship = this.transform.parent.FindChild("Spaceship");
		// HUDs reference to its Spaceship Controller
		this.spaceshipController = this.spaceship.GetComponent<SpaceshipController>();
		
		// Best Lap Attributes
		this.bestLapTimeLabel = new HUDLabel();
		this.bestLapTimeLabel.label = "Best Lap Time";
		
		// Current Lap Attributes
		this.currentLapTimeLabel = new HUDLabel();
		this.currentLapTimeLabel.label = "Current Lap Time";
		
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
		this.pointerTexture = (Texture2D)Resources.Load("Textures/HUD/Speedometer/PointerV2",typeof(Texture2D)) as Texture2D;
		this.speedometerTexture = (Texture2D)Resources.Load("Textures/HUD/Speedometer/SpeedometerV2",typeof(Texture2D)) as Texture2D;
		
		// PowerUp Attributes
		this.powerUpTextureMap = new Dictionary<string, Texture2D>();
		
		this.powerUpKeysMap = new Dictionary<string, Texture2D>();
	}
	
	// Update is called once per frame
	public void Update() {

		// Update the Screens Offset so that it matches the Spaceships ID
		if(this.raceManager.spaceshipTotal != 1) {

			this.screenOffset = new Vector2(Screen.width * (0.5f * (float)(this.spaceshipController.raceRecord.id - 1)), 0.0f);
			this.speedometerOffset = new Vector2(Screen.width * (0.5f * (float)(this.spaceshipController.raceRecord.id - 2)), 0.0f);
		}
		else {

			this.screenOffset = new Vector2(0.0f, 0.0f);
			this.speedometerOffset = new Vector2(0.0f, 0.0f);
		}

		Vector2 position = new Vector2(Screen.width * 0.01f, Screen.height * 0.01f) + this.screenOffset;

		// Update the Standings Texture Position and Value
		position = UpdateStandings(position);

		// Update the Laps Texture Position and Value
		position = UpdateLaps(position);
		// Update the Current and Best Times Labels Positions and Values
		position = UpdateTimers(position);

		// Update the Health Bars Position and Value
		position = UpdateHealth(position);
		// Update the Repair Bars Position and Value
		position = UpdateRepair(position);
		// Update the Gold Bars Position and Value
		position = UpdateGold(position);

		// Update the Speedometers Position and Value
		UpdateSpeedometer();

		// Update the PowerUps Position and Values
		UpdatePowerUps();
	}

	public Vector2 UpdateStandings(Vector2 position) {

		if(this.raceManager.mode == RaceManager.RaceMode.SingleRace) {
			
			// Update the Standings Texture Position according to the Screen
			this.standingPosition = position;
			
			return position + new Vector2(0.0f, HUD.standingHeight + HUD.standingHeightOffset);
		}

		return position;
	}
	
	public Vector2 UpdateLaps(Vector2 position) {

		if(this.raceManager.mode == RaceManager.RaceMode.SingleRace || this.raceManager.mode == RaceManager.RaceMode.TimeAttack) {

			// Update the Laps Texture Position according to the Screen
			this.lapPosition = position;
			
			return position + new Vector2(0.0f, HUD.lapsHeight + HUD.lapsHeightOffset);
		}

		return position;
	}
	
	public Vector2 UpdateTimers(Vector2 position) {

		if(this.raceManager.mode == RaceManager.RaceMode.TimeAttack) {

			// Update the Best Lap Time Labels Position and Dimensions according to the Screens Resolution
			this.bestLapTimeLabel.position = position;

			this.bestLapTimeLabel.width = HUD.lapsWidth;
			this.bestLapTimeLabel.height = HUD.lapsHeight;

			// Update the Best Lap Time Labels Value	
			TimeSpan bestLapTime = TimeSpan.FromSeconds(this.spaceshipController.raceRecord.bestLapTime);

			this.bestLapTimeLabel.text = string.Format("{0:00}:{1:00}:{2:000}", bestLapTime.Minutes, bestLapTime.Seconds, bestLapTime.Milliseconds);

			// Update the Current Lap Time Position and Dimensions according to the Screens Resolution
			this.currentLapTimeLabel.position = position + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset);
			
			this.currentLapTimeLabel.width = HUD.lapsWidth;
			this.currentLapTimeLabel.height = HUD.lapsHeight;
			
			// Update the Current Lap Time Labels Value
			TimeSpan currentLapTime = TimeSpan.FromSeconds(this.spaceshipController.raceRecord.currentLapTime);
			
			this.currentLapTimeLabel.text = string.Format("{0:00}:{1:00}:{2:000}", currentLapTime.Minutes, currentLapTime.Seconds, currentLapTime.Milliseconds);
			
			return position + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset * 2.0f) + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset);
		}
		else {

			// Update the Current Lap Time Position and Dimensions according to the Screens Resolution
			this.currentLapTimeLabel.position = position;

			this.currentLapTimeLabel.width = HUD.lapsWidth;
			this.currentLapTimeLabel.height = HUD.lapsHeight;

			// Update the Current Lap Time Labels Value
			TimeSpan currentLapTime = TimeSpan.FromSeconds(this.spaceshipController.raceRecord.currentLapTime);
			
			this.currentLapTimeLabel.text = string.Format("{0:00}:{1:00}:{2:000}", currentLapTime.Minutes, currentLapTime.Seconds, currentLapTime.Milliseconds);

			return position + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset * 2.0f);
		}
	}
	
	public Vector2 UpdateHealth(Vector2 position) {

		// Update the Health Bars Position and Dimensions according to the Screens Resolution
		this.healthBar.position = position;
		
		this.healthBar.width = HUD.barWidth;
		this.healthBar.height = HUD.barHeight;
		
		this.healthBar.amount = this.spaceshipController.health;
		this.healthBar.minimumAmount = spaceshipController.minimumHealth;
		this.healthBar.maximumAmount = spaceshipController.maximumHealth;

		return position + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset);
	}
	
	public Vector2 UpdateRepair(Vector2 position) {

		// Update the Repair Bars Position and Dimensions according to the Screens Resolution
		this.repairBar.position = position;
		
		this.repairBar.width = HUD.barWidth;
		this.repairBar.height = HUD.barHeight;
		
		this.repairBar.amount = this.spaceshipController.repairTime;
		this.repairBar.minimumAmount = spaceshipController.minimumRepairTime;
		this.repairBar.maximumAmount = spaceshipController.maximumRepairTime;

		return position + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset);
	}
	
	public Vector2 UpdateGold(Vector2 position) {

		// Update the Gold Bars Position and Dimensions according to the Screens Resolution
		this.goldBar.position = position;
		
		this.goldBar.width = HUD.barWidth;
		this.goldBar.height = HUD.barHeight;
		
		this.goldBar.amount = this.spaceshipController.gold;
		this.goldBar.minimumAmount = 0.0f;
		this.goldBar.maximumAmount = 0.0f;

		return position + new Vector2(0.0f, HUD.barHeight + HUD.barHeightOffset);
	}
	
	public void UpdateSpeedometer() {

		// Update the Speedometers size according to the Screens Resolution
		this.speedometerPosition = new Vector2(Screen.width - this.speedometerWidth, Screen.height - this.speedometerHeight) + speedometerOffset;
		
		this.speedometerWidth = HUD.speedometerSize * 1.5f;
		this.speedometerHeight = HUD.speedometerSize;
		
		// Update the Pointers size according to the Screens Resolution
		this.pointerWidth = 10.0f;
		this.pointerHeight = speedometerWidth * 0.35f;
		
		// Update the Pointers position according to the Screens Resolution and to the Spaceships Velocity
		float pointerAngle = (Mathf.Abs(this.spaceship.rigidbody.velocity.magnitude) / 300.0f  + 0.75f) * Mathf.PI;
		
		Vector2 pointerDistance = new Vector3(
			this.pointerHeight * Mathf.Cos (pointerAngle) - this.pointerHeight * Mathf.Sin (pointerAngle),
			this.pointerHeight * Mathf.Sin (pointerAngle) + this.pointerHeight * Mathf.Cos (pointerAngle));
		
		this.pointerStart = new Vector2(Screen.width - speedometerWidth * 0.5f, Screen.height - speedometerHeight * 0.15f) + speedometerOffset;
		this.pointerFinish = this.pointerStart + pointerDistance;
	}
	
	public void UpdatePowerUps() {

		// Update the Power Up size according to the Screens Resolution
		this.powerUpPosition = new Vector2(Screen.width * 0.01f, Screen.height - this.powerUpHeight) + screenOffset;
		
		this.powerUpWidth = Screen.height * 0.10f;
		this.powerUpHeight = Screen.height * 0.10f;
		
		this.powerUpKeyPosition = new Vector2(Screen.width * 0.018f, Screen.height * 0.84f) + screenOffset;
		
		this.powerUpKeyWidth = Screen.height * 0.06f;
		this.powerUpKeyHeight = Screen.height * 0.06f;
	}
			
	public void OnGUI() {

		if(this.raceManager.mode == RaceManager.RaceMode.SingleRace) {
			
			// Draw the Standings on the Top Left Corner
			DrawStandings();
			// Draw the Laps on the Top Left Corner
			DrawLaps();
		}

		if(this.raceManager.mode == RaceManager.RaceMode.TimeAttack) {

			// Draw the Best Lap Label on the Top Left Corner
			this.bestLapTimeLabel.Draw();
		}

		// Draw the Current Lap Label on the Top Left Corner
		this.currentLapTimeLabel.Draw();

		// Draw the Health Bar on the Left
		this.healthBar.Draw();
		// Draw the Repair Bar on the Left
		this.repairBar.Draw();
		// Draw the Gold Bar on the Left
		this.goldBar.Draw();

		// Draw the Speedometer on the Lower Right Corner
		DrawSpeedometer();

		// Draw the Speedometer on the Lower Left Corner
		DrawPowerUps();
	}

	public void DrawStandings() {

		// Draw the Standings
		int standing = Mathf.Clamp(this.spaceshipController.raceRecord.currentStanding-1, 0, this.raceManager.spaceshipTotal-1);
		
		GUI.DrawTexture(new Rect(this.standingPosition.x, this.standingPosition.y, HUD.standingWidth, HUD.standingHeight), this.standingsTextureList[standing]);
	}

	public void DrawLaps() {

		// Draw the Laps
		int lap = Mathf.Clamp(this.spaceshipController.raceRecord.currentLap, 0, this.raceManager.lapTotal-1);
		
		GUI.DrawTexture(new Rect(this.lapPosition.x, this.lapPosition.y, HUD.lapsWidth, HUD.lapsHeight), this.lapTextureList[lap]);
	}

	public void DrawSpeedometer() {

		// Draw the Speedometer
		GUI.DrawTexture(new Rect(this.speedometerPosition.x, this.speedometerPosition.y, this.speedometerWidth, this.speedometerHeight), this.speedometerTexture);
		
		// Draw the Pointer
		Vector2 distance = this.pointerFinish - this.pointerStart;
		float a = Mathf.Rad2Deg * Mathf.Atan(distance.y / distance.x);
		
		if(distance.x < 0)
			a += 180;
		
		int width = (int)Mathf.Ceil(this.pointerWidth / 2);
		
		GUIUtility.RotateAroundPivot(a, this.pointerStart);
		GUI.DrawTexture(new Rect(this.pointerStart.x, this.pointerStart.y - width, this.pointerHeight, width), this.pointerTexture);
		GUIUtility.RotateAroundPivot(-a, this.pointerStart);
	}

	public void DrawPowerUps() {

		// Draw the Power Ups
		float powerUpOffset = 0.0f;
		
		SpaceshipController spaceshipController = this.spaceship.GetComponent<SpaceshipController>();
		
		foreach(string powerUpName in spaceshipController.powerUpList) {
			
			Texture2D powerUpTexture;
			
			Texture2D powerUpKeyTexture;
			
			if(this.powerUpTextureMap.TryGetValue(powerUpName, out powerUpTexture) == false) {
				
				powerUpTexture = (Texture2D)Resources.Load("Textures/HUD/PowerUps/" + powerUpName, typeof(Texture2D)) as Texture2D;
				
				this.powerUpTextureMap.Add(powerUpName,powerUpTexture); 
			}
			
			if (this.powerUpKeysMap.TryGetValue(powerUpName + "K", out powerUpKeyTexture) == false) {
				
				powerUpKeyTexture = (Texture2D)Resources.Load("Textures/HUD/PowerUps/" + powerUpName + "K", typeof(Texture2D)) as Texture2D;
				
				this.powerUpKeysMap.Add(powerUpName + "K", powerUpKeyTexture);
			}
			
            // Power Up Position
			Vector2 powerUpPosition = this.powerUpPosition + new Vector2(powerUpOffset, 0.0f);
			
            // Power Up Key Position
			Vector2 powerUpKeyPosition = this.powerUpKeyPosition + new Vector2(powerUpOffset, 0.0f);
			
			GUI.DrawTexture (new Rect(powerUpPosition.x, powerUpPosition.y, this.powerUpWidth, this.powerUpHeight), powerUpTexture);
			
			GUI.DrawTexture(new Rect(powerUpKeyPosition.x, powerUpKeyPosition.y, this.powerUpKeyWidth, this.powerUpKeyHeight), powerUpKeyTexture);

			powerUpOffset += powerUpWidth * 1.1f;

		}
	}
}
