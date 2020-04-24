using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Dictionary<Vector2, TileBehavior> positionMap = new Dictionary<Vector2, TileBehavior>();
    public static List<Vector2> currentPath = new List<Vector2>();
    public static HeroPathing pathing = new HeroPathing();

    public static readonly string PROJECTILE_TAG = "Projectile";

    public static List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum EnemyTypeEnum {
        ENEMY_BASIC
    }
    public enum ProjectileTypeEnum {
        PROJECTILE_BASIC
    }
    public enum TowerTypeEnum {
        TOWER_BASIC
    }

    public static int GetEnemyDamageDealt(EnemyTypeEnum enemy, ProjectileTypeEnum projectile) {
        int returnValue = 5;

        switch(enemy){
            case EnemyTypeEnum.ENEMY_BASIC:

                switch (projectile) {
                    case ProjectileTypeEnum.PROJECTILE_BASIC:
                        break;
                }

                break;
               
        }

        return returnValue;
    }

}
