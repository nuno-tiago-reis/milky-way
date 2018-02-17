// Unity
using System;
using UnityEngine;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements the spaceships model controller.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	/// <seealso cref="IObjectController" />
	public sealed class ModelController : MonoBehaviour, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The models turn angle variation.
		/// </summary>
		public const float TurnAngleVariation = 0.97f;
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
			float angle = this.transform.localRotation.eulerAngles.z;
			angle = (angle > 180.0f ? (angle - 360.0f) : angle) * TurnAngleVariation;

			// Update the models rotation
			this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}
		#endregion
	}
}