using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public float maxTime;
    public TextMeshProUGUI timerLabel;
    public Image timerImage;
    public TextMeshProUGUI player1ScoreLabel;
    public TextMeshProUGUI player2ScoreLabel;

    public GameObject endOfGamePanel;
    public TextMeshProUGUI endOfGameText;

    public JoyconDemo player1;
    public JoyconDemo player2;
    public GameObject pow;

    private bool gameOver;
    public bool GameOver { get { return gameOver; } }

    [ShowOnly]
    public float timer;
    [ShowOnly]
    public int player1Score;
    [ShowOnly]
    public int player2Score;
    [ShowOnly]
    public float refreshTimer;

    public void Awake()
    {
        Instance = this;
        gameOver = false;

        endOfGamePanel.SetActive(false);

        AudioManager.Instance.PlayIngame();

        Application.targetFrameRate = 60;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void AddPlayer1Score(int score)
    {
        player1Score += score;
        AudioManager.Instance.PlayHitPink();
    }

    public void AddPlayer2Score(int score)
    {
        player2Score += score;
        AudioManager.Instance.PlayHitGreen();
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

            endOfGamePanel.SetActive(true);
            bool player1Win = player1Score > player2Score;
            bool player2Win = player2Score > player1Score;
            var winnerText = player1Win ? " Green!" : player2Win ? " Pink!" : "Draw?";
            endOfGameText.text = string.Format(endOfGameText.text, winnerText);

            player1.GameOver(player1Win);
            player2.GameOver(player2Win);

            AudioManager.Instance.PlayEndOfGame();
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