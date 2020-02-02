using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public float maxTime;
    public TextMeshProUGUI timerLabel;
    public Image timerImage;
    public TextMeshProUGUI player1ScoreLabel;
    public TextMeshProUGUI player2ScoreLabel;

    public bool GameOver { get { return gameOver; } }

    [ShowOnly]
    public float timer;
    [ShowOnly]
    public int player1Score;
    [ShowOnly]
    public int player2Score;
    [ShowOnly]
    public float refreshTimer;
    private bool gameOver;

    public void Awake()
    {
        Instance = this;
        gameOver = false;
    }

    public void AddPlayer1Score(int score)
    {
        player1Score += score;
    }

    public void AddPlayer2Score(int score)
    {
        player2Score += score;
    }

    public void Update()
    {
        if (gameOver) return;

        refreshTimer += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            timer = maxTime;
            refreshTimer = 0.5f; //forceRefresh;
            gameOver = true;
        }

        if (refreshTimer > 0.1f)
        {
            refreshTimer = 0f;
            timerLabel.text = (maxTime - timer).ToString("0");
            timerImage.fillAmount = timer / maxTime;

            player1ScoreLabel.text = player1Score.ToString("0");
            player2ScoreLabel.text = player2Score.ToString("0");
        }
    }
}