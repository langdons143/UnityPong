using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightPaddleController : PaddleController, ICollidable
{
    protected override float GetMovementInput()
    {
        
        return Input.GetAxis("Player2Vertical");

    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Ball was hit!");
        // Ball specific collision response
    }

}