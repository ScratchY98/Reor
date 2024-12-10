using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private GameObject levelWindow;

    public void Start()
    {
        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Button to open the Setting Window.
    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    // Button to Close the Settings Window.
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    // Button to open the Setting Window.
    public void playButton()
    {
        levelWindow.SetActive(true);
    }

    // Button to Close the Settings Window.
    public void CloseLevelWindow()
    {
        levelWindow.SetActive(false);
    }

    // Button to exit
    public void QuitGame()
    {
        Application.Quit();
    }


    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Coroutine pour charger la scène asynchrone
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        Debug.Log("Scène chargée avec succès !");
    }
}