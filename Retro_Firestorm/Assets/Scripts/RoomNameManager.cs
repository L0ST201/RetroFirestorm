using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomNameInput;

    void Start()
    {
        InitializeRoomName();
    }

    private void InitializeRoomName()
    {
        string defaultRoomName = "Room " + Random.Range(0, 10000).ToString("0000");
        roomNameInput.text = PlayerPrefs.GetString("roomName", defaultRoomName);
    }

    public void OnRoomNameInputChanged()
    {
        if (roomNameInput != null)
        {
            PlayerPrefs.SetString("roomName", roomNameInput.text);
        }
        else
        {
            Debug.LogError("RoomNameInput is null.");
        }
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput?.text))
        {
            PhotonNetwork.CreateRoom(roomNameInput.text);
        }
        else
        {
            Debug.LogError("Room name is empty or the input field is null.");
        }
    }
}
