// ================================================================================================================================
// File:        BlockReplacer.cs
// Description:	Replaces all the blocks onto the field once they have all been destroyed so the user can continue playing
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class BlockReplacer : MonoBehaviour
{
    //Singleton Instance
    public static BlockReplacer Instance;
    private void Awake() { Instance = this; }

    public GameObject BlocksPrefab; //Prefab containing all the blocks in the game so they can easily be replaced
    private int BlocksDestroyed = 0; //Tracks how many blocks have been destroyed so far
    private int TotalBlockCount = 45;   //How many blocks need to be destroyed before they will all be respawned back again

    //Blocks alert this class when they have been destroyed so we can keep track of them
    public void BlockDestroyed()
    {
        //Update the number of blocks destroyed so far
        BlocksDestroyed += 1;

        //If the total amount have been destroyed, then they all need to be placed back again
        if(BlocksDestroyed == TotalBlockCount)
        {
            //Instruct the ball to move back to the paddle so it doesnt get stuck under the blocks that are about to be spawned in
            GameObject.Find("Ball").GetComponent<BallMovement>().ReturnBall();

            //Replace all the blocks
            GameObject.Instantiate(BlocksPrefab, BlocksPrefab.transform.position, BlocksPrefab.transform.rotation);

            //Reset the counter of blocks destroyed so far
            BlocksDestroyed = 0;
        }
    }
}
