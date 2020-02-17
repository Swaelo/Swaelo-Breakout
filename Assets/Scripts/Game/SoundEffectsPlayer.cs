// ================================================================================================================================
// File:        SoundEffectsPlayer.cs
// Description:	Handles the playing of sound effects during gameplay
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    //Singleton class instance
    public static SoundEffectsPlayer Instance;
    private void Awake() { Instance = this; }

    public AudioSource SoundPlayer; //Component used to play the sound effects

    //Sounds to play when blocks are destroyed in the game
    public AudioClip DestroyRedBlockSound;
    public AudioClip DestroyOrangeBlockSound;
    public AudioClip DestroyYellowBlockSound;
    public AudioClip DestroyGreenBlockSound;
    public AudioClip DestroyBlueBlockSound;

    //Sounds to play when the ball bounces off things
    public AudioClip BounceBoundarySound;
    public AudioClip BouncePaddleSound;

    //Sounds to play during specific gameplay events
    public AudioClip BeginRoundSound;
    public AudioClip BallLostSound;

    //Plays the sound when one of the colored blocks is destroyed
    public void PlayDestroyBlockSound(string BlockColor)
    {
        switch(BlockColor)
        {
            case ("Red"):
                SoundPlayer.PlayOneShot(DestroyRedBlockSound);
                break;
            case ("Orange"):
                SoundPlayer.PlayOneShot(DestroyOrangeBlockSound);
                break;
            case ("Yellow"):
                SoundPlayer.PlayOneShot(DestroyYellowBlockSound);
                break;
            case ("Green"):
                SoundPlayer.PlayOneShot(DestroyGreenBlockSound);
                break;
            case ("Blue"):
                SoundPlayer.PlayOneShot(DestroyBlueBlockSound);
                break;
        }
    }

    //Plays the sound when the ball bounces off something
    public void PlayBallBounceSound(string BounceObject)
    {
        switch(BounceObject)
        {
            case ("Boundary"):
                SoundPlayer.PlayOneShot(BounceBoundarySound);
                break;
            case ("Paddle"):
                SoundPlayer.PlayOneShot(BouncePaddleSound);
                break;
        }
    }

    //Plays the sound when a certain gameplay event occurs
    public void PlayEventSound(string EventName)
    {
        switch (EventName)
        {
            case ("BeginRound"):
                SoundPlayer.PlayOneShot(BeginRoundSound);
                break;
            case ("BallLost"):
                SoundPlayer.PlayOneShot(BallLostSound);
                break;
        }

    }
}
