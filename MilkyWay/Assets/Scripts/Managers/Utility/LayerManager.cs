// Unity
using UnityEngine;

// JetBrains
using JetBrains.Annotations;

// System
using System.Collections.Generic;

namespace MilkyWay.Managers
{
	/// <summary>
	/// Enumerates all the existing Tags.
	/// </summary>
	public enum Tag
	{
		/// <summary>
		/// Indicates that the given object is a spaceship.
		/// </summary>
		Spaceship,
		/// <summary>
		/// Indicates that the given object is a shield.
		/// </summary>
		Shield,

		/// <summary>
		/// Indicates that the given object is a boundary.
		/// </summary>
		Boundary,
		/// <summary>
		/// Indicates that the given object is a road.
		/// </summary>
		Road
	}

	/// <summary>
	/// Enumerates all the existing Layers.
	/// </summary>
	public enum Layer
	{
		/// <summary>
		/// Contains the Spaceships objects.
		/// </summary>
		Spaceships,
		/// <summary>
		/// Contains the Power-Ups objects.
		/// </summary>
		PowerUps,
		/// <summary>
		/// Contains the Items objects.
		/// </summary>
		Items,
		/// <summary>
		/// Contains the Tracks objects.
		/// </summary>
		Tracks,
		/// <summary>
		/// Contains the Tracker objects.
		/// </summary>
		Tracker,
		/// <summary>
		/// Contains the Environment objects.
		/// </summary>
		Environment
	}

	/// <summary>
	/// Helper class that managers the Layers.
	/// </summary>
	public static class LayerManager
	{
		#region [Constants and Statics]

		#region [Statics]
		/// <summary>
		/// LayerManagers mapping of tags <see cref="Tag"/>.
		/// </summary>
		private static Dictionary<Tag, string> TagNameMap;

		/// <summary>
		/// LayerManagers mapping of Layers <see cref="Layer"/>.
		/// </summary>
		private static Dictionary<Layer, int> LayerMap;
		/// <summary>
		/// LayerManagers mapping of layer names <see cref="Layer"/>.
		/// </summary>
		private static Dictionary<Layer, string> LayerNameMap;

		#region [Loading]
		/// <summary>
		/// Loads the necessary attributes.
		/// </summary>
		[UsedImplicitly] [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void LoadStaticAttributes()
		{
			// Initialize the tag layer mapping
			TagNameMap = new Dictionary<Tag, string>()
			{
				{ Tag.Spaceship,	"Spaceship" },
				{ Tag.Shield,		"Shield" },
				{ Tag.Boundary,     "Boundary" },
				{ Tag.Road,			"Road" }
			};

			// Initialize the layer mapping
			LayerMap = new Dictionary<Layer, int>()
			{
				{ Layer.Spaceships,		LayerMask.NameToLayer("Spaceships") },
				{ Layer.PowerUps,		LayerMask.NameToLayer("PowerUps") },
				{ Layer.Items,			LayerMask.NameToLayer("Items") },
				{ Layer.Tracks,			LayerMask.NameToLayer("Tracks") },
				{ Layer.Tracker,		LayerMask.NameToLayer("Tracker") },
				{ Layer.Environment,	LayerMask.NameToLayer("Environment") }
			};

			// Initialize the layer name mapping
			LayerNameMap = new Dictionary<Layer, string>()
			{
				{Layer.Spaceships,		"Spaceships"},
				{Layer.PowerUps,		"PowerUps"},
				{Layer.Items,			"Items"},
				{Layer.Tracks,			"Tracks"},
				{Layer.Tracker,			"Tracker"},
				{Layer.Environment,		"Interactables"}
			};
		}
		#endregion

		#endregion

		#endregion

		#region [Methods] Tags
		/// <summary>
		/// Returns a tags name.
		/// </summary>
		public static string GetTagName(Tag tag)
		{
			return TagNameMap[tag];
		}

		/// <summary>
		/// Determines whether the specified tag matches the provided tag name.
		/// </summary>
		/// 
		/// <param name="tag">The tag to match.</param>
		/// <param name="tagName">The tag name to match.</param>
		public static bool CompareTag(Tag tag, string tagName)
		{
			return GetTagName(tag) == tagName;
		}
		#endregion

		#region [Methods] Layers
		/// <summary>
		/// Returns a layer.
		/// </summary>
		public static int GetLayer(Layer layer)
		{
			return LayerMap[layer];
		}

		/// <summary>
		/// Returns a Layers name.
		/// </summary>
		public static string GetLayerName(Layer layer)
		{
			return LayerNameMap[layer];
		}

		/// <summary>
		/// Returns a Layers mask.
		/// </summary>
		public static LayerMask GetLayerMask(Layer layer)
		{
			return (1 << LayerMap[layer]);
		}

		/// <summary>
		/// Returns a Layers mask.
		/// </summary>
		public static LayerMask GetLayerMask(params Layer[] layers)
		{
			LayerMask layerMask = 0;

			// Concatenate the Layers into one
			foreach (Layer layer in layers)
				layerMask = layerMask | (1 << LayerMap[layer]);

			return layerMask;
		}

		/// <summary>
		/// Determines whether the specified layer mask contains the layer.
		/// </summary>
		/// 
		/// <param name="layerMask">The layer mask.</param>
		/// <param name="layer">The layer.</param>
		public static bool ContainsLayer(LayerMask layerMask, int layer)
		{
			return (layerMask.value & (1 << layer)) != 0;
		}

		/// <summary>
		/// Determines whether the specified layer mask contains the layer.
		/// </summary>
		/// 
		/// <param name="layerMask">The layer mask.</param>
		/// <param name="layer">The layer.</param>
		public static bool ContainsLayer(LayerMask layerMask, Layer layer)
		{
			return ContainsLayer(layerMask, GetLayer(layer));
		}

		/// <summary>
		/// Determines whether the specified layer matches the provided layer id.
		/// </summary>
		/// 
		/// <param name="layer">The layer to match.</param>
		/// <param name="layerId">The layer id to match.</param>
		public static bool CompareLayer(Layer layer, int layerId)
		{
			return GetLayer(layer) == layerId;
		}
		#endregion
	}
}