using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    [Header("References")]
    public TMP_Text pointsTextUI;
    public Text bestScoreMainMenu;
    public AudioSource gamePointAudioSource;
    public AudioClip gamePointSound;
    public AudioClip failSound;


    [Header("Settings")]
    public int score = 0;
    public int bestScore = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        pointsTextUI.text = score.ToString();
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreMainMenu.text = $"Best Score: {bestScore}";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddPoint()
    {
        score++;
        gamePointAudioSource.PlayOneShot(gamePointSound);
        pointsTextUI.text = score.ToString();
    }
    public void FailHandle()
    {
        gamePointAudioSource.PlayOneShot(failSound);

    }
    public int GetCurrentScore()
    {
        return score;
    }
    public int GetBestScore()
    {
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        return bestScore;
    }
    public void ResetPoints()
    {
        score = 0;
        pointsTextUI.text = score.ToString();
        bestScoreMainMenu.text = $"Best Score: {GetBestScore()}";
    }
}
