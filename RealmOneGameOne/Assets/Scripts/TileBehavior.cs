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
    private int row;
    private int col;
    private bool mousedOver;
    private TileTypeEnum typeEnum;

    // Start is called before the first frame update
    void Start()
    {
        OnMouseExit();
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

    void OnMouseEnter() {
        setTileMaterial(tileMouseOverMaterial);
        Debug.Log("---Mouse Over Event---\nRow: " + row.ToString() + "\nColumn: " + col.ToString());
        setMousedOver(true);
    }

    void OnMouseExit() {
        setTileMaterial(tileDefaultMaterial);
        setMousedOver(false);
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
}
