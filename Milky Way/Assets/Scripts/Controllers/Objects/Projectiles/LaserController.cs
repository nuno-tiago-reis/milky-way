using MilkyWay.Managers;
using UnityEngine;

namespace MilkyWay.Objects.Projectiles
{
	/// <summary>
	/// Implements a laser controller.
	/// </summary>
	/// 
	/// <seealso cref="ProjectileController" />
	public sealed class LaserController : ProjectileController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The lasers force.
		/// </summary>
		public const float Force = 10000.0f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The lasers rigidbody.
		/// </summary>
		private Rigidbody Rigidbody;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public override void ObjectCreate()
		{
			base.ObjectCreate();

			// Initialize the speed
			this.Rigidbody = GetComponent<Rigidbody>();
			this.Rigidbody.useGravity = false;
			this.Rigidbody.AddForce(this.transform.forward * Force);
		}

		/// <inheritdoc />
		public override void ObjectUpdate()
		{
			base.ObjectUpdate();

			RaycastHit raycastHit;

			// Cast a ray downward towards the tracks
			if(Physics.Raycast(this.transform.position + this.transform.up * 5.0f, -this.transform.up, out raycastHit, 25.0f, LayerManager.GetLayerMask(Layer.Tracks)))
			{
				if(raycastHit.collider.tag == LayerManager.GetTagName(Tag.Road))
				{
					// Adjust the lasers position to hover over the track
					this.transform.position = raycastHit.point + this.transform.up * 4.25f;
				}
			}
		}

		/// <inheritdoc />
		public override void ObjectDestroy()
		{
			base.ObjectDestroy();
		}
		#endregion
	}
}