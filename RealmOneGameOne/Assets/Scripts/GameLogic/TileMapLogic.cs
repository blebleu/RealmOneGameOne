using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TileMapLogic : MonoBehaviour
{
    public int numCols;
    public int numRows;

    // says size = 1 on inspector, but means 10 grid points, according to unity units of length
    private float tileWidth = 10;
    private float tileHeight = 10;

    public GameObject tile;

    public TextAsset[] mapFiles;

    // Start is called before the first frame update
    void Start()
    {
        //HeroPathing pathing = new HeroPathing();
        loadMap(0);
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

    public void loadMap(int mapIndex) {
        TextAsset mapFileAsset;
        try {
            mapFileAsset = mapFiles[mapIndex];
            string mapAsString = mapFileAsset.text;
            string[] lines = mapAsString.Split('\n');

            numCols = lines[0].Trim().ToCharArray().Length;
            numRows = lines.Length;

            for (int i = 1; i <= numRows; i++)
            {
                for (int j = 1; j <= numCols; j++)
                {
                    // get the jth char in the ith line (trimmed)
                    char c = lines[i-1].Trim().ToCharArray()[j-1];

                    // create new tile
                    GameObject tileIteration = Instantiate(tile, transform);
                    // get its logic object
                    TileBehavior tileBehavior = tileIteration.GetComponent<TileBehavior>();
                    // set row and col
                    tileBehavior.setRow(i);
                    tileBehavior.setCol(j);
                    // set position
                    setTilePosition(tileIteration, i, j);

                    char upper = c.ToString().ToUpper().ToCharArray()[0];

                    switch (upper)
                    {
                        case 'O':
                            // put in map for reference
                            GameController.positionMap.Add(new Vector2(i, j), tileBehavior);
                            break;
                        case 'X':
                            // put in map for reference
                            GameController.positionMap.Add(new Vector2(i, j), tileBehavior);
                            tileBehavior.makeObstruction();
                            break;
                        case 'A':
                            // put in map for reference
                            GameController.positionMap.Add(new Vector2(i, j), tileBehavior);
                            PathingLogic_Static.START = new Vector2(i, j);
                            tileBehavior.isProtectedTile = true;
                            break;
                        case 'B':
                            // put in map for reference
                            GameController.positionMap.Add(new Vector2(i, j), tileBehavior);
                            PathingLogic_Static.TARGET = new Vector2(i, j);
                            tileBehavior.makeBase();
                            break;
                        default:
                            Destroy(tileIteration);
                            continue;
                            break;
                    }
                }
            }
        }
        catch (Exception except){
            Debug.Log("Failed to Load Map");
            Debug.Log(except.ToString());
            SceneManager.LoadScene("StartScene");
            return;
        }
 
    }
}
