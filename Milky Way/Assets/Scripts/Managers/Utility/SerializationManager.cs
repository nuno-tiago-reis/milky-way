// Unity
using UnityEngine;

// Newtonsoft
using Newtonsoft.Json;

// JetBrains
using JetBrains.Annotations;

// System
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace MilkyWay.Managers
{
	/// <summary>
	/// Helper class that handles serialization.
	/// </summary>
	public static class SerializationManager
	{
		#region [Constants and Statics]
		#region [Statics]
		/// <summary>
		/// The serializer settings.
		/// </summary>
		private static JsonSerializerSettings SerializerSettings { get; set; }

		#region [Loading]
		/// <summary>
		/// Loads the necessary attributes.
		/// </summary>
		[UsedImplicitly] [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void LoadStaticAttributes()
		{
			// Create the serializer settings
			SerializerSettings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
				NullValueHandling = NullValueHandling.Include,
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
			};
		}
		#endregion
		#endregion
		#endregion

		#region [Methods] Serialization
		/// <summary>
		/// Serializes an object into the given file. Based from the resources folder.
		/// </summary>
		/// 
		/// <param name="serializationObject">The object to serialize.</param>
		/// <param name="serializationFileName">The file name to serialize the object to.</param>
		public static void Serialize<SerializationType>(SerializationType serializationObject,
			string serializationFileName)
		{
			// Create the writer
			JsonTextWriter writer = new JsonTextWriter(File.CreateText(Application.dataPath + "/" + serializationFileName))
			{
				Formatting = Formatting.Indented,
				Indentation = 1,
				IndentChar = '\t'
			};

			JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
			// Serialize the object
			serializer.Serialize(writer, serializationObject);

			// Close the serialization file
			writer.Close();
		}

		/// <summary>
		/// Deserializes an object from the given file. Based from the resources folder.
		/// </summary>
		/// 
		/// <param name="serializationFileName">The file name to deserialize the object from.</param>
		public static SerializationType Deserialize<SerializationType>(string serializationFileName)
		{
			// Open the serialization file
			StreamReader configurationFile = File.OpenText(Application.dataPath + "/" + serializationFileName);

			// Create the deserializer
			JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
			// Deserialize the object
			SerializationType serializationObject =
				(SerializationType) serializer.Deserialize(configurationFile, typeof(SerializationType));

			// Close the serialization file
			configurationFile.Close();

			return serializationObject;
		}
		#endregion
	}
}