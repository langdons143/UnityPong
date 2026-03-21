using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        // If ball touches the zone, update the score AND reset ball
        if (other.CompareTag("Ball"))
        {
            GameManager manager = FindObjectOfType<GameManager>();

            // Determine which score zone was hit and update the score accordingly
            if (gameObject.CompareTag("LeftScoreZone"))
            {
                manager.AddRightScore();
            }
            else if (gameObject.CompareTag("RightScoreZone"))
            {
                manager.AddLeftScore();
            }
        }
    }
}