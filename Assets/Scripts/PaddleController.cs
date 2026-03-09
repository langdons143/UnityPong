using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public abstract class PaddleController : NetworkBehaviour
{
    protected float speed = 25f;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float input = 0f;   

    void FixedUpdate()
    {
        input = GetMovementInput();
        rb.velocity = new Vector2(0, input * speed);
    }

    private NetworkVariable<float> yPosition = new NetworkVariable<float>(0f);

    void Update()
    {
        // Only the owner can control this car
        if (IsOwner)
        {
            float input = Input.GetAxis("Player1Vertical");
            float newY = transform.position.y + (input * 10f * Time.deltaTime);

            rb.velocity = new Vector2(0, input * speed);


            yPosition.Value = newY;
        }
     
    }
    
    protected abstract float GetMovementInput();

}