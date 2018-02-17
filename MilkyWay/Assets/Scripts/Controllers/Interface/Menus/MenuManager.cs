// Unity
using UnityEngine;

// System
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MilkyWay.Managers.Menu
{
	/// <summary>
	/// Manages the main-menu in MilkyWay.
	/// </summary>
	public sealed class MenuManager : MonoBehaviour
	{
		#region [Constants and Statics]
		/// <summary>
		/// The main menu game-object name.
		/// </summary>
		public const string MainMenuName = "Main Menu";

		/// <summary>
		/// The singleplayer menu game-object name.
		/// </summary>
		public const string SingleplayerMenuName = "Singleplayer Menu";
		/// <summary>
		/// The multiplayer menu game-object name.
		/// </summary>
		public const string MultiplayerMenuName = "Multiplayer Menu";
		/// <summary>
		/// The upgrade menu game-object name.
		/// </summary>
		public const string UpgradeMenuName = "Upgrade Menu";
		#endregion

		#region [Attributes]
		/// <summary>
		/// The current menu.
		/// </summary>
		public MenuController CurrentMenu { get; private set; }
		/// <summary>
		/// The list of available menus.
		/// </summary>
		public List<MenuController> MenuList { get; private set; }
		#endregion

		#region [Methods]
		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start()
		{
			// Retrieve the menus
			this.MenuList = new List<MenuController>(GetComponentsInChildren<MenuController>());

			// Show the main menu by default
			ShowMenu(this.MenuList.FirstOrDefault(menu => menu.name == MainMenuName));
		}

		/// <summary>
		/// Shows the specified menu.
		/// </summary>
		public void ShowMenu(MenuController menuController)
		{
			// Hide the previous menu
			if (this.CurrentMenu != null)
				this.CurrentMenu.gameObject.SetActive(false);

			// Show the current menu
			this.CurrentMenu = menuController;
			this.CurrentMenu.gameObject.SetActive(true);
		}

		/// <summary>
		/// Loads the level.
		/// </summary>
		/// 
		/// <param name="levelName">Name of the level.</param>
		public void LoadLevel(string levelName)
		{
			SceneManager.LoadScene(levelName, LoadSceneMode.Single);
		}
		#endregion
	}
}