using UnityEngine;

public class SpaceshipConfiguration {
	
	// Spaceships Health Stats
	public int health
	{ get; protected set; }
	private const int minimumHealth = 0;
	private const int maximumHealth = 5;

	// Spaceships Power Stats
	public int power
	{ get; protected set; }
	private const int minimumPower = 0;
	private const int maximumPower = 5;

	// Spaceships Speed Stats
	public int acceleration
	{ get; protected set; }
	private const int minimumAcceleration = 0;
	private const int maximumAcceleration = 5;

	// Spaceships Handling Stats
	public int handling
	{ get; protected set; }
	private const int minimumHandling = 0;
	private const int maximumHandling = 5;
	
	public SpaceshipConfiguration(int health, int power, int speed, int handling) {
		
		// Spaceships starting Health
		SetHealth(health);
		// Spaceships starting Power
		SetPower(power);
		// Spaceships starting Speed
		SetAcceleration(speed);
		// Spaceships starting Handling
		SetHandling(handling);
	}

	public void SetHealth(int health) {

		this.health = Mathf.Clamp(health, SpaceshipConfiguration.minimumHealth, SpaceshipConfiguration.maximumHealth);
	}

	public void SetPower(int power) {
		
		this.power = Mathf.Clamp(power, SpaceshipConfiguration.minimumPower, SpaceshipConfiguration.maximumPower);
	}

	public void SetAcceleration(int acceleration) {
		
		this.acceleration = Mathf.Clamp(acceleration, SpaceshipConfiguration.minimumAcceleration, SpaceshipConfiguration.maximumAcceleration);
	}

	public void SetHandling(int handling) {
		
		this.handling = Mathf.Clamp(handling, SpaceshipConfiguration.minimumHandling, SpaceshipConfiguration.maximumHandling);
	}
}
