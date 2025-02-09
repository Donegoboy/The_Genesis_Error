using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreAnimationDuration = 0.5f;

    private int currentScore = 0;
    private int targetScore = 0;

    private void Awake()
    {   
            Instance = this;      
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        targetScore += amount;
        StartCoroutine(AnimateScore());
    }

    private IEnumerator AnimateScore()
    {
        int startScore = currentScore;
        float timer = 0;

        while (timer < scoreAnimationDuration)
        {
            timer += Time.deltaTime;
            float fraction = timer / scoreAnimationDuration;

            currentScore = (int)Mathf.Lerp(startScore, targetScore, fraction);
            UpdateScoreText();

            yield return null;
        }
        currentScore = targetScore;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }
}
