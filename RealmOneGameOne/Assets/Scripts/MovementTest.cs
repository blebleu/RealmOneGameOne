using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is sort of a frankenstein code from my other project where it's used for enemies as patrol logic.
 * Originally, this code pulled random points from an array and had the enemy walk there and look around randomly.
 * The basic point to point movement behavior has been preserved.
 * 
 * - Jaden
 **/

public class MovementTest : MonoBehaviour
{

    public float moveSpeed;
    public float lookSpeed;

    // tileSequence defines the sequence of tiles to traverse. 
    // For example: If we want the player to move from (1,1) to (1,2) then to (2,2), then tileSequence would be [ (1,1) , (1,2) , (2,2) ].

    // currentPlaceInTileSequence just keeps track of how far in the sequence you are. In the above example, if the player was moving to (1,2), then this value would be 1.
    private int currentPlaceInTileSequence;

    private Quaternion travelToNewPointRotation;

    private Vector3 currentMoveToLocation;

    // Use this for initialization
    void Start()
    {
        currentPlaceInTileSequence = 0;
        setNextMoveToLocation();
    }

    // Update is called once per frame
    void Update()
    {
        // if hero has reached new destination
        if (Vector3.Distance(transform.position, currentMoveToLocation) < 1.0f)
        {
            setNextMoveToLocation();               
        }
        // if hero has NOT yet reached new destination
        else
        {
            // move toward destination
            transform.position = Vector3.MoveTowards(transform.position, currentMoveToLocation, moveSpeed * Time.deltaTime);

            // set look rotation
            travelToNewPointRotation = Quaternion.LookRotation(currentMoveToLocation - transform.position);
            travelToNewPointRotation.x = 0;
            travelToNewPointRotation.z = 0;

            // look at destination
            transform.rotation = Quaternion.Lerp(transform.rotation, travelToNewPointRotation, lookSpeed * Time.deltaTime);
        }

    }

    public void setNextMoveToLocation() {
        // set next moveto location
        Vector2 nextTileCoord = GameController.currentPath[currentPlaceInTileSequence];
        if (nextTileCoord == null) {
            Debug.Log("HIT DESTINATION");
            Destroy(gameObject);
        }
        // gets current moveto location by getting the specific tile at that grid coordinate and grabbing its position.
        currentMoveToLocation = TileMapLogic
            .getTileBehaviorByGridPosition((int)nextTileCoord[0], (int)nextTileCoord[1])
            .transform
            .position;

        currentPlaceInTileSequence++;
    }
}
