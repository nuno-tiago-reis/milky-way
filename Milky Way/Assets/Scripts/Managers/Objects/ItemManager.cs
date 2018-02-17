// System
using System.Linq;
using System.Collections.Generic;

// MilkyWay
using MilkyWay.Objects.Items;

namespace MilkyWay.Managers
{
	/// <summary>
	/// Manages the items.
	/// </summary>
	public sealed class ItemManager : SingletonManager<ItemManager>, IObjectController
	{
		#region [Constants and Statics]
		/// <summary>
		/// The name of the item container game-object.
		/// </summary>
		public const string ItemContainerName = "Items";
		#endregion

		#region [Attributes]
		/// <summary>
		/// The list of item controllers <see cref="ItemController"/>.
		/// </summary>
		public List<ItemController> ItemList { get; private set; }
		/// <summary>
		/// The list of added item controllers <see cref="ItemController"/>.
		/// </summary>
		public List<ItemController> AddedItemList { get; private set; }
		/// <summary>
		/// The list of removed item controllers <see cref="ItemController"/>.
		/// </summary>
		public List<ItemController> RemovedItemList { get; private set; }
		#endregion

		#region [Methods]
		/// <inheritdoc />
		public void ObjectCreate()
		{
			// Find the items in the arena/track
			this.ItemList = GameManager.Instance.GetComponentsInChildren<ItemController>().ToList();
			this.AddedItemList = new List<ItemController>();
			this.RemovedItemList = new List<ItemController>();

			// Initialize the items
			foreach(ItemController item in this.ItemList)
			{
				item.transform.parent = this.transform;
				item.ObjectCreate();
			}
		}

		/// <inheritdoc />
		public void ObjectUpdate()
		{
			// Clean the item list
			this.ItemList.AddRange(this.AddedItemList);
			this.ItemList.RemoveAll(item => item == null);
			this.ItemList.RemoveAll(item => this.RemovedItemList.Contains(item));

			// Clean the temporary lists
			this.AddedItemList.Clear();
			this.RemovedItemList.Clear();

			// Update the items
			foreach(ItemController item in this.ItemList)
				item.ObjectUpdate();
		}

		/// <inheritdoc />
		public void ObjectDestroy()
		{
			// Clean the item list
			this.ItemList.RemoveAll(item => item == null);
			this.ItemList.RemoveAll(item => this.RemovedItemList.Contains(item));

			// Clean the temporary lists
			this.AddedItemList.Clear();
			this.RemovedItemList.Clear();

			// Destroy the items
			foreach(ItemController item in this.ItemList)
				item.ObjectDestroy();

			Destroy(this.gameObject);
		}

		/// <summary>
		/// Adds the item to the list.
		/// </summary>
		/// 
		/// <param name="item">The item.</param>
		public void AddItem(ItemController item)
		{
			if(this.ItemList.Contains(item) == false && this.AddedItemList.Contains(item) == false)
			{
				// Update the list
				this.AddedItemList.Add(item);
				// Update the parent
				item.transform.parent = this.transform;
			}
		}

		/// <summary>
		/// Removes the item from the list.
		/// </summary>
		/// <param name="item">The item.</param>
		public void RemoveItem(ItemController item)
		{
			if(this.ItemList.Contains(item) && this.RemovedItemList.Contains(item) == false)
			{
				// Update the list
				this.RemovedItemList.Add(item);
				// Update the parent
				item.transform.parent = null;
			}
		}
		#endregion
	}
}