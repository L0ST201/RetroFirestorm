using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text text; // Reference to the TextMeshPro component
    private Player player;

    // Setup the list item with player information
    public void SetUp(Player _player)
    {
        if (_player == null)
        {
            Debug.LogError("PlayerListItem: SetUp called with a null player.");
            return;
        }

        player = _player;
        text.text = _player.NickName; // Set the player's name on the UI
    }

    // Called when another player leaves the room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Debug.Log($"PlayerListItem: Player {otherPlayer.NickName} left the room, destroying list item.");
            Destroy(gameObject);
        }
    }

    // Called when the local player leaves the room
    public override void OnLeftRoom()
    {
        Debug.Log("PlayerListItem: Local player left the room, destroying list item.");
        Destroy(gameObject);
    }

    // Unsubscribe from callbacks when the object is destroyed
    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
