// ================================================================================================================================
// File:        BallMovement.cs
// Description:	Moves the ball around the play field and handles different collision events
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    //Movement variables
    public float NormalSpeed = 3f;      //The balls default speed at the start of a new round
    public float WarpSpeed = 5f;        //The balls increased speed once it destroys a red or orange block
    private float CurrentSpeed = 3f;    //The balls current speed used to move it around
    private Vector3 CurrentDirection;   //Current direction the ball is travelling

    //Paddle variables
    public GameObject PaddleObject;     //The paddle the player uses to keep the ball inside the game
    private Vector3 PaddleOffset = new Vector3(0f, 0.5f, 0f);   //Balls offset from the paddle at the start of the round
    public PaddleMovement PaddleMovementTracker;    //Tracks the paddles movement during gameplay, effects how the ball bounces off it
    public float PaddleSpinPower = 0.25f;   //How much the paddle effects the balls x direction value if its hit while the paddle is moving

    //Countdown variables
    public NumberDisplayer RoundBeginCounter;   //Displays time left until new round begins
    private float BeginCounterDuration = 3f;    //How long the start of round countdown lasts for
    private float BeginCounterRemaining = 3f;   //Time until end of current start of round countdown
    private bool RoundBegun = false;            //Flags if the round has started yet or not

    //Enforce a cooldown for reflecting the balls direction so it doesnt cancel itself out when destroying two blocks at once
    private float ReflectionLimit = 0.01f;      //How often the balls direction can be reflected
    private float ReflectionCooldown = 0.01f;   //Duration before the ball is allowed to be reflected again
    private bool ReflectionLock = false;        //Flagged to stop the balls direction from reflecting during the cooldown phase

    private void Update()
    {
        //Handle the countdown timer if the round hasnt started yet
        if (!RoundBegun)
        {
            HandleRoundCountdown();
            return;
        }
        
        //Manage the reflection lockout when its active
        if(ReflectionLock)
        {
            ReflectionCooldown -= Time.deltaTime;
            if (ReflectionCooldown <= 0.0f)
                ReflectionLock = false;
        }

        //Apply movement to the ball each frame
        transform.position += CurrentDirection * CurrentSpeed * Time.deltaTime;
    }

    //Returns the ball immediately to the paddle and releases it instantly
    public void ReturnBall()
    {
        transform.position = PaddleObject.transform.position + PaddleOffset;
        CurrentDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f);
    }

    //Manages counting down and displaying of the new round timer
    private void HandleRoundCountdown()
    {
        //Decrement the current timer value
        BeginCounterRemaining -= Time.deltaTime;

        //Start the round if the timer has expired
        if(BeginCounterRemaining <= 0.0f)
        {
            RoundBegun = true;
            RoundBeginCounter.DisplayNone();
            ReleaseBall();
            return;
        }

        //Update the timer display
        RoundBeginCounter.DisplayNumber((int)BeginCounterRemaining + 1);
    }

    //Releases the ball from the paddle and sends it in a random starting direction
    private void ReleaseBall()
    {
        //Release the ball from the paddle
        transform.parent = null;

        //Reset the speed and direction values
        CurrentSpeed = NormalSpeed;
        CurrentDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f);

        //Play the round begin sound effect
        SoundEffectsPlayer.Instance.PlayEventSound("BeginRound");
    }

    //Handles collision events between all different objects in the game
    private void OnCollisionEnter(Collision collision)
    {
        //Read the tag of the object the ball has collided with
        string CollisionTag = collision.transform.tag;

        //Use this tag to invoke the correct function for handling this collision event correctly
        switch(CollisionTag)
        {
            //Colored Block Collision Handlers
            case "Red":
                HandleRedCollision(collision);
                break;
            case "Orange":
                HandleOrangeCollision(collision);
                break;
            case "Yellow":
                HandleYellowCollision(collision);
                break;
            case "Green":
                HandleGreenCollision(collision);
                break;
            case "Blue":
                HandleBlueCollision(collision);
                break;

            //Top and Side Handlers
            case "Side":
                HandleSideCollision(collision);
                break;
            case "Top":
                HandleTopCollision(collision);
                break;

            //Paddle Collision Handler
            case "Paddle":
                HandlePaddleCollision(collision);
                break;

            //Death Trigger Handler
            case "Death":
                HandleDeathCollision();
                break;
        }
    }

    //Block Collision Handlers
    private void HandleRedCollision(Collision Block)
    {
        //Reflect Ball, Play sounds, add points, destroy block, increase to warp speed
        ReflectBall(Block);
        SoundEffectsPlayer.Instance.PlayDestroyBlockSound("Red");
        ScoreCounter.Instance.IncrementScore(10);
        GameObject.Destroy(Block.gameObject);
        BlockReplacer.Instance.BlockDestroyed();
        CurrentSpeed = WarpSpeed;
    }
    private void HandleOrangeCollision(Collision Block)
    {
        //Reflect Ball, Play sounds, add points, destroy block, increase to warp speed
        ReflectBall(Block);
        SoundEffectsPlayer.Instance.PlayDestroyBlockSound("Orange");
        ScoreCounter.Instance.IncrementScore(7);
        GameObject.Destroy(Block.gameObject);
        BlockReplacer.Instance.BlockDestroyed();
        CurrentSpeed = WarpSpeed;
    }
    private void HandleYellowCollision(Collision Block)
    {
        //Reflect Ball, Play sounds, add points, destroy block
        ReflectBall(Block);
        SoundEffectsPlayer.Instance.PlayDestroyBlockSound("Yellow");
        ScoreCounter.Instance.IncrementScore(5);
        GameObject.Destroy(Block.gameObject);
        BlockReplacer.Instance.BlockDestroyed();
    }
    private void HandleGreenCollision(Collision Block)
    {
        //Reflect Ball, Play sounds, add points, destroy block
        ReflectBall(Block);
        SoundEffectsPlayer.Instance.PlayDestroyBlockSound("Green");
        ScoreCounter.Instance.IncrementScore(3);
        GameObject.Destroy(Block.gameObject);
        BlockReplacer.Instance.BlockDestroyed();
    }
    private void HandleBlueCollision(Collision Block)
    {
        //Reflect Ball, Play sounds, add points, destroy block
        ReflectBall(Block);
        SoundEffectsPlayer.Instance.PlayDestroyBlockSound("Blue");
        ScoreCounter.Instance.IncrementScore(1);
        GameObject.Destroy(Block.gameObject);
        BlockReplacer.Instance.BlockDestroyed();
    }

    //Top and Side Collision Handlers
    private void HandleTopCollision(Collision Top)
    {
        ReflectBall(Top);
        SoundEffectsPlayer.Instance.PlayBallBounceSound("Boundary");
    }
    private void HandleSideCollision(Collision Side)
    {
        ReflectBall(Side);
        SoundEffectsPlayer.Instance.PlayBallBounceSound("Boundary");
    }

    //Paddle Collision Handler
    private void HandlePaddleCollision(Collision Paddle)
    {
        //Reflect and play sounds
        ReflectBall(Paddle);
        SoundEffectsPlayer.Instance.PlayBallBounceSound("Paddle");

        //Adjust the x direction value, applying spin to the ball if the paddle was moving when it was hit
        if(PaddleMovementTracker.Movement != Direction.None)
            CurrentDirection.x += PaddleMovementTracker.Movement == Direction.Right ? PaddleSpinPower : -PaddleSpinPower;
    }

    //Death Trigger Handler
    private void HandleDeathCollision()
    {
        //Play sounds
        SoundEffectsPlayer.Instance.PlayEventSound("BallLost");

        //Remove 1 from remaining balls
        LivesTracker.Instance.BallsRemaining -= 1;
        LivesTracker.Instance.UpdateBallsDisplay();

        //Move to gameover if no balls remain
        if(LivesTracker.Instance.BallsRemaining == 0)
        {
            //Save the score into a persistant object then transition to the game over scene
            ScoreCounter.Instance.SaveScore();
            SceneManager.LoadScene(2);
        }
        //Otherwise start the countdown until the new round begins
        else
        {
            //Start the round over again
            RoundBegun = false;
            BeginCounterRemaining = BeginCounterDuration;
            //Return the ball to the paddle
            transform.position = PaddleObject.transform.position + PaddleOffset;
            transform.parent = PaddleObject.transform;
        }
    }

    //Reflects the ball from the surface it has collided with
    private void ReflectBall(Collision ReflectionSurface)
    {
        //Exit immediately if the ball is still in reflection lockout
        if (ReflectionLock)
            return;

        //Activate the reflection lock and start the cooldown
        ReflectionLock = true;
        ReflectionCooldown = ReflectionLimit;

        //Use the normal of the surface the ball collided with to reflect the direction vector off it
        Vector3 CollisionNormal = ReflectionSurface.contacts[0].normal;
        CurrentDirection = Vector3.Reflect(CurrentDirection, CollisionNormal);
    }
}
