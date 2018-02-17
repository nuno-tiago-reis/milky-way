// Unity
using UnityEngine;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements the spaceships trail controller.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	/// <seealso cref="IObjectController" />
	public sealed class TrailController : MonoBehaviour, IObjectController
	{
		#region [Attributes]
		/// <summary>
		/// The trails particle system.
		/// </summary>
		private Rigidbody RigidBody;
		/// <summary>
		/// The trails particle system.
		/// </summary>
		private ParticleSystem Particles;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			this.RigidBody = this.transform.parent.parent.parent.GetComponent<Rigidbody>();
			this.Particles = GetComponent<ParticleSystem>();
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			ParticleSystem.MainModule module = this.Particles.main;
			module.startSpeed = (this.RigidBody.velocity.magnitude / 100.0f) * 10.0f;
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}
		#endregion
	}
}