// ================================================================================================================================
// File:        PaddleMovement.cs
// Description:	Tracks whether or not the paddle is moving, and in which direction
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public enum Direction
{
    None = 0,
    Left = 1,
    Right = 2
}

public class PaddleMovement : MonoBehaviour
{
    public Direction Movement = Direction.None; //Which direction the paddle is currently moving
    private Vector3 PreviousPosition;   //Previous frames position, compared with current to find movement direction

    private void Awake()
    {
        //Store initial position value
        PreviousPosition = transform.position;
    }

    private void Update()
    {
        TrackMovement();
    }

    //Tracks the paddles movement
    private void TrackMovement()
    {
        bool IsMoving = transform.position != PreviousPosition;

        if (!IsMoving)
            Movement = Direction.None;
        else
            Movement = transform.position.x > PreviousPosition.x ? Direction.Right : Direction.Left;

        PreviousPosition = transform.position;
    }
}
