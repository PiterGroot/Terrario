using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClearTilemap : MonoBehaviour
{
    [ContextMenu("Clear Tilemap")]
    public void ClearTilemapEditor(){
        transform.GetChild(0).gameObject.GetComponent<Tilemap>().ClearAllTiles();
        transform.GetChild(1).gameObject.GetComponent<Tilemap>().ClearAllTiles();
    }
}
