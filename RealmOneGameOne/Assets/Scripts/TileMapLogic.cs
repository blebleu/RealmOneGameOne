using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GameController;

public class TileMapLogic : MonoBehaviour
{
    //private Dictionary<Vector2, TileBehavior> positionMap = new Dictionary<Vector2, TileBehavior>();
    public int numCols = 10;
    public int numRows = 10;

    // says size = 1 on inspector, but means 10 grid points, according to unity units of length
    private float tileWidth = 10;
    private float tileHeight = 10;

    public GameObject tile;

    // REMOVE THIS LATER, THIS IS JUST TO TEST ENEMY MOVEMENT
    public GameObject enemyTest;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i<= numRows; i++) {
            for (int j = 1; j <= numCols; j++) {

                // create new tile
                GameObject tileIteration = Instantiate(tile);
                // get its logic object
                TileBehavior tileBehavior = tileIteration.GetComponent<TileBehavior>();
                // set row and col
                tileBehavior.setRow(i);
                tileBehavior.setCol(j);
                // set position
                setTilePosition(tileIteration, i, j);
                // put in map for reference
                GameController.positionMap.Add(new Vector2(i, j), tileBehavior);
               
               
            }
        }

        HeroPathing pathing = new HeroPathing();
        GameController.currentPath = GameController.pathing.GetPath();
    }

    // Update is called once per frame
    void Update()
    {
        // THIS IS JUST A TEST OF SUMMONING ENEMIES AND GETTING THEM TO RECOGIZE THE MAP LOGIC
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(enemyTest);
        }
    }

    private void setTilePosition(GameObject tile, int i, int j) {
        float xPos = -1f*(numCols * (tileWidth / 2f) - (tileWidth * (j - 0.5f)));
        float yPos = numRows * (tileHeight / 2f) - (tileHeight * (i - 0.5f));
        //float xPos = numCols * tileWidth * -0.5f + tileWidth * (i - 1);
        //float yPos = numRows * tileHeight * -0.5f + tileHeight * (j - 1);
        tile.transform.position = new Vector3(xPos, 0, yPos);
    }

    public static TileBehavior getTileBehaviorByGridPosition(int i, int j) {
        TileBehavior returnTileBehavior = null;
        try
        {
            returnTileBehavior = GameController.positionMap[new Vector2(i, j)];
        }
        catch (KeyNotFoundException e) {
            return null;
        }
        return returnTileBehavior;
    }
}
