using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    private static bool isMusicActive = true;

    // List to store the names of scenes where music should not play
    public List<string> nonPlayingScenes = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Prevent the music object from being destroyed on load

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        foreach (var obj in musicObj)
        {
            if (obj != this.gameObject)
            {
                Destroy(obj); // Destroy other music objects
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe when the object is destroyed
    }

    // Method called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (nonPlayingScenes.Contains(scene.name))
        {
            this.gameObject.SetActive(false);
            isMusicActive = false;
        }
        else
        {
            // Activate music if it's not in the non-playing list
            this.gameObject.SetActive(true);
            isMusicActive = true;
        }
    }

    // Call this method to toggle music state
    public void ToggleActiveState()
    {
        isMusicActive = !isMusicActive;
        this.gameObject.SetActive(isMusicActive);
    }
}
