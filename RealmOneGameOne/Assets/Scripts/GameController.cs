using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is the primary game controller.
Will likely be attached to an empty object in game and keep track of certain game variables.
*/

public class GameController : MonoBehaviour
{
    public static Dictionary<Vector2, TileBehavior> positionMap = new Dictionary<Vector2, TileBehavior>();
    public static List<Vector2> currentPath = new List<Vector2>();
    //public static HeroPathing pathing = new HeroPathing();

    // global list of tags to mitigate spelling errors, because referencing by string values is very bad no good
    public static readonly string PROJECTILE_TAG = "Projectile";
    public static readonly string ENEMY_TAG = "Enemy";

    // list of enemies currently in the scene
    public static List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
