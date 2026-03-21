using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class GameManager : NetworkBehaviour
{
    private NetworkVariable<int> RightScore = new NetworkVariable<int>(0);
    private NetworkVariable<int> LeftScore = new NetworkVariable<int>(0);

    private NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(false);
    private NetworkVariable<bool> gameOver = new NetworkVariable<bool>(false);
    private int scoreToWin = 5;

    void Start()
    {
        if (IsServer)
        {
            ResetBall(true); // puts ball in center but DOESN’T launch
        }
    }

    // Added last, but this is the function to start the game and reset everything to default values
    public void StartGame()
    {
        if (IsServer)
        {
            // Reset scores
            LeftScore.Value = 0;
            RightScore.Value = 0;

            // Reset game state
            gameOver.Value = false;
            gameStarted.Value = true;

            // Reset and launch ball
            ResetBall(true);

            Debug.Log("Game started!");
        }
    }

    // BALL RESET AND RELAUNCH :DDDD
    void ResetBall(bool leftPlayerScored)
    {
        // Only the server should control the ball's position and velocity
        if (!IsServer) return;

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball != null)
        {
            // Reset position to center
            ball.transform.position = new Vector3(239.0859f, 128.4238f, 0f);

            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            // Stop movement
            rb.velocity = Vector2.zero;

            if (!gameStarted.Value)
            {
                return;
            }

            // Send ball toward the player who scored
            if (leftPlayerScored)
            {
                // toward left player
                rb.velocity = new Vector2(-5f, 0f); 
            }
            else
            {
                // toward RIGHT player
                rb.velocity = new Vector2(5f, 0f);
            }
        }
    }

    // Left player scores start
    public void AddLeftScore()
    {
        if (IsServer && gameStarted.Value && !gameOver.Value)
        {
            // Only process scoring if race isn't finished
            LeftScore.Value++;
            Debug.Log("Player 1 scored: " + LeftScore.Value);

            // This is waht it says
            CheckWinCondition();
            // left player scores so ball goes right
            ResetBall(true);
        }
    }

    // right player scores start
    public void AddRightScore()
    {
        if (IsServer && gameStarted.Value && !gameOver.Value)
        {
            RightScore.Value++;
            Debug.Log("Player 2 scored: " + RightScore.Value);

            // Yeah this is what it says again lol
            CheckWinCondition();
            // right player scores so ball goes left
            ResetBall(false);

        }
    }

    public int GetLeftScore()
    {
        return LeftScore.Value;
    }

    public int GetRightScore()
    {
        return RightScore.Value;
    }

    // Check if either player has reached the score to win
    void CheckWinCondition()
    {
        if (LeftScore.Value >= scoreToWin)
        {
            gameOver.Value = true;
            gameStarted.Value = false;
            Debug.Log("Player 1 Wins!");
        }
        else if (RightScore.Value >= scoreToWin)
        {
            gameOver.Value = true;
            gameStarted.Value = false;
            Debug.Log("Player 2 Wins!");
        }
    }

    // Check if the game is started
    public bool IsGameStarted()
    {
        return gameStarted.Value;
    }

    // Check if the game is over
    public bool IsGameOver()
    {
        return gameOver.Value;
    }
}