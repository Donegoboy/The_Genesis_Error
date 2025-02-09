using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;



public class GameManager : MonoBehaviour
{
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string titleScreenScene = "TitleScreen";
    [SerializeField] private string levelScene = "Level1";
    [SerializeField] private string levelScene2 = "Level2";
    [SerializeField] private string levelScene3 = "Level3";
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

    private string currentSceneName;

    public void LoadScene(string loadSceneName, string unloadSceneName = null)
    {
        if (unloadSceneName != null || currentSceneName != null)

            SceneManager.UnloadSceneAsync(unloadSceneName != null ? unloadSceneName : currentSceneName);

        SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);

        currentSceneName = loadSceneName;
    }


    public void onDeath()
    {
        onDeathPanel.SetActive(true);

        Time.timeScale = 1;

        isPaused = true;
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        isPaused = false;

        if (!string.IsNullOrEmpty(levelName))
        {
            SceneManager.LoadScene(levelName);
        }

    }
    public void LevelCompleted()
    {
        levelCompletionPanel.SetActive(true);
        Time.timeScale = 1f;
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
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }

    public void ToTitleScreen()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(titleScreenScene);
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
    public void toLevel2()
    {
        SceneManager.LoadScene(levelScene2);

    }

    public void toLevel3()
    {
        SceneManager.LoadScene(levelScene3);
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