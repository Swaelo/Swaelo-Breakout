// ================================================================================================================================
// File:        DisplayFinalScore.cs
// Description:	Loads and views the final score when entering into the game over scene
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections.Generic;
using UnityEngine;

public class DisplayFinalScore : MonoBehaviour
{
    public NumberDisplayer ThousandsDisplay;
    public NumberDisplayer HundredsDisplay;
    public NumberDisplayer TensDisplay;
    public NumberDisplayer SinglesDisplay;

    private void Start()
    {
        //Get the players final score count
        int FinalScore = GameObject.Find("ScoreSaver").GetComponent<ScoreSaver>().FinalScore;

        //Split the numbers apart
        int[] ScoreNumbers = GetIntArray(FinalScore);
        
        //Display the score on the UI
        SinglesDisplay.DisplayNumber(ScoreNumbers[0]);
        if (ScoreNumbers.Length > 1)
            TensDisplay.DisplayNumber(ScoreNumbers[1]);
        else
            TensDisplay.DisplayNone();
        if (ScoreNumbers.Length > 2)
            HundredsDisplay.DisplayNumber(ScoreNumbers[2]);
        else
            HundredsDisplay.DisplayNone();
        if (ScoreNumbers.Length > 3)
            ThousandsDisplay.DisplayNumber(ScoreNumbers[3]);
        else
            ThousandsDisplay.DisplayNone();
    }

    //This function taken from https://stackoverflow.com/questions/4808612/how-to-split-a-number-into-individual-digits-in-c
    private int[] GetIntArray(int Number)
    {
        List<int> ListOfInts = new List<int>();

        while (Number > 0)
        {
            ListOfInts.Add(Number % 10);
            Number = Number / 10;
        }

        //ListOfInts.Reverse();
        return ListOfInts.ToArray();
    }
}
