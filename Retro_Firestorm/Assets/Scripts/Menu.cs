using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // For UnityEvent

public class Menu : MonoBehaviour
{
    [SerializeField] private string menuName; // Serialized to allow setting in the Inspector
    private bool isOpen;

    // Unity events for external listeners
    public UnityEvent onOpen;
    public UnityEvent onClose;

    // Public property for isOpen with encapsulation
    public bool IsOpen
    {
        get { return isOpen; }
        private set { isOpen = value; }
    }

    // Public property for menuName with encapsulation
    public string MenuName
    {
        get { return menuName; }
        set { menuName = value; }
    }

    // Method to open the menu
    public void Open()
    {
        if (IsOpen) return; // Prevent opening if it's already open

        IsOpen = true;
        gameObject.SetActive(true);
        onOpen?.Invoke(); // Invoke the open event
    }

    // Method to close the menu
    public void Close()
    {
        if (!IsOpen) return; // Prevent closing if it's already closed

        IsOpen = false;
        gameObject.SetActive(false);
        onClose?.Invoke(); // Invoke the close event
    }
}
