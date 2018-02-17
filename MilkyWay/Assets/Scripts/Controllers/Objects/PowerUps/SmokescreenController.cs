namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Implements the Smokescreen Controller.
	/// </summary>
	/// 
	/// <seealso cref="PowerUpController" />
	public sealed class SmokescreenController : PowerUpController
	{
		#region [Attributes]
		/// <summary>
		/// The smokescreens radius.
		/// </summary>
		public float Radius { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override void ObjectCreate()
		{
			base.ObjectCreate();
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
		public override bool Activate()
		{
			// Update the smokescreens position
			this.transform.position = this.Spaceship.transform.position;

			return base.Activate();
		}
		#endregion
	}
}