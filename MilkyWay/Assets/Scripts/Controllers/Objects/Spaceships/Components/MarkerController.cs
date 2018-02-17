// Unity
using UnityEngine;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements the spaceships tracker controller.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	/// <seealso cref="IObjectController" />
	public sealed class MarkerController : MonoBehaviour, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The trackers rotation speed.
		/// </summary>
		public const float RotationSpeed = 2.5f;
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{ 
			// Update the markers rotation
			this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.eulerAngles + Vector3.up * RotationSpeed);
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}
		#endregion
	}
}