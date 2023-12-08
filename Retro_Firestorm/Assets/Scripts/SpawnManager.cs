using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private Spawnpoint[] spawnpoints;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        spawnpoints = GetComponentsInChildren<Spawnpoint>();

        if (spawnpoints == null || spawnpoints.Length == 0)
        {
            Debug.LogError("No spawnpoints found for the SpawnManager.");
        }
    }

    public Transform GetSpawnpoint()
    {
        if (spawnpoints == null || spawnpoints.Length == 0)
        {
            Debug.LogError("Spawnpoints array is empty or null.");
            return null;
        }

        return spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
    }
}
