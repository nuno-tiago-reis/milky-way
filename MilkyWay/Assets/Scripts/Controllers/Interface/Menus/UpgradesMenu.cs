// Unity
using UnityEngine.UI;

// System
using System;
using System.Collections.Generic;
using System.Globalization;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Managers.Menu
{
	/// <summary>
	/// The upgrade entries.
	/// </summary>
	public enum UpgradeEntries
	{
		Health,
		Handling,
		WeaponPower,
		Acceleration,
		Points
	}

	/// <summary>
	/// Manages the upgrades menus in MilkyWay.
	/// </summary>
	public sealed class UpgradesMenu : MenuController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The maximum upgrade points.
		/// </summary>
		public const int MaximumUpgradePoints = 15;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The spaceship configurations.
		/// </summary>
		private SpaceshipConfiguration[] Configurations;

		/// <summary>
		/// The upgrade labels.
		/// </summary>
		private Dictionary<UpgradeEntries, Text>[] Labels;
		/// <summary>
		/// The upgrade sliders.
		/// </summary>
		private Dictionary<UpgradeEntries, Slider>[] Sliders;
		#endregion

		#region [Methods]
		/// <summary>
		/// Starts this instance.
		/// </summary>
		public override void Awake()
		{
			base.Awake();

			// Initilize the configurations
			this.Configurations = new SpaceshipConfiguration[2];
			// Initilize the labels and sliders
			this.Labels = new Dictionary<UpgradeEntries, Text>[2];
			this.Sliders = new Dictionary<UpgradeEntries, Slider>[2];

			for (int i = 0; i < 2; i++)
			{
				// Load the configuration (if it exists)
				this.Configurations[i] = SpaceshipConfiguration.LoadConfiguration(i+1);

				// Load the labels and sliders
				this.Labels[i] = new Dictionary<UpgradeEntries, Text>();
				this.Sliders[i] = new Dictionary<UpgradeEntries, Slider>();

				foreach(UpgradeEntries upgradeEntry in Enum.GetValues(typeof(UpgradeEntries)))
				{
					Text text = this.transform.Find(string.Format("Panel/Options/Player {0}/Stats/{1}/Value", i+1, upgradeEntry)).GetComponent<Text>();
					Slider slider = this.transform.Find(string.Format("Panel/Options/Player {0}/Stats/{1}/Slider", i + 1, upgradeEntry)).GetComponent<Slider>();

					switch (upgradeEntry)
					{
						case UpgradeEntries.Health:
							// Update the label
							text.text = this.Configurations[i].Health.ToString();
							// Update the slider
							slider.value = this.Configurations[i].Health;
							slider.onValueChanged.AddListener(UpdateHealth); 
							break;

						case UpgradeEntries.Handling:
							// Update the label
							text.text = this.Configurations[i].Handling.ToString();
							// Update the slider
							slider.value = this.Configurations[i].Handling;
							slider.onValueChanged.AddListener(UpdateHandling);
							break;

						case UpgradeEntries.WeaponPower:
							// Update the label
							text.text = this.Configurations[i].WeaponPower.ToString();
							// Update the slider
							slider.value = this.Configurations[i].WeaponPower;
							slider.onValueChanged.AddListener(UpdateWeaponPower);
							break;

						case UpgradeEntries.Acceleration:
							// Update the label
							text.text = this.Configurations[i].Acceleration.ToString();
							// Update the slider
							slider.value = this.Configurations[i].Acceleration;
							slider.onValueChanged.AddListener(UpdateAcceleration);
							break;

						case UpgradeEntries.Points:
							// Calculate the available points
							int points = MaximumUpgradePoints;
							points -= this.Configurations[i].Health;
							points -= this.Configurations[i].Handling;
							points -= this.Configurations[i].WeaponPower;
							points -= this.Configurations[i].Acceleration;
							// Update the label
							text.text = points.ToString();
							break;
					}

					this.Labels[i].Add(upgradeEntry, text);
					this.Sliders[i].Add(upgradeEntry, slider);
				}
			}
		}

		/// <summary>
		/// Updates the health.
		/// </summary>
		/// <param name="value">The value.</param>
		public void UpdateHealth(float value)
		{
			OnUpdate(UpgradeEntries.Health);
		}

		/// <summary>
		/// Updates the handling.
		/// </summary>
		/// <param name="value">The value.</param>
		public void UpdateHandling(float value)
		{
			OnUpdate(UpgradeEntries.Handling);
		}

		/// <summary>
		/// Updates the weapon power.
		/// </summary>
		/// <param name="value">The value.</param>
		public void UpdateWeaponPower(float value)
		{
			OnUpdate(UpgradeEntries.WeaponPower);
		}

		/// <summary>
		/// Updates the acceleration.
		/// </summary>
		/// <param name="value">The value.</param>
		public void UpdateAcceleration(float value)
		{
			OnUpdate(UpgradeEntries.Acceleration);
		}

		/// <summary>
		/// Gets the spent points.
		/// </summary>
		/// 
		/// <param name="spaceshipID">The spaceship identifier.</param>
		private int GetSpentPoints(int spaceshipID)
		{
			// Calculate the acumulated points
			int points = 0;
			points += (int)this.Sliders[spaceshipID][UpgradeEntries.Health].value;
			points += (int)this.Sliders[spaceshipID][UpgradeEntries.Handling].value;
			points += (int)this.Sliders[spaceshipID][UpgradeEntries.WeaponPower].value;
			points += (int)this.Sliders[spaceshipID][UpgradeEntries.Acceleration].value;

			return points;
		}

		/// <summary>
		/// Called when [update].
		/// </summary>
		private void OnUpdate(UpgradeEntries upgradeEntry)
		{
			// Validate the points
			for(int i = 0; i < 2; i++)
			{
				// Calculate the spent points
				int points = GetSpentPoints(i);

				// Too many points were spent
				if(points > MaximumUpgradePoints)
				{
					int difference = points - MaximumUpgradePoints;

					// Update the slider
					this.Sliders[i][upgradeEntry].value -= difference;

					// Update the points
					this.Labels[i][UpgradeEntries.Points].text = (MaximumUpgradePoints - points + difference).ToString();
				}
				else
				{
					// Update the points
					this.Labels[i][UpgradeEntries.Points].text = (MaximumUpgradePoints - points).ToString();
				}

				// Update the label
				this.Labels[i][upgradeEntry].text = this.Sliders[i][upgradeEntry].value.ToString(CultureInfo.InvariantCulture);

				// Update the configuration
				this.Configurations[i].Health = (int)this.Sliders[i][UpgradeEntries.Health].value;
				this.Configurations[i].Handling = (int)this.Sliders[i][UpgradeEntries.Handling].value;
				this.Configurations[i].WeaponPower = (int)this.Sliders[i][UpgradeEntries.WeaponPower].value;
				this.Configurations[i].Acceleration = (int)this.Sliders[i][UpgradeEntries.Acceleration].value;

				// Save the configuration
				SpaceshipConfiguration.SaveConfiguration(this.Configurations[i]);
			}
		}
		#endregion
	}
}