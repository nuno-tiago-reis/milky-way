// Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// System
using System;
using System.Linq;

// MilkyWay
using MilkyWay.Utility;
using MilkyWay.Managers;
using MilkyWay.Objects.PowerUps;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements the spaceships interface.
	/// </summary>
	/// 
	/// <seealso cref="IObjectController" />
	/// <seealso cref="SingletonManager{InterfaceManager}" />
	public sealed class InterfaceManager : SingletonManager<InterfaceManager>, IObjectController
	{
		#region [Classes]
		/// <summary>
		/// The players interface controller.
		/// </summary>
		private sealed class PlayerInterfaceController
		{
			#region [Attributes] Standing
			/// <summary>
			/// The player standings text.
			/// </summary>
			private readonly Text Standing;
			#endregion

			#region [Attributes] Laps
			/// <summary>
			/// The player lap text.
			/// </summary>
			private readonly Text Lap;
			/// <summary>
			/// The player lap time text.
			/// </summary>
			private readonly Text LapTime;
			/// <summary>
			/// The player best lap time text.
			/// </summary>
			private readonly Text BestLapTime;
			#endregion

			#region [Attributes] Health
			/// <summary>
			/// The players health bar image.
			/// </summary>
			private readonly Image HealthBar;
			/// <summary>
			/// The players health bar text.
			/// </summary>
			private readonly Text HealthText;
			/// <summary>
			/// The players repair time text.
			/// </summary>
			private readonly Text RepairTime;
			#endregion

			#region [Attributes] Items
			/// <summary>
			/// The players star dust text.
			/// </summary>
			private readonly Text StarDust;
			/// <summary>
			/// The players power up images.
			/// </summary>
			private readonly Image[] PowerUpIcons;
			/// <summary>
			/// The players power up bar images.
			/// </summary>
			private readonly Image[] PowerUpBars;
			#endregion

			#region [Attributes] Spaceship
			/// <summary>
			/// The players spaceship.
			/// </summary>
			private readonly SpaceshipController Spaceship;
			#endregion

			#region [Methods]
			/// <summary>
			/// Initializes an instance of the <see cref="PlayerInterfaceController"/> class.
			/// </summary>
			/// <param name="spaceship"></param>
			public PlayerInterfaceController(SpaceshipController spaceship)
			{
				this.Spaceship = spaceship;

				// Retrieve the spaceships panel
				Transform spaceshipPanel = GameManager.Instance.transform.Find(string.Format("Interface/Canvas/Player {0}", spaceship.ID));

				// Retrieve the standings
				this.Standing = spaceshipPanel.Find("Panel/Standings/Value/Text").GetComponent<Text>();

				// Retrieve the lap
				this.Lap = spaceshipPanel.Find("Panel/Laps/Value/Text").GetComponent<Text>();
				// Retrieve the lap time
				this.LapTime = spaceshipPanel.Find("Panel/Lap Time/Value/Text").GetComponentInChildren<Text>();
				// Retrieve the best lap time
				this.BestLapTime = spaceshipPanel.Find("Panel/Best Lap Time/Value/Text").GetComponentInChildren<Text>();

				// Retrieve the health bar
				this.HealthBar = spaceshipPanel.Find("Panel/Health/Value/Image").GetComponentInChildren<Image>();
				// Retrieve the health text
				this.HealthText = spaceshipPanel.Find("Panel/Health/Value/Text").GetComponentInChildren<Text>();
				// Retrieve the repair time
				this.RepairTime = spaceshipPanel.Find("Panel/Repair Time/Value/Text").GetComponentInChildren<Text>();

				// Retrieve the power-up icons
				this.PowerUpIcons = new Image[]
				{
					spaceshipPanel.Find("PowerUps/HomingRocket/Icon").GetComponent<Image>(),
					spaceshipPanel.Find("PowerUps/Smokescreen/Icon").GetComponent<Image>(),
					spaceshipPanel.Find("PowerUps/Shield/Icon").GetComponent<Image>()
				};
				// Retrieve the power-up bars
				this.PowerUpBars = new Image[]
				{
					spaceshipPanel.Find("PowerUps/HomingRocket/Image").GetComponent<Image>(),
					spaceshipPanel.Find("PowerUps/Smokescreen/Image").GetComponent<Image>(),
					spaceshipPanel.Find("PowerUps/Shield/Image").GetComponent<Image>()
				};

				// Retrieve the star dust
				this.StarDust = spaceshipPanel.Find("Panel/Star Dust/Value/Text").GetComponentInChildren<Text>();
			}

			/// <summary>
			/// Updates this instance.
			/// </summary>
			public void Update()
			{
				// Update the standing
				switch (this.Spaceship.Record.CurrentStanding)
				{
					case 1:
						this.Standing.text = "1st Place";
						break;
					case 2:
						this.Standing.text = "2nd Place";
						break;
					case 3:
						this.Standing.text = "3rd Place";
						break;
					case 4:
						this.Standing.text = "4th Place";
						break;
				}

				// Update the lap
				this.Lap.text = string.Format("{0}/{1}", Mathf.Clamp(this.Spaceship.Record.CurrentLap + 1, 0, GameManager.Instance.LapTotal), GameManager.Instance.LapTotal);
				// Update the lap time
				TimeSpan lapTimeSpan = TimeSpan.FromSeconds(this.Spaceship.Record.CurrentLapTime);
				this.LapTime.text = string.Format("{0:00}:{1:00}:{2:000}", lapTimeSpan.Minutes, lapTimeSpan.Seconds, lapTimeSpan.Milliseconds);
				// Update the best lap time
				TimeSpan bestLapTimeSpan = TimeSpan.FromSeconds(this.Spaceship.Record.BestLapTime);
				this.BestLapTime.text = string.Format("{0:00}:{1:00}:{2:000}", bestLapTimeSpan.Minutes, bestLapTimeSpan.Seconds, bestLapTimeSpan.Milliseconds);

				if (this.Spaceship.Health != 0.0f)
				{
					// Update the health bar
					this.HealthBar.fillAmount = this.Spaceship.Health / this.Spaceship.MaximumHealth;
					this.HealthText.text = string.Format("{0}/{1}", this.Spaceship.Health, this.Spaceship.MaximumHealth);

					if (this.HealthBar.fillAmount > 0.5f)
						this.HealthBar.color = Color.green * 0.75f;
					else if (this.HealthBar.fillAmount > 0.25f)
						this.HealthBar.color = Color.yellow * 0.75f;
					else
						this.HealthBar.color = Color.red * 0.75f;

					// Enable the health bar
					this.HealthBar.transform.parent.parent.gameObject.SetActive(true);
					// Disable the repair time
					this.RepairTime.transform.parent.parent.gameObject.SetActive(false);
				}
				else
				{
					// Update the repair time
					TimeSpan repairTimeSpan = TimeSpan.FromSeconds(this.Spaceship.RepairTimer.Current);
					this.RepairTime.text = string.Format("{0:00}:{1:00}:{2:000}", repairTimeSpan.Minutes, repairTimeSpan.Seconds, repairTimeSpan.Milliseconds);

					// Enable the repair time
					this.RepairTime.transform.parent.parent.gameObject.SetActive(true);
					// Disable the health bar
					this.HealthBar.transform.parent.parent.gameObject.SetActive(false);
				}

				// Update the homing-rocket power-up
				IPowerUp homingRocket = this.Spaceship.PowerUpList.FirstOrDefault(powerUp => powerUp is HomingRocket);
				UpdatePowerUp(homingRocket, this.PowerUpBars[0], this.PowerUpIcons[0]);
				// Update the smokescreen power-up
				IPowerUp smokescreen = this.Spaceship.PowerUpList.FirstOrDefault(powerUp => powerUp is Smokescreen);
				UpdatePowerUp(smokescreen, this.PowerUpBars[1], this.PowerUpIcons[1]);
				// Update the shield power-up
				IPowerUp shield = this.Spaceship.PowerUpList.FirstOrDefault(powerUp => powerUp is Shield);
				UpdatePowerUp(shield, this.PowerUpBars[2], this.PowerUpIcons[2]);

				// Update the star dust
				this.StarDust.text = string.Format("{0}", this.Spaceship.StarDust);
			}

			/// <summary>
			/// Updates a power-ups icon and bar.
			/// </summary>
			/// 
			/// <param name="powerUp">The power-up.</param>
			/// <param name="powerUpBar">The power-up bar.</param>
			/// <param name="powerUpIcon">The power-up icon.</param>
			private void UpdatePowerUp(IPowerUp powerUp, Image powerUpBar, Image powerUpIcon)
			{
				if(powerUp != null)
				{
					Timer powerUpTimer = powerUp.PowerUpController != null ? powerUp.PowerUpController.DurationTimer : null;

					if(powerUp.Active && powerUpTimer != null)
					{
						powerUpBar.fillAmount = powerUpTimer.Current / powerUpTimer.Maximum;
						powerUpBar.color = new Color(0.75f, 0.0f, 0.0f, 1.0f);

						powerUpBar.fillAmount = powerUpTimer.Current / powerUpTimer.Maximum;
						powerUpIcon.color = new Color(0.75f, 0.0f, 0.0f, 1.0f);
					}
					else if(powerUp.Active == false)
					{
						powerUpBar.fillAmount = 1.0f;
						powerUpBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

						powerUpIcon.fillAmount = 1.0f;
						powerUpIcon.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					}
				}
				else
				{
					powerUpBar.fillAmount = 1.0f;
					powerUpBar.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

					powerUpIcon.fillAmount = 1.0f;
					powerUpIcon.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
				}
			}
			#endregion
		}
		#endregion

		#region [Attributes]
		/// <summary>
		/// The return to main menu pop up.
		/// </summary>
		private GameObject ReturnToMainMenuPopUp;
		/// <summary>
		/// The players interface controllers.
		/// </summary>
		private PlayerInterfaceController[] PlayerInterfaces;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Create the player interfaces
			this.PlayerInterfaces = new PlayerInterfaceController[GameManager.Instance.SpaceshipList.Count];

			// Initialize the player interfaces
			foreach(SpaceshipController spaceship in GameManager.Instance.SpaceshipList)
				this.PlayerInterfaces[spaceship.ID - 1] = new PlayerInterfaceController(spaceship);

			// Initialize the pop-up
			this.ReturnToMainMenuPopUp = this.transform.Find("MenuPopUp").gameObject;
			this.ReturnToMainMenuPopUp.SetActive(false);

			Button yesButton = this.ReturnToMainMenuPopUp.transform.Find("Yes").GetComponent<Button>();
			yesButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
			Button noButton = this.ReturnToMainMenuPopUp.transform.Find("No").GetComponent<Button>();
			noButton.onClick.AddListener(() => this.ReturnToMainMenuPopUp.SetActive(false));
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Update the player interfaces
			foreach(SpaceshipController spaceship in GameManager.Instance.SpaceshipList)
				this.PlayerInterfaces[spaceship.ID - 1].Update();

			// Update the pop-up
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.ReturnToMainMenuPopUp.SetActive(!this.ReturnToMainMenuPopUp.activeSelf);
			}
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}
		#endregion
	}
}