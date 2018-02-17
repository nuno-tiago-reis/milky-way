// Unity
using UnityEngine;

// System
using System.Diagnostics.CodeAnalysis;

// MilkyWay
using MilkyWay.Utility;

namespace MilkyWay
{
	/// <summary>
	/// Defines the necessary methods to implement a Runnable Object.
	/// 
	/// These methods are necessary to control the Objects Lifecycle.
	/// These events are used to initialized, update and destroy the Object.
	/// </summary>
	public interface IPowerUpController : IObjectController
	{
		#region [Attributes]
		/// <summary>
		/// Gets the transform.
		/// </summary>
		[SuppressMessage("ReSharper", "InconsistentNaming")]
		Transform transform { get; }
		/// <summary>
		/// The power-ups duration.
		/// </summary>
		float Duration { get; }
		/// <summary>
		/// The power-ups duration timer.
		/// </summary>
		Timer DurationTimer { get; }
		#endregion

		#region [Method]
		/// <summary>
		/// Activates the PowerUp.
		/// </summary>
		bool Activate();
		#endregion
	}
}