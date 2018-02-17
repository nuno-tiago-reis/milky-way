// System
using System.Linq;
using System.Collections.Generic;

// MilkyWay
//using MilkyWay.Objects.PowerUps;

namespace MilkyWay.Managers
{
	/// <summary>
	/// Manages the powerp-ups.
	/// </summary>
	public sealed class PowerUpManager : SingletonManager<PowerUpManager>, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The name of the power-up container game-object.
		/// </summary>
		public const string PowerUpContainerName = "PowerUps";
		#endregion

		#region [Attributes]
		/// <summary>
		/// The list of power-up controllers <see cref="IPowerUpController"/>.
		/// </summary>
		public List<IPowerUpController> PowerUpList { get; private set; }
		/// <summary>
		/// The list of added power-up controllers <see cref="IPowerUpController"/>.
		/// </summary>
		public List<IPowerUpController> AddedPowerUpList { get; private set; }
		/// <summary>
		/// The list of removed power-up controllers <see cref="IPowerUpController"/>.
		/// </summary>
		public List<IPowerUpController> RemovedPowerUpList { get; private set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Find the power-ups in the arena/track
			this.PowerUpList = GameManager.Instance.GetComponentsInChildren<IPowerUpController>().ToList();
			this.AddedPowerUpList = new List<IPowerUpController>();
			this.RemovedPowerUpList = new List<IPowerUpController>();

			// Initialize the power-ups
			foreach(IPowerUpController powerUp in this.PowerUpList)
			{
				powerUp.transform.parent = this.transform;
				powerUp.ObjectCreate();
			}
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Clean the powerUp list
			this.PowerUpList.AddRange(this.AddedPowerUpList);
			this.PowerUpList.RemoveAll(powerUp => powerUp == null);
			this.PowerUpList.RemoveAll(powerUp => this.RemovedPowerUpList.Contains(powerUp));

			// Clean the temporary lists
			this.AddedPowerUpList.Clear();
			this.RemovedPowerUpList.Clear();

			// Update the powerUps
			foreach(IPowerUpController powerUp in this.PowerUpList)
				powerUp.ObjectUpdate();
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			// Clean the powerUp list
			this.PowerUpList.RemoveAll(powerUp => powerUp == null);
			this.PowerUpList.RemoveAll(powerUp => this.RemovedPowerUpList.Contains(powerUp));

			// Clean the temporary lists
			this.AddedPowerUpList.Clear();
			this.RemovedPowerUpList.Clear();

			// Destroy the powerUps
			foreach(IPowerUpController powerUp in this.PowerUpList)
				powerUp.ObjectDestroy();

			Destroy(this.gameObject);
		}

		/// <summary>
		/// Adds the power-up to the list.
		/// </summary>
		/// 
		/// <param name="powerUp">The powerUp.</param>
		public void AddPowerUp(IPowerUpController powerUp)
		{
			if(this.PowerUpList.Contains(powerUp) == false && this.AddedPowerUpList.Contains(powerUp) == false)
			{
				// Update the list
				this.AddedPowerUpList.Add(powerUp);
				// Update the parent
				powerUp.transform.parent = this.transform;
			}
		}

		/// <summary>
		/// Removes the power-up from the list.
		/// </summary>
		/// <param name="powerUp">The powerUp.</param>
		public void RemovePowerUp(IPowerUpController powerUp)
		{
			if(this.PowerUpList.Contains(powerUp) && this.RemovedPowerUpList.Contains(powerUp) == false)
			{
				// Update the list
				this.RemovedPowerUpList.Add(powerUp);
				// Update the parent
				powerUp.transform.parent = null;
			}
		}
		#endregion
	}
}