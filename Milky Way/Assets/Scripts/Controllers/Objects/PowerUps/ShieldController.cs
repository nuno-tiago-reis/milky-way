// Unity
using UnityEngine;

namespace MilkyWay.Objects.PowerUps
{
	/// <summary>
	/// Implements the Shield Controller.
	/// </summary>
	/// 
	/// <seealso cref="PowerUpController" />
	public sealed class ShieldController : PowerUpController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The shields rotation speed.
		/// </summary>
		public const float RotationSpeed = 2.5f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The shields HealthPoints.
		/// </summary>
		public float Health { get; set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override void ObjectCreate()
		{
			base.ObjectCreate();

			this.transform.position = this.Spaceship.transform.position;
		}

		/// <inheritdoc />
		public override void ObjectUpdate()
		{
			base.ObjectUpdate();

			// Update the shields position
			this.transform.position = this.Spaceship.transform.position;
			// Update the shields rotation
			this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.eulerAngles + Vector3.up * RotationSpeed);
		}

		/// <inheritdoc />
		public override void ObjectDestroy()
		{
			base.ObjectDestroy();
		}

		/// <inheritdoc />
		public override bool Activate()
		{
			// Update the shields position
			this.transform.position = this.Spaceship.transform.position;

			return base.Activate();
		}
		#endregion
	}
}