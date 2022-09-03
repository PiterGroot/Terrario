using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    private Vector2Int WidthHeight1 = new Vector2Int(700, 50);
    private int xPos;
    private int yPos;
    private int xPos1;
    private int yPos1;
    private float hiddenFlatten;
    
    [Header("General")]
    [SerializeField]private bool GenerateOnAwake;
    [SerializeField]public Vector2Int WidthHeight = new Vector2Int(700, 50);
    [SerializeField, Range(0, 100)]private float FlattenPercentage = 20;
    [SerializeField]public Tilemap Tilemap, ExplodedTileMap, FluidsTilemap;
    [SerializeField] public RuleTile ruleTile;
    [SerializeField]public Tile StoneBG;
    [SerializeField]private GameObject ChunkTrigger_right, ChunkTrigger_left;
    [SerializeField]public GameObject CaveTrigger, MountainTrigger;
    [SerializeField]public Transform Character;
    [SerializeField]private GameObject testArea;
    [Header("Settings")]
    [SerializeField]private int minStoneHeight;
    [SerializeField]private int maxStoneHeight;
    [SerializeField]public float PlayerSpawnOffset;
    [SerializeField, Range(.1f, .5f)]private float CentralCaves = .2f;
    [SerializeField, Range(.1f, .5f)]public float TunnleCaves = .3f;
    [SerializeField, Range(.1f, .5f)]private float Mountains = .15f;

    [ContextMenu("Generate now")]
    private void Start() {
        if(GenerateOnAwake){
            testArea.SetActive(false);
            WidthHeight1 = WidthHeight;
            Application.targetFrameRate = -1;
            hiddenFlatten = FlattenPercentage / 100f;
            Invoke("DisablePhysics", 5f);
            InitChunk();
        }
        else{
            testArea.SetActive(true);
        }
    }
    private void InitChunk(){
        Instantiate(ChunkTrigger_left, new Vector3(xPos, WidthHeight.y + 1, 0f), Quaternion.identity);
        xPos1 = xPos;
        yPos1 = WidthHeight.y;
        for (int x = 0; x < WidthHeight.x; x++)
        {
            bool Flatten = (Random.value < hiddenFlatten);
            if(!Flatten){
                bool SpawnCave = (Random.value < CentralCaves);
                if(SpawnCave){
                    Instantiate(CaveTrigger, new Vector3(x, WidthHeight.y + 45), Quaternion.identity);
                    bool SpawnMountain = (Random.value < Mountains);
                    if(SpawnMountain && x < WidthHeight.x - 60){
                        Instantiate(MountainTrigger, new Vector3(x, WidthHeight.y + 30), Quaternion.identity);
                    }
                }
                int minHeight = WidthHeight.y -1;
                int maxHeight = WidthHeight.y +2;
                WidthHeight.y = Random.Range(minHeight, maxHeight);
            }
            int minStoneDistance = WidthHeight.y - minStoneHeight;
            int maxStoneDistance = WidthHeight.y - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneDistance, maxStoneDistance);
            for (int y = 0; y < WidthHeight.y; y++)
            {
                Vector3Int gridPos = Tilemap.WorldToCell(new Vector3(x, y, 0));
                PaintTile(gridPos, ruleTile, Tilemap);
                yPos = y;
            }
            Vector3Int gridPos__ = Tilemap.WorldToCell(new Vector3(x, WidthHeight.y, 0));
            PaintTile(gridPos__, ruleTile, Tilemap);
            xPos = x;
        }
        Instantiate(ChunkTrigger_right, new Vector3(xPos, yPos + 1, 0f), Quaternion.identity);    
    }
    
    public IEnumerator CreateChunkRight(Vector2 position){
        int ChunkX = WidthHeight.x + (int)position.x;
        for (int x = (int)position.x; x < ChunkX; x++)
        {
            bool Flatten = (Random.value < hiddenFlatten);
            if(!Flatten){
                bool SpawnCave = (Random.value < CentralCaves);
                if(SpawnCave){
                    Instantiate(CaveTrigger, new Vector3(x, WidthHeight.y + 55), Quaternion.identity);
                    bool SpawnMountain = (Random.value < Mountains);
                    if(SpawnMountain){
                        Instantiate(MountainTrigger, new Vector3(x, WidthHeight.y + 30), Quaternion.identity);
                    }
                }
                int minHeight = WidthHeight.y -1;
                int maxHeight = WidthHeight.y +2;
                WidthHeight.y = Random.Range(minHeight, maxHeight);
            }
            int minStoneDistance = WidthHeight.y - minStoneHeight;
            int maxStoneDistance = WidthHeight.y - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneDistance, maxStoneDistance);
            for (int y = 0; y < WidthHeight.y; y++)
            {
                Vector3Int gridPos = Tilemap.WorldToCell(new Vector3(x, y, 0));
                PaintTile(gridPos, ruleTile, Tilemap);
                yPos = y;
                yield return new WaitForSeconds(FPSLoadtimes());
            }
            Vector3Int gridPos__ = Tilemap.WorldToCell(new Vector3(x, WidthHeight.y, 0));
            PaintTile(gridPos__, ruleTile, Tilemap);
            xPos = x;
        }
        Instantiate(ChunkTrigger_right, new Vector3(xPos, yPos + 1, 0f), Quaternion.identity);
    }

    public IEnumerator CreateChunkLeft(Vector2 position){
        int ChunkX = WidthHeight1.x - (int)position.x;
        for (int x = (int)position.x; x > -ChunkX; x--)
        {   
            bool Flatten = (Random.value < hiddenFlatten);
            if(!Flatten){
                bool SpawnCave = (Random.value < CentralCaves);
                if(SpawnCave){
                    Instantiate(CaveTrigger, new Vector3(x, WidthHeight.y + 55), Quaternion.identity);
                    bool SpawnMountain = (Random.value < Mountains);
                    if(SpawnMountain){
                        Instantiate(MountainTrigger, new Vector3(x, WidthHeight.y + 30), Quaternion.identity);
                    }
                }
                int minHeight = WidthHeight1.y -1;
                int maxHeight = WidthHeight1.y +2;
                WidthHeight1.y = Random.Range(minHeight, maxHeight);
            }
            int minStoneDistance = WidthHeight1.y - minStoneHeight;
            int maxStoneDistance = WidthHeight1.y - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneDistance, maxStoneDistance);
            for (int y = 0; y < WidthHeight1.y; y++)
            {
                Vector3Int gridPos = Tilemap.WorldToCell(new Vector3(x, y, 0));
                PaintTile(gridPos, ruleTile, Tilemap);
                yPos1 = y;
                yield return new WaitForSeconds(FPSLoadtimes());
            }
            Vector3Int gridPos__ = Tilemap.WorldToCell(new Vector3(x, WidthHeight1.y, 0));
            PaintTile(gridPos__, ruleTile, Tilemap);
            xPos1 = x;
        }
        Instantiate(ChunkTrigger_left, new Vector3(xPos1, yPos1 + 1, 0f), Quaternion.identity);
    }

    private void PaintTile(Vector3Int _Position, RuleTile tile, Tilemap map){
        map.SetTile(_Position, tile);
    }
    public void GenerateChunkRight(Vector2 position){
        StartCoroutine(CreateChunkRight(position));
    }
    public void GenerateChunkLeft(Vector2 position){
        StartCoroutine(CreateChunkLeft(position));
    }
    public void GenerateCave(Vector2 position){
        gameObject.GetComponent<CaveGeneration>().GenerateCave(position);
    }
    public void GenerateMountain(Vector2 position){
        gameObject.GetComponent<MountainsGeneration>().GenerateMountain(position);
    }
    private void DisablePhysics(){
        GameObject.FindGameObjectWithTag("TileMap").GetComponent<TilemapCollider2D>().enabled = false;
    }

    private float FPSLoadtimes(){
        float Result = 0f;
        if(DebugMenu.fps < 100){
            Result = 0.01f;
        }
        if(DebugMenu.fps > 100 && DebugMenu.fps < 200){
            Result = 0.006f;
        }
        if(DebugMenu.fps > 200 && DebugMenu.fps < 300){
            Result = 0.005f;
        }
        if(DebugMenu.fps > 300){
            Result = 0.004f;
        }
        return Result;
    }
}
