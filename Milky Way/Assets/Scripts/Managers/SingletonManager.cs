// Unity
using UnityEngine;

// System
using System;
using System.Collections.Generic;

// ReSharper disable StaticMemberInGenericType
namespace MilkyWay.Managers
{
	/// <summary>
	/// Abstract class that provides the necessary methods for a singleton manager.
	/// </summary>
	/// 
	/// <seealso cref="MonoBehaviour" />
	public abstract class SingletonManager<SingletonType> : MonoBehaviour
	{
		/// <summary>
		/// The singleton map of every singleton that implements this class.
		/// </summary>
		private static readonly Dictionary<Type, object> SingletonMap = new Dictionary<Type, object>();

		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static SingletonType Instance
		{
			get { return (SingletonType) SingletonMap[typeof(SingletonType)]; }
		}

		/// <summary>
		/// Called when the game-object is [enabled].
		/// </summary>
		public void Awake()
		{
			// Remove the previous instance
			if (SingletonMap.ContainsKey(GetType()))
				SingletonMap.Remove(GetType());

			// Add the new instance
			SingletonMap.Add(GetType(), this);
		}
	}
}