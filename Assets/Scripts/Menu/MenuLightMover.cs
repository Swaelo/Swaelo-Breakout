// ================================================================================================================================
// File:        MenuLightMover.cs
// Description:	Moves the point light back and forth during the main menu to create some effect on the letters
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class MenuLightMover : MonoBehaviour
{
    public float MinimumXPosition = -1f;   //Left most position the light will move to
    public float MaximumXPosition = .5f;   //Right most position the light will move to
    private bool MovingRight = true;        //Which direction the light is currently moving
    public float MoveSpeed = 3.5f;          //How fast the light moves

    private void Update()
    {
        //Find a new position for the light after moving in its current direction
        Vector3 NewLightPosition = transform.position;
        NewLightPosition.x += MovingRight ? MoveSpeed : -MoveSpeed;

        //Move the light towards this new target position
        transform.position = Vector3.Lerp(transform.position, NewLightPosition, MoveSpeed * Time.deltaTime);

        //Change the movement direction if the light reaches its movement limitations
        if ((MovingRight && transform.position.x >= MaximumXPosition)
            || (!MovingRight && transform.position.x <= MinimumXPosition))
            MovingRight = !MovingRight;
    }
}
