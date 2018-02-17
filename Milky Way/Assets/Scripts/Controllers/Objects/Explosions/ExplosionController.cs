// Unity
using UnityEngine;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements an explosion controller.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	public sealed class ExplosionController : MonoBehaviour
	{
		#region [Attributes]
		/// <summary>
		/// The flame particle system.
		/// </summary>
		private ParticleSystem Flame;
		/// <summary>
		/// The smoke particle system.
		/// </summary>
		private ParticleSystem Smoke;
		#endregion

		#region [Methods]
		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start()
		{
			this.Flame = this.transform.Find("Flame").GetComponent<ParticleSystem>();
			this.Smoke = this.transform.Find("Smoke").GetComponent<ParticleSystem>();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update()
		{
			if (this.Flame.isStopped && this.Smoke.isStopped)
				Destroy(this.gameObject);
		}
		#endregion
	}
}