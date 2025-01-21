using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    [SerializeField] private string levelScene = "SampleScene";
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject levelCompletionPanel;
    [SerializeField] private string levelSelection = "LevelSelection";

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void LevelCompleted()
    {
        Debug.Log("LevelCompleted() called - about to activate panel");

        levelCompletionPanel.SetActive(true); // Activate the panel

        Debug.Log("LevelCompleted() - panel should be active");

        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
        isPaused = false;
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ToLevelSelection() 
    {
        SceneManager.LoadScene(levelSelection);
    }

    public void toLevel()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
