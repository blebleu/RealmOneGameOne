using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // REMOVE THIS LATER, THIS IS JUST TO TEST ENEMY MOVEMENT
    public GameObject enemyBasic;

    // public GameObject enemy(other types);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // THIS IS JUST A TEST OF SUMMONING ENEMIES AND GETTING THEM TO RECOGIZE THE MAP LOGIC
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject enemyInstance = Instantiate(enemyBasic,TileMapLogic.getTileBehaviorByGridPosition(1,1).transform.position,Quaternion.identity);
            GameController.enemies.Add(enemyInstance);
        }
    }

    
}
