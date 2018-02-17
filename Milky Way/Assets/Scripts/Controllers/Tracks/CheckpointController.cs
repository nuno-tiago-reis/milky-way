// Unity
using UnityEngine;

// System
using System.Text.RegularExpressions;

// MilkyWay
using MilkyWay.Managers;

namespace MilkyWay.Objects.Spaceships
{
	/// <summary>
	/// Implements a checkpoint.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	public sealed class CheckpointController : MonoBehaviour, IObjectController
	{
		#region [Attributes]
		/// <summary>
		/// The checkpoints identifier.
		/// </summary>
		public int ID { get; private set; }
		#endregion  

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Extract the checkpoints identifier
			Regex regex = new Regex(@"([a-zA-Z]+)\s([0-9]+)");
			Match match = regex.Match(this.name);

			// Parse the checkpoints identifier
			this.ID = int.Parse(match.Groups[2].Value);
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}

		/// <summary>
		/// Called when another collider enters the checkpoints collider.
		/// </summary>
		/// 
		/// <param name="collider">The collider.</param>
		public void OnTriggerEnter(Collider collider)
		{
			if (collider.transform.gameObject.layer == LayerManager.GetLayer(Layer.Spaceships))
			{
				SpaceshipController spaceship = collider.transform.GetComponent<SpaceshipController>();

				if (spaceship.Record.CurrentCheckpoint == this.ID - 1)
					spaceship.Record.CurrentCheckpoint = this.ID;
			}
		}
		#endregion
	}
}