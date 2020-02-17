// ================================================================================================================================
// File:        MenuButtonInteraction.cs
// Description:	Alerts the buttons when they are being moused over and interacted with by the player
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonInteraction : MonoBehaviour
{
    private Camera MenuCamera;  //Used for raycasting to see what buttons the mouse is hovered over
    private GameObject ActiveButton = null; //If the mouse is currently hovering over a menu button its stored here

    private void Awake()
    {
        //Store reference to the menu camera object
        MenuCamera = Camera.main;
    }

    private void Update()
    {
        HighlightButtons();
        InteractButtons();
    }

    //Highlights whatever button the mouse is currently hovering over
    private void HighlightButtons()
    {
        //Cast a ray from the camera to the mouse position to keep track of menu buttons being moused over
        RaycastHit RayHit;
        Ray MouseRay = MenuCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(MouseRay, out RayHit))
        {
            //If there was no active button, set this to be it
            if (ActiveButton == null)
            {
                ActiveButton = RayHit.transform.gameObject;
                ActiveButton.GetComponent<MenuButtonHighlight>().SetMouseOn();
            }
            //If there was a change in the active button update it
            else if (ActiveButton != RayHit.transform.gameObject)
            {
                ActiveButton.GetComponent<MenuButtonHighlight>().SetMouseOff();
                ActiveButton = RayHit.transform.gameObject;
                ActiveButton.GetComponent<MenuButtonHighlight>().SetMouseOn();
            }
        }
        //Deactivate any previously active menu button once the mouse moves away
        else if (ActiveButton != null)
        {
            ActiveButton.GetComponent<MenuButtonHighlight>().SetMouseOff();
            ActiveButton = null;
        }
    }

    //Loads different scene when player clicks on specific button
    private void InteractButtons()
    {
        GameObject ScoreSaver = GameObject.Find("ScoreSaver");
        if (ScoreSaver != null)
            GameObject.Destroy(ScoreSaver);

        if (Input.GetMouseButtonDown(0))
        {
            if (ActiveButton.transform.name == "Play HitBox")
                SceneManager.LoadScene(1);
        }
    }
}
