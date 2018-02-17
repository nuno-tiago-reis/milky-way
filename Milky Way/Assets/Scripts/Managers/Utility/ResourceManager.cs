// Unity
using UnityEngine;

// System
using System;
using Object = UnityEngine.Object;

namespace MilkyWay.Managers
{
	/// <summary>
	/// Helper class that stores the resource paths.
	/// </summary>
	public static class ResourceManager
	{
		/// <summary>
		/// The material resources root folder path.
		/// </summary>
		public const string Materials = "Materials/";
		/// <summary>
		/// The prefab resources root folder path.
		/// </summary>
		public const string Prefabs = "Prefabs/";
		/// <summary>
		/// The sprite resources root folder path.
		/// </summary>
		public const string Sprites = "Sprites/";
		/// <summary>
		/// The sound resources root folder path.
		/// </summary>
		public const string Sounds = "Sounds/";

		/// <summary>
		/// Loads a prefab from the resources folder (assumes the prefabs root folder, 'Prefab').
		/// </summary>
		/// 
		/// <param name="prefabName">Name of the prefab.</param>
		public static GameObject LoadGameObject(string prefabName)
		{
			try
			{
				GameObject gameObject = Object.Instantiate(Resources.Load(Prefabs + prefabName)) as GameObject;

				return gameObject;
			}
			catch(Exception exception)
			{
				Debug.LogError(string.Format("Error loading GameObject [{0}]: {1}", prefabName, exception.Message));
				throw;
			}

		}
		/// <summary>
		/// Loads a prefab from the resources folder (assumes the prefabs root folder, 'Prefab').
		/// </summary>
		/// 
		/// <param name="prefabName">Name of the prefab.</param>
		public static ComponentType LoadGameObject<ComponentType>(string prefabName) where ComponentType : Component
		{
			try
			{
				GameObject gameObject = Object.Instantiate(Resources.Load(Prefabs + prefabName)) as GameObject;

				return gameObject != null? gameObject.GetComponent<ComponentType>() : null;
			}
			catch(Exception exception)
			{
				Debug.LogError(string.Format("Error loading GameObject [{0}]: {1}", prefabName, exception.Message));
				throw;
			}

		}

		/// <summary>
		/// Loads a material from the resources folder (assumes the materials root folder, 'Materials').
		/// </summary>
		/// 
		/// <param name="materialName">Name of the material.</param>
		public static Material LoadMaterial(string materialName)
		{
			try
			{
				Material material = Resources.Load<Material>(Materials + materialName);

				return material;
			}
			catch(Exception exception)
			{
				Debug.LogError(string.Format("Error loading Material [{0}]: {1}", materialName, exception.Message));
				throw;
			}
		}

		/// <summary>
		/// Loads a sprite from the resources folder (assumes the sprites root folder, 'Sprites').
		/// </summary>
		/// 
		/// <param name="soundName">Name of the sprite.</param>
		public static Sprite LoadSprite(string soundName)
		{
			try
			{
				Sprite sprite = Resources.Load<Sprite>(Sprites + soundName);

				return sprite;
			}
			catch(Exception exception)
			{
				Debug.LogError(string.Format("Error loading Sprite [{0}]: {1}", soundName, exception.Message));
				throw;
			}
		}

		/// <summary>
		/// Loads a sound from the resources folder (assumes the sprites root folder, 'Sounds').
		/// </summary>
		/// 
		/// <param name="soundName">Name of the sound.</param>
		public static AudioClip LoadSound(string soundName)
		{
			try
			{
				AudioClip sound = Resources.Load<AudioClip>(Sounds + soundName);

				return sound;
			}
			catch(Exception exception)
			{
				Debug.LogError(string.Format("Error loading Sprite [{0}]: {1}", soundName, exception.Message));
				throw;
			}
		}

		/// <summary>
		/// Loads a resource from the resources folder (Doesn't assume the root folder).
		/// </summary>
		/// 
		/// <param name="resourceName">Name of the resource file.</param>
		public static AssetType LoadConfiguration<AssetType>(string resourceName) where AssetType : Object
		{
			try
			{
				AssetType resource = Resources.Load<AssetType>(resourceName);

				return resource;
			}
			catch(Exception exception)
			{
				Debug.LogError(string.Format("Error loading resource of type {0} [{1}]: {2}", typeof(AssetType).Name, resourceName, exception.Message));
				throw;
			}
		}
	}
}