using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Dictionary<Vector2, TileBehavior> positionMap = new Dictionary<Vector2, TileBehavior>();
    public static List<Vector2> currentPath = new List<Vector2>();
    public static HeroPathing pathing = new HeroPathing();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
