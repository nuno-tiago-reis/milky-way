// Unity
using UnityEngine;

// MilkyWay
using MilkyWay.Managers;
using MilkyWay.Objects.Spaceships;

namespace MilkyWay.Objects.Items
{
	/// <summary>
	/// Abstract class representing an item that can be picked up by a spaceship.
	/// </summary>
	public abstract class ItemController : MonoBehaviour, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The items rotation speed.
		/// </summary>
		public const float RotationSpeed = 2.5f;
		#endregion

		#region [Attributes]
		/// <summary>
		/// The items name.
		/// </summary>
		protected string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public virtual void ObjectCreate()
		{
			// Initialize the name
			this.Name = GetType().Name;
	
			// Update the parent
			ItemManager.Instance.AddItem(this);
		}

		/// <inheritdoc />
		public virtual void ObjectUpdate()
		{
			Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
			eulerAngles.y += RotationSpeed;

			this.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
		}

		/// <inheritdoc />
		public virtual void ObjectDestroy()
		{
			Destroy(this.gameObject);
		}
		
		/// <summary>
		/// Called when a trigger collider enters this instance.
		/// </summary>
		/// 
		/// <param name="collider">The collider.</param>
		public void OnTriggerEnter(Collider collider)
		{
			// Collision with spaceships
			if(collider.gameObject.layer == LayerManager.GetLayer(Layer.Spaceships))
			{
				SpaceshipController spaceship = collider.GetComponent<SpaceshipController>();

				if(AddItem(spaceship))
					ObjectDestroy();
			}
		}

		/// <summary>
		/// Adds the item to the spaceship.
		/// </summary>
		/// 
		/// <param name="spaceship">The spaceship.</param>
		public abstract bool AddItem(SpaceshipController spaceship);
		#endregion
	}
}