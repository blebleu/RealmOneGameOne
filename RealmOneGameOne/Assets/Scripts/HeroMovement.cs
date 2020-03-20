using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* Author: Brent
* HeroMovement handles the movement of the heroes along the grid. 
* 
*/
public class HeroMovement : MonoBehaviour
{


    int numRows;
    int numCols;
    public TileMapLogic currentMapLogic;
    // Start is called before the first frame update
    void Start()
    {
        GetMapSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetMapSize()
    {
        numCols = currentMapLogic.numCols;
        numRows = currentMapLogic.numRows;
    }


    bool TestDirection(int x, int y, int step, int direction)
    {
        return true;
    }



}
