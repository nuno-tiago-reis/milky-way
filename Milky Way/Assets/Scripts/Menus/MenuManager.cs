using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public Menu currentMenu;

	public void Start () {

        showMenu(currentMenu);
	}

    public void showMenu(Menu menu) {

        if (currentMenu != null)
            currentMenu.isOpen = true;

        currentMenu.isOpen = false;

        currentMenu = menu;
        currentMenu.isOpen = true;
    }
}
