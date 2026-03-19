using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public abstract class PaddleController : NetworkBehaviour
{
    protected float speed = 25f;
    protected Rigidbody2D rb;

    // Network variable to sync the Y position of the paddle across the network
    private NetworkVariable<float> syncedYPosition = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsOwner)
        {
            float input;

            // Host using WASD
            if (OwnerClientId == 0)
            {
                input = Input.GetAxis("Player1Vertical");
            }
            // Client using arrow
            else
            {
                input = Input.GetAxis("Player2Vertical");
            }

            // Calculate new Y position based on input
            float newY = transform.position.y + (input * speed * Time.deltaTime);

            // Move instantly on owner screen
            transform.position = new Vector3(transform.position.x, newY, 0);

            // Sync to network
            syncedYPosition.Value = newY;
        }
        else
        {
            // Non-owners follow synced value
            transform.position = new Vector3(transform.position.x, syncedYPosition.Value, 0);
        }
    }

    // Had to do research on moving over the paddle??
    // I found that the best way to do this was to use a NetworkVariable to sync the Y position of the paddle across the network.
    // This way, the owner can move the paddle instantly on their screen, and the non-owners will follow the synced value.
    // I also set the initial position of the paddle based on whether it's the host or client in the OnNetworkSpawn method.
    // This ensures that both players start on opposite sides of the screen.
    // WHICH WAS GIVING ME SO MUCH TROUBLE BEFORE
    public override void OnNetworkSpawn()
    {
        if (OwnerClientId == 0)
        {
            // Left side
            transform.position = new Vector3(209.2743f, 128.4238f, 0f);
        }
        else
        {
            // Right side
            transform.position = new Vector3(268.8976f, 128.4238f, 0f);
        }

        // Only owner sets the synced Y value
        if (IsOwner)
        {
            syncedYPosition.Value = transform.position.y;
        }
    }

    protected abstract float GetMovementInput();

}