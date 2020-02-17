// ================================================================================================================================
// File:        PaddleControls.cs
// Description:	Allows the player to move the paddle around during gameplay
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    //Only either keyboard or mouse movement mode will be active at any one time
    private bool KeyboardModeActive = true;

    //Movement Speed Variables
    public float KeyboardMoveSpeed = 10f;       //How fast the paddle moves when controlled by the keyboard
    public float MouseCursorMoveSpeed = 25f;    //How fast the paddle moves when controlled by the mouse cursor

    //Positional Limitations
    private float MaxXPos = 6.25f;  //Furthest distance the paddle may travel towards the right
    private float MinXPos = -6.25f; //Furthest distance the paddle may travel towards the left

    //Track difference in mouse cursor position over time
    private Vector3 PreviousMousePosition = new Vector3(1000, 1000, 1000);

    private void Awake()
    {
        //Get the mouse cursors starting position to store
        PreviousMousePosition = GetCursorPosition();
    }

    private void Update()
    {
        //Manage changing between keyboard and mouse input modes
        ManageControlMode();

        //Move the paddle based on the current control mode that is active
        if (KeyboardModeActive)
            MovePaddleWithKeyboard();
        else
            MousePaddleWithMouseCursor();

        //Store the mouse cursors position for next frame
        PreviousMousePosition = GetCursorPosition();
    }

    //Moves the paddle using keyboard input
    private void MovePaddleWithKeyboard()
    {
        //Read keyboard input for moving the paddle
        bool MoveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool MoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        //Get a new target position for the paddle based on this keyboard input
        Vector2 TargetPos = transform.position;
        TargetPos.x += MoveRight ? KeyboardMoveSpeed : MoveLeft ? -KeyboardMoveSpeed : 0f;

        //Keep the paddle inside the boundaries
        TargetPos.x = Mathf.Clamp(TargetPos.x, MinXPos, MaxXPos);

        //Move the paddle to its new location
        transform.position = Vector3.Lerp(transform.position, TargetPos, KeyboardMoveSpeed * Time.deltaTime);
    }

    //Moves the paddle using mouse cursor input
    private void MousePaddleWithMouseCursor()
    {
        //Target position for the paddle should have it matching the x pos of the cursor
        Vector3 CursorPos = GetCursorPosition();
        Vector3 TargetPos = new Vector3(CursorPos.x, transform.position.y, transform.position.z);

        //Keep it inside the limits
        TargetPos.x = Mathf.Clamp(TargetPos.x, MinXPos, MaxXPos);

        //Move to the new location
        transform.position = Vector3.Lerp(transform.position, TargetPos, MouseCursorMoveSpeed * Time.deltaTime);
    }

    //Detects keyboard and mouse inputs in order to swap between keyboard and mouse control
    private void ManageControlMode()
    {
        //If any keyboard input is detected during mouse input mode, then keyboard control mode is activated
        if(!KeyboardModeActive)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                KeyboardModeActive = true;
                return;
            }
        }

        //If any mouse cursor movement is detected during keyboard input mode, then mouse control mode is activated
        if(KeyboardModeActive)
        {
            if(GetCursorPosition() != PreviousMousePosition)
            {
                KeyboardModeActive = false;
                return;
            }
        }
    }
    
    //Send a raycast to find the mouse cursors position inside the world
    private Vector3 GetCursorPosition()
    {
        LayerMask BackgroundLayer = 1 << LayerMask.NameToLayer("Backdrop");
        RaycastHit RayHit;
        Ray CursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(CursorRay, out RayHit, Mathf.Infinity, BackgroundLayer))
            return RayHit.point;

        return Vector3.zero;
    }
}
