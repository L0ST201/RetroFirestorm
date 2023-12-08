using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private Menu[] menus;

    void Awake()
    {
        Instance = this;
    }

    // Method to open a menu by its name
    public void OpenMenu(string menuName)
    {
        foreach (Menu menu in menus)
        {
            if (menu.MenuName == menuName)
            {
                menu.Open();
            }
            else if (menu.IsOpen)
            {
                CloseMenu(menu);
            }
        }
    }

    // Method to open a specific Menu object
    public void OpenMenu(Menu menu)
    {
        foreach (Menu existingMenu in menus)
        {
            if (existingMenu.IsOpen)
            {
                CloseMenu(existingMenu);
            }
        }
        menu.Open();
    }

    // Method to close a specific Menu object
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
