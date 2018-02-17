// Unity
using UnityEngine;

// System
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnityEditor
{
	/// <summary>
	/// Implements menu options to convert script files (e.g. normalizing line endings).
	/// </summary>
	public static class ScriptsMenu
	{
		#region [Statics and Constants]

		#region [Menu Paths]
		/// <summary>
		/// The generic menu path.
		/// </summary>
		private const string MenuPath = "Custom Tools/Scripts/";
		#endregion

		#region [Messages]

		#region [Conversion]
		/// <summary>
		/// The title for the dialog shown when attempting the end of line conversion.
		/// </summary>.
		private const string EndOfLineConversionDialogTitle = "EOL Conversion to {0} Format";
		/// <summary>
		/// The body for the dialog shown when attempting the end of line conversion.
		/// </summary>.
		private const string EndOfLineConversionDialogBody =
		  @"This operation may potentially modify many files in the current project!
			Hopefully you have backups of everything.
			Are you sure you want to proceed?";
		/// <summary>
		/// The confirmation message after attempting the end of line conversion.
		/// </summary>
		private const string EndOfLineConversionConfirmationMessage = "Conversion skipped {0} " + "files and changed {1} files";
		#endregion

		#endregion

		#region [Scripts]
		/// <summary>
		/// The file extensions considered for the end of line conversion.
		/// </summary>
		private static string[] ScriptExtensions = new string[]
{           "*.txt",
			"*.cs",
			"*.js",
			"*.boo",
			"*.compute",
			"*.shader",
			"*.cginc",
			"*.glsl",
			"*.xml",
			"*.xaml",
			"*.json",
			"*.inc",
			"*.css",
			"*.htm",
			"*.html",
		};
		/// <summary>
		/// The windows end of line string.
		/// </summary>
		private const string WindowsEndOfLine = "\r\n";
		/// <summary>
		/// The unix end of line string.
		/// </summary>
		private const string UnixEndOfLine = "\n";
		#endregion

		#endregion

		#region [Methods] Scripts
		/// <summary>
		/// Converts the line endings to windows format.
		/// </summary>
		[MenuItem(MenuPath + "Convert to Windows Format")]
		public static void ConvertLineEndingsToWindowsFormat()
		{
			ConvertLineEndings(false);
		}

		/// <summary>
		/// Converts the line endings to unix format.
		/// </summary>
		[MenuItem(MenuPath + "Convert to Unix Format")]
		public static void ConvertLineEndingsToUnixFormat()
		{
			ConvertLineEndings(true);
		}

		/// <summary>
		/// Converts the line endings to the specified format.
		/// </summary>
		/// 
		/// <param name="unixFormat">if set to <c>true</c> [is unix format].</param>
		private static void ConvertLineEndings(bool unixFormat)
		{
			// Show the confirmation dialog
			if(!EditorUtility.DisplayDialog(
				string.Format(EndOfLineConversionDialogTitle, (unixFormat ? "Unix" : "Windows")),
				EndOfLineConversionDialogBody,
				"Yes", "No"))
			{
				return;
			}

			Regex regex = new Regex(@"(?<!\r)\n");

			int totalFileCount = 0;

			List<string> changedFiles = new List<string>();

			StringComparison comparisonType = StringComparison.Ordinal;

			// For each script extension
			foreach(string fileExtension in ScriptExtensions)
			{
				// Retrieve all the files inside the assets folder with the current extension
				string[] filenames = Directory.GetFiles(Application.dataPath, fileExtension, SearchOption.AllDirectories);

				totalFileCount += filenames.Length;

				// Update the script
				foreach(string filename in filenames)
				{
					string originalText = File.ReadAllText(filename);
					string changedText = regex.Replace(originalText, WindowsEndOfLine);

					if(unixFormat)
						changedText = changedText.Replace(WindowsEndOfLine, UnixEndOfLine);

					bool isTextIdentical = string.Equals(changedText, originalText, comparisonType);

					if(!isTextIdentical)
					{
						changedFiles.Add(filename);

						File.WriteAllText(filename, changedText, System.Text.Encoding.UTF8);
					}
				}
			}

			int changedFileCount = changedFiles.Count;
			int skippedFileCount = (totalFileCount - changedFileCount);

			string message = string.Format(EndOfLineConversionConfirmationMessage, skippedFileCount, changedFileCount);

			if(changedFileCount <= 0)
			{
				message += ".";
			}
			else
			{
				message += (":" + WindowsEndOfLine);
				message += string.Join(WindowsEndOfLine, changedFiles.ToArray());
			}

			Debug.Log(message);

			// Recompile the modified scripts.
			if(changedFileCount > 0)
				AssetDatabase.Refresh();
		}
		#endregion
	}
}