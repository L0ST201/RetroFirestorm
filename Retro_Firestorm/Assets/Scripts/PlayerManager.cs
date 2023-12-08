using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;

    public int Kills { get; private set; }
    public int Deaths { get; private set; }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PV == null)
        {
            Debug.LogError("PhotonView not found on the player object!");
        }
    }

    void Start()
    {
        if (PV != null && PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        if (spawnpoint == null)
        {
            Debug.LogError("Spawnpoint not found!");
            return;
        }

        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        if (controller != null)
        {
            PhotonNetwork.Destroy(controller);
            CreateController();
        }

        UpdateDeathCount();
    }

    void UpdateDeathCount()
    {
        Deaths++;
        UpdatePlayerProperties("deaths", Deaths);
    }

    public void GetKill()
    {
        PV.RPC(nameof(RPC_GetKill), PV.Owner);
    }

    [PunRPC]
    void RPC_GetKill()
    {
        Kills++;
        UpdatePlayerProperties("kills", Kills);
    }

    void UpdatePlayerProperties(string key, int value)
    {
        Hashtable hash = new Hashtable();
        hash.Add(key, value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV != null && x.PV.Owner == player);
    }
}
