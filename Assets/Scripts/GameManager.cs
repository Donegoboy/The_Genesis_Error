using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    [SerializeField] private string levelScene = "SampleScene";
    [SerializeField] private string levelSelection = "LevelSelection";
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject levelCompletionPanel;
    [SerializeField] private GameObject onDeathPanel;

    private bool isPaused = false;
    private string levelName;

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


    public void onDeath() 
    {
        onDeathPanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Retry() 
    {
        Time.timeScale = 1f; // Unpause the game if it was paused
        isPaused = false; // reset the flag

        // Check if we have stored the current level name
        if (!string.IsNullOrEmpty(levelName))
        {
            // Reload the current level using its name
            SceneManager.LoadScene(levelName);
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

    public void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
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
        Time.timeScale = 1f;
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
