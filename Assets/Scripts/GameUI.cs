using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public UnityEngine.UI.Button startButton;
    public TextMeshProUGUI LeftScoreText;
    public TextMeshProUGUI RightScoreText;
    public TextMeshProUGUI winText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        winText.gameObject.SetActive(false);

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(OnStartButtonClicked);

    }

    void OnStartButtonClicked()
    {
        if (gameManager != null && gameManager.IsServer)
        {
            gameManager.StartGame();
            startButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            return;
        }

        if (gameManager.IsServer)
        {
            if (!gameManager.IsGameStarted() || gameManager.IsGameOver())
            {
                startButton.gameObject.SetActive(true);
            }
            else
            {
                startButton.gameObject.SetActive(false);
            }
        }
        else
        {
            startButton.gameObject.SetActive(false);
        }

        // Read NetworkVariable values and update UI
        LeftScoreText.text = "Player 2: " + gameManager.GetLeftScore();
        RightScoreText.text = "Player 1: " + gameManager.GetRightScore();

        // Check if game is over and update win text and button visibility
        if (gameManager.IsGameOver())
        {
            winText.gameObject.SetActive(true);

            // Only server gets restart button
            if (gameManager.IsServer)
            {
                startButton.gameObject.SetActive(true);
            }

            if (gameManager.GetLeftScore() >= 5)
            {
                winText.text = "Player 2 Wins!";
            }
            else
            {
                winText.text = "Player 1 Wins!";
            }
        }
        else
        {
            winText.gameObject.SetActive(false);
        }
    }
}