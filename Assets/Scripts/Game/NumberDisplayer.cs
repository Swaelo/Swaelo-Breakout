// ================================================================================================================================
// File:        NumberDisplayer.cs
// Description:	Hides and Unhides its children to display only the number desired
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class NumberDisplayer : MonoBehaviour
{
    //Objects to hide and unhide
    public GameObject[] NumberObjects;

    //Hides all the numbers
    public void DisplayNone()
    {
        foreach (GameObject Number in NumberObjects)
            Number.SetActive(false);
    }

    //Hides all but 1 of the numbers
    public void DisplayNumber(int NumberToDisplay)
    {
        for (int i = 0; i < 10; i++)
            NumberObjects[i].SetActive(i == NumberToDisplay);
    }
}
