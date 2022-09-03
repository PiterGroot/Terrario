using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTile : MonoBehaviour
{
    private Vector3 mouseWorldPos;
    private bool updatePhysics;
    private bool isDead;
    private Vector3 pos;
    private WorldGeneration worldGen;
    private PhysicsManager physicsManager;
    [SerializeField]private Tile thisTile;
    private void Awake() {
        worldGen = FindObjectOfType<WorldGeneration>();
        physicsManager = FindObjectOfType<PhysicsManager>();
        Vector3Int cellPosition = worldGen.Tilemap.LocalToCell(transform.position);
        transform.localPosition = worldGen.Tilemap.GetCellCenterLocal(cellPosition);
        worldGen.Tilemap.SetTile(cellPosition, thisTile);
        InvokeRepeating("UpdateTile", 0, physicsManager.updateTimer);
    }
    //update order: down --> down-left --> down-right --> left --> right
    public void UpdateTile(){
        if(!isDead){
            Vector3Int xyPosDown = worldGen.Tilemap.WorldToCell(new Vector3(transform.position.x, transform.position.y -1, 0));
            Vector3Int xyPosDownLeft = worldGen.Tilemap.WorldToCell(new Vector3(transform.position.x - 1, transform.position.y -1, 0));
            Vector3Int xyPosDownRight = worldGen.Tilemap.WorldToCell(new Vector3(transform.position.x + 1, transform.position.y -1, 0));
            Vector3Int xyPosLeft = worldGen.Tilemap.WorldToCell(new Vector3(transform.position.x - 1, transform.position.y, 0));
            Vector3Int xyPosRight = worldGen.Tilemap.WorldToCell(new Vector3(transform.position.x + 1, transform.position.y, 0));
            if(!worldGen.Tilemap.HasTile(xyPosDown)){
                //movedown
                Vector3Int cellPosition = worldGen.Tilemap.LocalToCell(transform.position);
                worldGen.Tilemap.SetTile(cellPosition, null);
                worldGen.Tilemap.SetTile(xyPosDown, thisTile);
                transform.position = new Vector3(transform.position.x, transform.position.y -1,0);
                if(transform.position.y < physicsManager.fluidsKillBarrier){
                    worldGen.Tilemap.SetTile(xyPosDown, null);
                    Destroy(gameObject);
                }
            }
            else if(!worldGen.Tilemap.HasTile(xyPosDownLeft)){
                //down-left
                Vector3Int cellPosition = worldGen.Tilemap.LocalToCell(transform.position);
                worldGen.Tilemap.SetTile(cellPosition, null);
                worldGen.Tilemap.SetTile(xyPosDownLeft, thisTile);
                transform.position = new Vector3(transform.position.x - 1, transform.position.y -1, 0);
            }
            else if(!worldGen.Tilemap.HasTile(xyPosDownRight)){
                //down-right
                Vector3Int cellPosition = worldGen.Tilemap.LocalToCell(transform.position);
                worldGen.Tilemap.SetTile(cellPosition, null);
                worldGen.Tilemap.SetTile(xyPosDownRight, thisTile);
                transform.position = new Vector3(transform.position.x + 1, transform.position.y -1, 0);
            }
            else if(!worldGen.Tilemap.HasTile(xyPosRight) && !worldGen.Tilemap.HasTile(xyPosLeft)){
                //isDead = true;
                return;
            }
            else if(!worldGen.Tilemap.HasTile(xyPosLeft)){
                //left
                if(worldGen.Tilemap.GetTile(xyPosRight) != thisTile){
                    Vector3Int cellPosition = worldGen.Tilemap.LocalToCell(transform.position);
                    worldGen.Tilemap.SetTile(cellPosition, null);
                    worldGen.Tilemap.SetTile(xyPosLeft, thisTile);
                    transform.position = worldGen.Tilemap.WorldToCell(new Vector3(transform.position.x - 1, transform.position.y, 0));
                }
            }
            else if(!worldGen.Tilemap.HasTile(xyPosRight)){
                //right
                if(worldGen.Tilemap.GetTile(xyPosLeft) != thisTile){
                    Vector3Int cellPosition = worldGen.Tilemap.LocalToCell(transform.position);
                    worldGen.Tilemap.SetTile(cellPosition, null);
                    worldGen.Tilemap.SetTile(xyPosRight, thisTile);
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                }
            }
            //TODO another left right hastile check, but now with a check if under 3 cells (downleft, downright and down) are occupied
            else{
                //isDead = true;
                return;
            }
        }
    }
}
