using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeftPaddleController : PaddleController, ICollidable
{
    protected override float GetMovementInput()
    {
        
        return Input.GetAxis("Player1Vertical"); 

    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Ball was hit!");
        // Ball specific collision response
    }
}