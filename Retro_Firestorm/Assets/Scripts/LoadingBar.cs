using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private float loadingDuration = 5.0f;

    private float elapsedTime = 0.0f;
    private bool isLoading = false;

    // Start loading and optionally specify a scene or menu to load
    public void StartLoading(string target = null)
    {
        isLoading = true;
        elapsedTime = 0.0f;
        gameObject.SetActive(true);
        StartCoroutine(LoadingCoroutine(target));
    }

    private IEnumerator LoadingCoroutine(string target)
    {
        while (isLoading)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = elapsedTime / loadingDuration;

            if (elapsedTime >= loadingDuration)
            {
                FinishLoading(target);
            }

            yield return null;
        }
    }

    private void FinishLoading(string target)
    {
        isLoading = false;
        gameObject.SetActive(false);

        // Check if a scene needs to be loaded
        if (!string.IsNullOrEmpty(target))
        {
            // Check if target is a scene or a menu
            if (target == "GameScene") // replace "GameScene" with your game scene's name
            {
                SceneManager.LoadScene(target);
            }
            else
            {
                // Open a menu within the current scene
                if (MenuManager.Instance != null)
                {
                    MenuManager.Instance.OpenMenu(target);
                }
                else
                {
                    Debug.LogError("LoadingBar: MenuManager instance not found.");
                }
            }
        }
    }
}
