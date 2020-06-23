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

public class EnemyBehavior : MonoBehaviour
{
    // PUBLIC PROPERTIES
    public int health;
    public int speed;
    public int lookSpeed;
    public int damage;
    public EnemyTypeEnum type;


    // PRIVATE VARS
    private List<Vector2> myCurrentPath;

    // tileSequence defines the sequence of tiles to traverse. 
    // For example: If we want the player to move from (1,1) to (1,2) then to (2,2), then tileSequence would be [ (1,1) , (1,2) , (2,2) ].

    // currentPlaceInTileSequence just keeps track of how far in the sequence you are. In the above example, if the player was moving to (1,2), then this value would be 1.
    private int currentPlaceInTileSequence;

    private Quaternion travelToNewPointRotation;

    private Vector3 currentMoveToLocation;

    // Use this for initialization
    void Awake()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (myCurrentPath.Count > 0) {
            MoveAlongPath();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameController.PROJECTILE_TAG)
        {
            if (other.gameObject.TryGetComponent<ProjectileBehavior>(out ProjectileBehavior projectileBehavior)) {
                health -= CalculateDamageFromEnemyTypeAndProjectileType(type, projectileBehavior.type);
                Destroy(other.gameObject);
                CheckHealth();
            }
                
        }
    }

    void OnDestroy()
    {
        GameController.enemies.Remove(gameObject);
    }


    void init() {

        // define current path based on global path set in gamecontroller
        myCurrentPath = GameController.currentPath;
        currentPlaceInTileSequence = 0;

        if (myCurrentPath.Count > 0) {
            setNextMoveToLocation();
        }
    }

    public void setNextMoveToLocation() {

        // set next moveto location
        currentPlaceInTileSequence++;

        if (currentPlaceInTileSequence > myCurrentPath.ToArray().Length-1) {
            Debug.Log("HIT DESTINATION");
            GameController.ChangeBaseHealthBy(-1 * damage);
            Destroy(gameObject);
            return;
        }

        Vector2 nextTileCoord = myCurrentPath[currentPlaceInTileSequence];

        // gets current moveto location by getting the specific tile at that grid coordinate and grabbing its position.
        currentMoveToLocation = TileMapLogic
            .getTileBehaviorByGridPosition((int)nextTileCoord[0], (int)nextTileCoord[1])
            .transform
            .position;
    }

    void MoveAlongPath() {
        // if hero has reached new destination
        if (Vector3.Distance(transform.position, currentMoveToLocation) < 1.0f)
        {
            setNextMoveToLocation();
        }
        // if hero has NOT yet reached new destination
        else
        {
            // move toward destination
            transform.position = Vector3.MoveTowards(transform.position, currentMoveToLocation, speed * Time.deltaTime);

            // set look rotation
            travelToNewPointRotation = Quaternion.LookRotation(currentMoveToLocation - transform.position);
            travelToNewPointRotation.x = 0;
            travelToNewPointRotation.z = 0;

            // look at destination
            transform.rotation = Quaternion.Lerp(transform.rotation, travelToNewPointRotation, lookSpeed * Time.deltaTime);
        }
    }

    public void updateMyPath() {
        myCurrentPath = PathingLogic_Static.GetPath(getNextTileInPath());
        currentPlaceInTileSequence = -1;
        setNextMoveToLocation();
    }

    public Vector2 getLastTileHitInPath() {
        // if it hasn't been anywhere, only restrict current place in tile sequence
        if (currentPlaceInTileSequence == 0) {
            return myCurrentPath[currentPlaceInTileSequence];
        }
        // if it has been somewhere, restrict where it's coming from AND going to
        return myCurrentPath[currentPlaceInTileSequence-1];
    }
    public Vector2 getNextTileInPath() {
        return myCurrentPath[currentPlaceInTileSequence];
    }

    public static int CalculateDamageFromEnemyTypeAndProjectileType(EnemyTypeEnum enemyType, ProjectileTypeEnum projectileType) {
        return 10;
    }

    private void CheckHealth() {
        if (health <= 0)
        {
            GameController.ChangeCreditsBy(10);
            Destroy(gameObject);
        }
    }
}
