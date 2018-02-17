// Unity
using UnityEngine;

// System
using System;

// MilkyWay
using MilkyWay.Managers;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements the spaceships camera controller.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	public sealed class CameraController : MonoBehaviour
	{
		#region [Constants and Statics]
		/// <summary>
		/// The dampen time.
		/// Higher values means the camera takes more time to adjust.
		/// </summary>
		private const float DampenTime = 0.05f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The camera component.
		/// </summary>
		private Camera Camera;
		/// <summary>
		/// The cameras spaceship.
		/// </summary>
		public SpaceshipController Spaceship;
		#endregion

		#region [Methods]
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			this.Camera = GetComponent<Camera>();

			switch (GameManager.Instance.RaceMode)
			{
				// Adjust the cameras viewport for multiplayer modes
				case RaceMode.Arena:
				case RaceMode.Track:

					this.Camera.rect = new Rect(0.5f * (Spaceship.ID - 1), 0.0f, 0.5f, 1.0f);
					break;

				// Adjust the cameras viewport for singleplayer modes
				case RaceMode.TimeAttack:

					this.Camera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void FixedUpdate()
		{
			// Calculate the destination from the spaceships position and place the camera behind it
			Vector3 destination = this.Spaceship.transform.position - this.Spaceship.transform.forward * 15.0f + this.Spaceship.transform.up * 12.5f;
			Vector3 velocity = Vector3.zero;

			// Adjust the position taking into account the dampen time required to catch up
			this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, DampenTime);
			// Adjust the rotation taking into account the spaceships position
			this.transform.LookAt(this.Spaceship.transform.position + this.Spaceship.transform.forward * 15.0f, this.Spaceship.transform.up);
		}
		#endregion
	}
}