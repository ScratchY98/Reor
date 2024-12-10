using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Script and components's References")]
    [SerializeField] private MirrorManager mirrorManager;
    [SerializeField] PlayerInput playerInput;

    [Header("UI")]
    [SerializeField] private static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsWindow;

    void Update()
    {
        if (playerInput.actions["PauseMenu"].WasPerformedThisFrame())
            PauseMenuButton();
    }

    public void PauseMenuButton()
    {
        if (gameIsPaused)
            Resume();
        else
            Paused();
    }

    void Paused()
    {
        mirrorManager.canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        mirrorManager.canMove = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        pauseMenuUI.SetActive(false);

        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("Main Menu");
    }
}