// Unity
using UnityEngine;
using UnityEditor;

namespace UnityEditor
{
	/// <summary>
	/// Implements a combo-box input pop-up window.
	/// </summary>
	/// 
	/// <seealso cref="EditorWindow" />
	public sealed class ComboBoxWindow : EditorWindow
{
		#region [Attributes] Window
		/// <summary>
		/// Gets the window title.
		/// </summary>
		public string windowTitle { get; private set; }
		/// <summary>
		/// Gets the combo box label.
		/// </summary>
		public string comboBoxLabel { get; private set; }
		/// <summary>
		/// Gets the combo box option.
		/// </summary>
		public int comboBoxOption { get; private set; }
		/// <summary>
		/// Gets the combo box options.
		/// </summary>
		public string[] comboBoxOptions { get; private set; }
		#endregion

		#region [Attributes] Delegate		
		/// <summary>
		/// Invoked when the window is closed.
		/// </summary>
		/// 
		/// <param name="option">The option selected.</param>
		public delegate void OnClose(string option);

		/// <summary>
		/// Gets or sets the on-close callback.
		/// </summary>
		private OnClose onCloseCallback { get; set; }
		#endregion

		#region [Methods] Window
		/// <summary>
		/// Initializes a new instance of the <see cref="ComboBoxWindow"/> class.
		/// </summary>
		/// 
		/// <param name="windowTitle">The window title.</param>
		/// <param name="comboBoxLabel">The combo box label.</param>
		/// <param name="comboBoxOptions">The combo box options.</param>
		/// <param name="onCloseCallback">The on close callback.</param>
		public void Initialize(string windowTitle, string comboBoxLabel, string[] comboBoxOptions, OnClose onCloseCallback)
{
			// Initialize the window
			this.windowTitle = windowTitle;
			this.comboBoxLabel = comboBoxLabel;
			this.comboBoxOptions = comboBoxOptions;

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
			EditorGUILayout.LabelField(this.comboBoxLabel, EditorStyles.boldLabel);
			// Small space
			EditorGUILayout.Space();
			// Create the text box
			this.comboBoxOption = EditorGUILayout.Popup(this.comboBoxOption, this.comboBoxOptions, EditorStyles.foldout);
			// Remaining space
			GUILayout.FlexibleSpace();

			// Create the find button
			if( (GUILayout.Button(this.windowTitle, EditorStyles.miniButtonMid)) ||
				(Event.current != null && Event.current.isKey && (Event.current.keyCode == KeyCode.End || Event.current.keyCode == KeyCode.KeypadEnter)))
{
				this.Close();

				// Trigger the delegate
				this.onCloseCallback(this.comboBoxOptions[this.comboBoxOption]);
			}
		}
		#endregion
	}
}