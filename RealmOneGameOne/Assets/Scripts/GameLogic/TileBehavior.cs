using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Jaden
* TileBehavior governs the behavior of individual tiles on the map.
* Covers hover behavior, as well as stores the tile's location in the grid (x,y).
*/
public class TileBehavior : MonoBehaviour
{

    public Material tileDefaultMaterial;
    public Material tileMouseOverMaterial;
    public Material tileMouseOverMaterialBadLocation;
    public Material tileBlockMaterial;
    public GameObject testBuilding;

    // protected tiles can be moved through by enemies but cannot have towers placed on them,
    // includes start and end points for enemy paths
    public bool isProtectedTile = false;
    public bool isBlocking = false;

    public bool isValidTowerLocation = true;

    private GameObject currentBuilding;
    private int row;
    private int col;
    private bool mousedOver;
    private TileTypeEnum typeEnum;

    // Start is called before the first frame update
    void Start()
    {
        OnMouseExit();
        setTileMaterial(tileDefaultMaterial);
    }

    void init(TileTypeEnum type) {
        this.typeEnum = type;

        switch (typeEnum)
        {
            case TileTypeEnum.GROUND:
                //do something
                break;
            case TileTypeEnum.BONE_PILE:
                //do something
                break;
            case TileTypeEnum.RIVER:
                //do something
                break;
            case TileTypeEnum.STALAGMITE:
                //do something
                break;
        }
    }

    private void Update()
    {
        if (mousedOver) {
            if (isValidTowerLocation)
            {
                //Left click will change the tile to one that blocks hero movement
                if (Input.GetMouseButtonDown(0))
                {
                        // create building, set to blocking
                        isBlocking = true;
                        getBuildingStatus();

                    if (currentBuilding != null) {
                        setTileMaterial(tileDefaultMaterial);
                    }

                        // update enemies paths.
                        foreach (GameObject enemy in GameController.enemies)
                        {
                            enemy.GetComponent<EnemyBehavior>().updateMyPath();
                        }

                        // update global starting path
                        GameController.currentPath.Clear();
                        GameController.currentPath = PathingLogic_Static.GetPath();

                }
                OnMouseEnter();
            }

            //Right click will change the tile to one that does not blocks hero movement
            if (Input.GetMouseButtonDown(1))
            {
                if (currentBuilding != null)
                {
                    isBlocking = false;
                    getBuildingStatus();
                    foreach (GameObject enemy in GameController.enemies)
                    {
                        enemy.GetComponent<EnemyBehavior>().updateMyPath();
                    }
                    GameController.currentPath.Clear();
                    GameController.currentPath = PathingLogic_Static.GetPath();
                }

                OnMouseEnter();
            }

        }

    }

    void OnMouseEnter() {
        setMousedOver(true);
        checkValidPlaceLocation();
        if (isValidTowerLocation) {
            setTileMaterial(tileMouseOverMaterial);
        }
        else
        {
            if (currentBuilding == null)
            {
                setTileMaterial(tileMouseOverMaterialBadLocation);
            }
        }

        //Debug.Log("---Mouse Over Event---\nRow: " + row.ToString() + "\nColumn: " + col.ToString());

    }

    void OnMouseExit() {
        setTileMaterial(tileDefaultMaterial);
        setMousedOver(false);
    }

    void getBuildingStatus()
    {
        if(currentBuilding != null)
        {
            if (currentBuilding.TryGetComponent<TowerBehavior>(out TowerBehavior currentTowerBehavior)) {
                int creditsToAdd = Mathf.CeilToInt(currentTowerBehavior.cost * GameController.towerSellMultiplier);
                GameController.ChangeCreditsBy(creditsToAdd);
            }
            Destroy(currentBuilding);
            isBlocking = false;

        }
        else if(currentBuilding == null)
        {
            if (testBuilding.TryGetComponent<TowerBehavior>(out TowerBehavior towerBehavior)){

                // if you have enough credits to purchase tower:
                if (GameController.GetCurrentCredits() - towerBehavior.cost >= 0)
                {
                    // instantiate tower
                    float yInstantiate = towerBehavior.model.transform.localScale.y / 2f;
                    currentBuilding = Instantiate(testBuilding, new Vector3(transform.position.x, yInstantiate, transform.position.z), Quaternion.identity);

                    // remove credits
                    GameController.ChangeCreditsBy(-1 * towerBehavior.cost);
                    isBlocking = true;
                }
                else {
                    isBlocking = false;
                }
            }         

        }
    }

    void setTileMaterial(Material material) {
        gameObject.GetComponent<Renderer>().material = material;
    }
    Material getTileMaterial() {
        return gameObject.GetComponent<Renderer>().material;
    }

    public int getRow() {
        return row;
    }
    public void setRow(int row) {
        this.row = row;
    }

    public int getCol() {
        return col;
    }
    public void setCol(int col) {
        this.col = col;
    }

    public bool isMousedOver() {
        return mousedOver;
    }
    public void setMousedOver(bool mousedOver) {
        this.mousedOver = mousedOver;
    }

    public TileTypeEnum getTypeEnum() {
        return this.typeEnum;
    }
    public void setTypeEnum(TileTypeEnum typeEnum){
        this.typeEnum = typeEnum;
    }


    private void checkValidPlaceLocation() {
        Vector2 thisTilePosition = new Vector2(row, col);

        // can't put a building if there's already a building or obstruction or tile is protected
        if (isBlocking || isProtectedTile) {
            isValidTowerLocation = false;
            return;
        }

        // can't put a building if it would obstruct the start to target path
        isBlocking = true;
        List<Vector2> fullPath = PathingLogic_Static.GetPath();
        if (!fullPath.Contains(PathingLogic_Static.TARGET)) {
            isValidTowerLocation = false;
            isBlocking = false;
            return;
        }
        isBlocking = false;

        // if there are any enemies:
        if (GameController.enemies.ToArray().Length > 0) {

            // For each enemy, see if enemy is moving to or from the location you're trying to put a building.
            //  If an enemy is moving to or from that spot, you can't put a building there. 
            foreach (GameObject enemy in GameController.enemies)
            {
                //Debug.Log("Last Tile Hit: " + enemy.GetComponent<MovementTest>().getLastTileHitInPath());
                //Debug.Log("Next Tile Hit: " + enemy.GetComponent<MovementTest>().getNextTileInPath());

                if (enemy.GetComponent<EnemyBehavior>().getLastTileHitInPath() == thisTilePosition ||
                    enemy.GetComponent<EnemyBehavior>().getNextTileInPath() == thisTilePosition)
                {
                    isValidTowerLocation = false;
                    return;
                }

                //Can't put a building that would obstruct any individual enemy's path
                isBlocking = true;
                Vector2 enemyPosition = enemy.GetComponent<EnemyBehavior>().getLastTileHitInPath();
                List<Vector2> enemyPathTest = PathingLogic_Static.GetPath(enemyPosition);
                if (!enemyPathTest.Contains(PathingLogic_Static.TARGET)) {
                    isValidTowerLocation = false;
                    isBlocking = false;
                    return;
                }
                isBlocking = false;
            }

        }

        isValidTowerLocation = true;
    }
}
