// MilkyWay
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.Items
{
	/// <summary>
	/// Class representing a star-fragment item that can be picked up by a spaceship.
	/// </summary>
	public class StarFragmentItemController : ItemController
	{
		#region [Attributes]
		/// <summary>
		/// The star-fragments star-dust amount.
		/// </summary>
		public int StarDust;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override void ObjectCreate()
		{
			base.ObjectCreate();
	
			this.Name = string.Format("StarFragment x{0}", StarDust);
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
				return spaceship.AddStarDust(this.StarDust);

			return false;
		}
		#endregion
	}
}