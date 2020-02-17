// ================================================================================================================================
// File:        ScoreCounter.cs
// Description:	Tracks the players current score and handles displaying it on the UI
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    //Singleton instance
    public static ScoreCounter Instance;
    private void Awake() { Instance = this; }

    //Current and maximum score value
    public int CurrentScore = 0;

    //Objects used to display each value in the current score count
    public NumberDisplayer SinglesDisplay;
    public NumberDisplayer TensDisplay;
    public NumberDisplayer HundredsDisplay;
    public NumberDisplayer ThousandsDisplay;

    //Used to carry score over to the gameover scene
    public GameObject ScoreSaverPrefab;

    private void Start()
    {
        //Hide the Thousands, Hundreds and Tens display, and set the singles to 0
        ThousandsDisplay.DisplayNone();
        HundredsDisplay.DisplayNone();
        TensDisplay.DisplayNone();
        SinglesDisplay.DisplayNumber(0);
    }

    //Spawns the ScoreSaverPrefab, sets it to remain between scenes and stores the current score within it
    public void SaveScore()
    {
        GameObject ScoreSaver = GameObject.Instantiate(ScoreSaverPrefab);
        GameObject.DontDestroyOnLoad(ScoreSaver);
        ScoreSaver.GetComponent<ScoreSaver>().FinalScore = CurrentScore;
    }

    //Adds to the current score and updates the displays
    public void IncrementScore(int IncrementValue)
    {
        //Increment the current score value
        CurrentScore += IncrementValue;

        //Split the score values into an array of single numbers
        int[] ScoreValues = GetIntArray(CurrentScore);

        //Display each value on the UI there is to display
        SinglesDisplay.DisplayNumber(ScoreValues[0]);
        if (ScoreValues.Length > 1)
            TensDisplay.DisplayNumber(ScoreValues[1]);
        if (ScoreValues.Length > 2)
            HundredsDisplay.DisplayNumber(ScoreValues[2]);
        if (ScoreValues.Length > 3)
            ThousandsDisplay.DisplayNumber(ScoreValues[3]);
    }

    //This function taken from https://stackoverflow.com/questions/4808612/how-to-split-a-number-into-individual-digits-in-c
    private int[] GetIntArray(int Number)
    {
        List<int> ListOfInts = new List<int>();

        while(Number > 0)
        {
            ListOfInts.Add(Number % 10);
            Number = Number / 10;
        }

        //ListOfInts.Reverse();
        return ListOfInts.ToArray();
    }
}
