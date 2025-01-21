using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    [SerializeField] private string levelScene = "SampleScene";
    //[SerializeField] private string[] levelScene = "SampleScene"; -> ako imam vise levela

    public void Pause(bool pause)
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void ToMainMeun()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void toLevel()
    {
        SceneManager.LoadScene(levelScene);
    }

    // public void toLevel(int levelindex)
    //{
    //  SceneManager.LoadScene(levelScene);   --ako imam vise levela.
    //}


    public void Quit()
    {
#if UNITY_EDITOR    //ovo mi sluzi da demostriram pravi quit u unity-u, jer inace se nece nista dogoditi aok ostavim samo application.quit().
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }
}
