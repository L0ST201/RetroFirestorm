using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    public TMP_Text usernameText;
    public TMP_Text killsText;
    public TMP_Text deathsText;

    private Player player;

    public void Initialize(Player player)
    {
        this.player = player;

        if (usernameText == null || killsText == null || deathsText == null)
        {
            Debug.LogError("ScoreboardItem: Text components are not assigned.");
            return;
        }

        usernameText.text = player.NickName;
        UpdateStats();
    }

    void UpdateStats()
    {
        killsText.text = player.CustomProperties.TryGetValue("kills", out object kills) ? kills.ToString() : "0";
        deathsText.text = player.CustomProperties.TryGetValue("deaths", out object deaths) ? deaths.ToString() : "0";
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            UpdateStats();
        }
    }
}