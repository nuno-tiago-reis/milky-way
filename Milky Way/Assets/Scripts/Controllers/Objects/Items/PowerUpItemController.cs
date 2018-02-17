// MilkyWay

using MilkyWay.Objects.PowerUps;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.Items
{
	/// <summary>
	/// Class representing a power-up item that can be picked up by a spaceship.
	/// </summary>
	public sealed class PowerUpItemController : ItemController
	{
		#region [Attributes]
		/// <summary>
		/// The power up name.
		/// </summary>
		public string PowerUpName;
		/// <summary>
		/// The power up.
		/// </summary>
		private IPowerUp PowerUp;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override void ObjectCreate()
		{
			base.ObjectCreate();

			// Initialize the name
			this.Name = string.Format("PowerUp-{0}", PowerUpName);

			// Initialize the power-up
			switch(this.PowerUpName)
			{
				case "HomingRocket":
					this.PowerUp = new HomingRocket();
					break;
				case "Smokescreen":
					this.PowerUp = new Smokescreen();
					break;
				case "Shield":
					this.PowerUp = new Shield();
					break;
			}
			this.PowerUp.PowerUpCreate();
		}

		/// <inheritdoc />
		public override void ObjectUpdate()
		{
			base.ObjectUpdate();
		}

		/// <inheritdoc />
		public override void ObjectDestroy()
		{
			base.ObjectDestroy();
		}

		/// <inheritdoc />
		public override bool AddItem(SpaceshipController spaceship)
		{
			if (spaceship != null)
				return spaceship.AddPowerUp(this.PowerUp);

			return false;
		}
		#endregion
	}
}