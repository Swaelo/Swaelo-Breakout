// ================================================================================================================================
// File:        GamePlayController.cs
// Description:	Manages gameplay state, tracks lives
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class LivesTracker : MonoBehaviour
{
    //Singleton instance
    public static LivesTracker Instance;
    private void Awake() { Instance = this; }

    //Players remaining balls and current score values
    public int BallsRemaining = 3;

    //Objects for displaying how many balls the player has remaining
    public GameObject ThreeBallsDisplay;
    public GameObject TwoBallsDisplay;
    public GameObject OneBallsDisplay;

    private void Start()
    {
        //Hide the Two/One ball display numbers
        TwoBallsDisplay.SetActive(false);
        OneBallsDisplay.SetActive(false);
    }

    public void UpdateBallsDisplay()
    {
        ThreeBallsDisplay.SetActive(BallsRemaining == 3);
        TwoBallsDisplay.SetActive(BallsRemaining == 2);
        OneBallsDisplay.SetActive(BallsRemaining == 1);
    }
}
