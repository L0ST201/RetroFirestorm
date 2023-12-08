using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomNameText; // Renamed for clarity

    public RoomInfo RoomInfo { get; private set; }

    /// <summary>
    /// Sets up the room list item with the provided RoomInfo.
    /// </summary>
    /// <param name="_info">Information about the room.</param>
    public void SetUp(RoomInfo roomInfo)
    {
        if (roomInfo == null)
        {
            Debug.LogError("RoomInfo is null.");
            return;
        }

        RoomInfo = roomInfo;

        if (roomNameText != null)
        {
            roomNameText.text = roomInfo.Name;
        }
        else
        {
            Debug.LogError("RoomNameText is not assigned in the inspector.");
        }
    }

    /// <summary>
    /// Called when this room list item is clicked.
    /// </summary>
    public void OnClick()
    {
        if (Launcher.Instance == null)
        {
            Debug.LogError("Launcher instance is null.");
            return;
        }

        Launcher.Instance.JoinRoom(RoomInfo);
    }
}
