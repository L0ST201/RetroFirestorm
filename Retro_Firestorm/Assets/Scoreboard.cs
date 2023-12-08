using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject scoreboardItemPrefab;
    [SerializeField] private CanvasGroup canvasGroup;

    private Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();

    void Start()
    {
        if (container == null || scoreboardItemPrefab == null || canvasGroup == null)
        {
            Debug.LogError("Scoreboard: Required components are not assigned.");
            return;
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    void AddScoreboardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
        if (item != null)
        {
            item.Initialize(player);
            scoreboardItems[player] = item;
        }
        else
        {
            Debug.LogError("ScoreboardItem component is missing in the prefab.");
        }
    }

    void RemoveScoreboardItem(Player player)
    {
        if (scoreboardItems.TryGetValue(player, out ScoreboardItem item))
        {
            Destroy(item.gameObject);
            scoreboardItems.Remove(player);
        }
    }

    void Update()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = Input.GetKey(KeyCode.Tab) ? 1 : 0;
        }
    }
}