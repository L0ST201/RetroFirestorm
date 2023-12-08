using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject graphics;

    void Awake()
    {
        if (graphics != null)
        {
            graphics.SetActive(false);
        }
        else
        {
            Debug.LogError("Spawnpoint graphics not assigned in the inspector.");
        }
    }
}