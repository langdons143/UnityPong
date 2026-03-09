using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour, ICollidable
{
    // Private Fields? :)
    private float speed = 10f;
    private float maxSpeed = 10f;
    private Vector2 direction;
    private Rigidbody2D rb;

    // Public properties - controlled access woah :O
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > maxSpeed)
            {
                speed = maxSpeed;
            }
            else if (value < 0)
            {
                speed = 0;
            }
            else
            {
                speed = value;
            }
        }
    }

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value.normalized; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(1f, 1f);
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    // Behavior when the ball collides with something!!
    void OnCollisionEnter2D(Collision2D collision)
    {
        ICollidable collidable =
            collision.gameObject.GetComponent<ICollidable>();

        if (collidable != null)
        {
            collidable.OnHit(collision);
        }

        OnHit(collision);

    }

    public void OnHit(Collision2D collision)
        {
        // Handle collision with paddles and walls
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Reflect the ball's direction based on the paddle's movement
            Vector2 newDirection = new Vector2(-direction.x, direction.y);
            Direction = newDirection;
        }
        else if (collision.gameObject.CompareTag("Wall Vertical"))
        {
            // Reverse vertical direction
            Vector2 newDirection = new Vector2(direction.x, -direction.y);
            Direction = newDirection;
        }
        else if (collision.gameObject.CompareTag("Wall Horizontal"))
        {
            // Reverse horizontal direction
            Vector2 newDirection = new Vector2(-direction.x, direction.y);
            Direction = newDirection;
        }
    }

}