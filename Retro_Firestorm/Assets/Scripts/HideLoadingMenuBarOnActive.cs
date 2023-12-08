using UnityEngine;

public class HideLoadingMenuBarOnActive : MonoBehaviour
{
    public GameObject loadingMenuBar; // Assign the LoadingMenuBar GameObject in the inspector

    void OnEnable()
    {
        // When the TitleMenu is enabled, hide the LoadingMenuBar
        if (loadingMenuBar != null)
        {
            loadingMenuBar.SetActive(false);
        }
    }
}
