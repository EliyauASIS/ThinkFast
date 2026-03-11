using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("References")]
    public GameObject gameOverPanel;
    public Text currentScoreTextGameOver;
    public Text bestScoreTextGameOver;

    [Header("Settings")]

    public float mainSizeReductionduration = 3f;
    public float minDuration = 0.5f;
    public float reductionPerSecond = 0.01f; // מוריד כל שנייה
    public bool gameRunning = false;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (!gameRunning) { return; }
        mainSizeReductionduration = Mathf.Max(minDuration, mainSizeReductionduration - reductionPerSecond * Time.deltaTime);
    }
    public void SetGameRunning()
    {
        gameRunning = true;
    }
    public void GameOver()
    {
        gameRunning = false;
        PointsManager pointsManager = PointsManager.instance;
        pointsManager.FailHandle();
        int currentScore = pointsManager.GetCurrentScore();
        int bestScore = pointsManager.GetBestScore();
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("Best Score", currentScore);
        }
        bestScore = pointsManager.GetBestScore();
        bestScoreTextGameOver.text = $"Best Score: {bestScore}";
        currentScoreTextGameOver.text = currentScore.ToString();
        gameOverPanel.SetActive(true);
    }
    public void ResetGame()
    {
        mainSizeReductionduration = 7;
        PointsManager.instance.ResetPoints();
    }
}