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

    // Start is called before the first frame update
    void Start()
    {

        ////////////////////////////////////////
        // EVENTUALLY THIS WILL GENERATE A MAP BASED ON SOME KIND OF FILE OR PREDETERMINED MAP FORMAT
        for (int i = 1; i<= numRows; i++) {
            for (int j = 1; j <= numCols; j++) {

                // create new tile
                GameObject tileIteration = Instantiate(tile,transform);
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

        PathingLogic_Static.START = new Vector2(1, 1);
        PathingLogic_Static.TARGET = new Vector2(numRows, numCols);

        TileBehavior startTile = getTileBehaviorByGridPosition((int)PathingLogic_Static.START.x, (int) PathingLogic_Static.START.y);
        TileBehavior targetTile = getTileBehaviorByGridPosition((int)PathingLogic_Static.TARGET.x, (int)PathingLogic_Static.TARGET.y);

        startTile.isProtectedTile = true;
        targetTile.isProtectedTile = true;


        ////////////////////////////////////////////

        //HeroPathing pathing = new HeroPathing();
        GameController.currentPath = PathingLogic_Static.GetPath();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setTilePosition(GameObject tile, int i, int j) {
        float xPos = -1f*(numCols * (tileWidth / 2f) - (tileWidth * (j - 0.5f)));
        float yPos = numRows * (tileHeight / 2f) - (tileHeight * (i - 0.5f));
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
