// Unity
using UnityEngine;
using UnityEditor;

namespace UnityEditor
{
	/// <summary>
	/// Implements a text-box input pop-up window.
	/// </summary>
	/// 
	/// <seealso cref="EditorWindow" />
	public sealed class TextBoxWindow : EditorWindow
{
		#region [Attributes] Window
		/// <summary>
		/// Gets the window title.
		/// </summary>
		public string windowTitle { get; private set; }
		/// <summary>
		/// Gets the text box label.
		/// </summary>
		public string textBoxLabel { get; private set; }
		/// <summary>
		/// Gets the text box text.
		/// </summary>
		public string textBoxText { get; private set; }
		#endregion

		#region [Attributes] Delegate		
		/// <summary>
		/// Invoked when the window is closed.
		/// </summary>
		/// 
		/// <param name="text">The text selected.</param>
		public delegate void OnClose(string text);

		/// <summary>
		/// Gets or sets the on-close callback.
		/// </summary>
		private OnClose onCloseCallback { get; set; }
		#endregion

		#region [Methods] Window
		/// <summary>
		/// Initializes a new instance of the <see cref="TextBoxWindow"/> class.
		/// </summary>
		/// 
		/// <param name="windowTitle">The window title.</param>
		/// <param name="textBoxLabel">The text box label.</param>
		/// <param name="onCloseCallback">The on close callback.</param>
		public void Initialize(string windowTitle, string textBoxLabel, OnClose onCloseCallback)
{
			// Initialize the window
			this.windowTitle = windowTitle;
			this.textBoxLabel = textBoxLabel;
			this.textBoxText = string.Empty;

			// Initialize the callback
			this.onCloseCallback = onCloseCallback;
		}

		/// <summary>
		/// Called when the [GUI] is drawn.
		/// </summary>
		public void OnGUI()
{
			// Update the title
			this.titleContent.text = this.windowTitle;

			// Create the label
			EditorGUILayout.LabelField(this.textBoxLabel, EditorStyles.boldLabel);
			// Small space
			EditorGUILayout.Space();
			// Create the text box
			this.textBoxText = EditorGUILayout.TextField(this.textBoxText, EditorStyles.textField);
			// Remaining space
			GUILayout.FlexibleSpace();

			// Create the find button
			if(	(GUILayout.Button(this.windowTitle, EditorStyles.miniButtonMid)) ||
				(Event.current != null && Event.current.isKey && (Event.current.keyCode == KeyCode.End || Event.current.keyCode == KeyCode.KeypadEnter)))
{
				this.Close();

				// Trigger the delegate
				this.onCloseCallback(this.textBoxText);
			}
		}
		#endregion
	}
}