using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaveGeneration : MonoBehaviour
{
    private bool CavesDone = false;
    private bool MakingCentralCaves = false;
    private bool MakingTunnleCaves = false;
    private WorldGeneration worldGen;

    [Header("Cave Gen")]
    [SerializeField]private bool GenerateCaves = true;
    [SerializeField]private int CaveSize;
    [SerializeField]private Vector2Int CaveSpawnOffset;

    private void Awake() {
        worldGen = gameObject.GetComponent<WorldGeneration>();
        InvokeRepeating("EnablePhysics", 0, 1);
    }
    private void EnablePhysics(){
        if(CavesDone){
            CancelInvoke();
            GameObject.FindGameObjectWithTag("TileMap").GetComponent<TilemapCollider2D>().enabled = true;
            worldGen.Character.transform.position = new Vector3(worldGen.WidthHeight.x/2f, worldGen.WidthHeight.y + worldGen.PlayerSpawnOffset, 0);
        }
    }
    public void GenerateCave(Vector2 position){
        if(GenerateCaves){
            StartCoroutine(CreateCentralCave(position));
        }
    }
    public void GenerateMountainCave(Vector2 position){
        if(GenerateCaves){
            bool LeftRight = (Random.value < .5f);
            if(LeftRight){
                //right
                position.x += Random.Range(8, 14);
            }
            else{
                //left
                position.x += Random.Range(-8, -14);
            }
            StartCoroutine(CreateMountainCave(position, Random.Range(170, 201)));
        }
    }
    public IEnumerator CreateCentralCave(Vector2 cavePosition){
        MakingCentralCaves = true;
        int floorX = Mathf.RoundToInt(cavePosition.x);
        int floorY = Mathf.RoundToInt(cavePosition.y - worldGen.WidthHeight.y / 2f + Random.Range(CaveSpawnOffset.x, CaveSpawnOffset.y));
        for (int i = 0; i < CaveSize; i++)
        {
            int randDir = Random.Range(1, 5);
            switch(randDir){
                case 1:
                //up
                floorY++;
                    break;
                case 2:
                //down
                floorY--;
                    break;
                case 3:
                //right
                floorX++;
                    break;
                case 4:
                //left
                floorX--;
                    break;
            }
            Vector3Int xyPos = worldGen.Tilemap.WorldToCell(new Vector3(floorX, floorY, 0));
            if(worldGen.Tilemap.HasTile(xyPos)){
                worldGen.Tilemap.SetTile(xyPos, null);
                worldGen.ExplodedTileMap.SetTile(xyPos, worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(0, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(0, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(0, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, 0, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, 0, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, 0, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, 0, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, 0, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, 0, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(0, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(0, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(0, -1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, -1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, -1, 0), worldGen.StoneBG);
            }
            yield return new WaitForSeconds(.001f);
        }
        bool SpawnTunnleCave = (Random.value < worldGen.TunnleCaves);
        if(SpawnTunnleCave){
            StartCoroutine(CreateTunnleCave(new Vector2(floorX, floorY)));
        }
        MakingCentralCaves = false;
    }
    public IEnumerator CreateTunnleCave(Vector2 cavePosition){
        MakingTunnleCaves = true;
        int floorX = Mathf.RoundToInt(cavePosition.x);
        int floorY = Mathf.RoundToInt(cavePosition.y);
        bool LeftRight = (Random.value < .5);
        for (int i = 0; i < CaveSize; i++)
        {
            if(LeftRight){
                //right
                int randDir = Random.Range(1, 6);
                switch(randDir){
                    case 1:
                    //up
                    floorY++;
                        break;
                    case 2:
                    //down
                    floorY--;
                        break;
                    case 3:
                    //right
                    floorX++;
                        break;
                    case 4:
                    //left
                    floorX--;
                        break;
                    case 5:
                    floorX++;
                        break;
                }
            }
            else{
                //left
                int randDir = Random.Range(1, 6);
                switch(randDir){
                    case 1:
                    //up
                    floorY++;
                        break;
                    case 2:
                    //down
                    floorY--;
                        break;
                    case 3:
                    //right
                    floorX++;
                        break;
                    case 4:
                    //left
                    floorX--;
                        break;
                    case 5:
                    floorX--;
                        break;
                }
            }
            Vector3Int xyPos = worldGen.Tilemap.WorldToCell(new Vector3(floorX, floorY, 0));
            if(worldGen.Tilemap.HasTile(xyPos)){
                worldGen.Tilemap.SetTile(xyPos, null);
                worldGen.ExplodedTileMap.SetTile(xyPos, worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(0, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(0, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(0, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, 0, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, 0, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, 0, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, 0, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, 0, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, 0, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(0, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(0, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(0, -1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, -1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, -1, 0), worldGen.StoneBG);
            }
            yield return new WaitForSeconds(.001f);
        }
        MakingTunnleCaves = false;
        if(MakingCentralCaves == false){
            CavesDone = true;
        }
        else{
            CavesDone = false;
        }
    }
    public IEnumerator CreateMountainCave(Vector2 cavePosition, int CaveSize){
        MakingTunnleCaves = true;
        int floorX = Mathf.RoundToInt(cavePosition.x);
        int floorY = Mathf.RoundToInt(cavePosition.y);
        for (int i = 0; i < CaveSize; i++)
        {
            int randDir = Random.Range(1, 5);
            switch(randDir){
                case 1:
                //down
                floorY--;
                    break;
                case 2:
                //down
                floorY--;
                    break;
                case 3:
                //right
                floorX++;
                    break;
                case 4:
                //left
                floorX--;
                    break;
            }
            Vector3Int xyPos = worldGen.Tilemap.WorldToCell(new Vector3(floorX, floorY, 0));
            if(worldGen.Tilemap.HasTile(xyPos)){
                worldGen.Tilemap.SetTile(xyPos, null);
                worldGen.ExplodedTileMap.SetTile(xyPos, worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(0, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(0, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(0, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, 0, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, 0, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, 0, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, 0, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, 0, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, 0, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, 1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, 1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, 1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(0, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(0, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(0, -1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(-1, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(-1, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(-1, -1, 0), worldGen.StoneBG);
            }
            if(worldGen.Tilemap.HasTile(xyPos + new Vector3Int(1, -1, 0))){
                worldGen.Tilemap.SetTile(xyPos + new Vector3Int(1, -1, 0), null);
                worldGen.ExplodedTileMap.SetTile(xyPos + new Vector3Int(1, -1, 0), worldGen.StoneBG);
            }
            yield return new WaitForSeconds(.001f);
        }
        MakingTunnleCaves = false;
        if(MakingCentralCaves == false){
            CavesDone = true;
            print("Cave gen done");
        }
        else{
            CavesDone = false;
        }
    }
}
