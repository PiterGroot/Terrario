using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainsGeneration : MonoBehaviour
{
    private Vector2 MountainCaveSpawnPos;
    private CaveGeneration caveGeneration;
    private int RandMountainSize;
    private int itteration = 0;
    private WorldGeneration worldGen;
    [SerializeField]private bool GenerateMountains;
    [SerializeField]private int MountainSize;
    [SerializeField] private Vector2 moutainTop;
    
    
    private void Awake() {
        worldGen = gameObject.GetComponent<WorldGeneration>();
        caveGeneration = gameObject.GetComponent<CaveGeneration>();
    }

    public void GenerateMountain(Vector2 position){
        if(GenerateMountains){
            MountainCaveSpawnPos = Vector2.zero;
            itteration = 0;
            RandMountainSize = 0;
            RandMountainSize = Random.Range(MountainSize/2, MountainSize + Mathf.RoundToInt(MountainSize/2f));
            CreateMountain(position);
        }
    }

    private void CreateMountain(Vector2 position){
        int walkX = Mathf.RoundToInt(position.x);
        int walkY = Mathf.RoundToInt(position.y - 2);
        for (int i = 0; i < RandMountainSize/2f; i++)
        {
            //left half
            int randInt = Random.Range(0,3);
            switch(randInt){
                case 0:
                    walkX++;
                    break;
                case 1: 
                    walkY++;
                    break;
                case 2:
                    walkY++;
                    break;
            }
            Vector3Int xyPos = worldGen.Tilemap.WorldToCell(new Vector3(walkX, walkY, 0));
            MountainCaveSpawnPos = new Vector2(walkX, walkY);
            if(!worldGen.Tilemap.HasTile(xyPos)){
                worldGen.Tilemap.SetTile(xyPos, worldGen.ruleTile);
                for (int y = xyPos.y; y > -999; y--)
                {
                    //Vector3Int xyPos_ = worldGen.Tilemap.WorldToCell(new Vector3(walkX, y, 0));
                    if(!worldGen.Tilemap.HasTile(new Vector3Int(walkX, y-1, 0))){
                        worldGen.Tilemap.SetTile(new Vector3Int(walkX, y-1, 0), worldGen.ruleTile);
                    }
                    else{
                        break;
                    }
                }
            }
        }
        bool NextHalf = (Random.value < .25f);
        if(NextHalf){
            caveGeneration.GenerateMountainCave(MountainCaveSpawnPos);
            for (int i = 0; i < RandMountainSize; i++)
            {
                itteration++;
                //right half
                int randInt = Random.Range(0,3);
                switch(randInt){
                    case 0:
                        walkX++;
                        break;
                    case 1: 
                        walkY--;
                        break;
                    case 2:
                        walkY--;
                        break;
                }
                Vector3Int xyPos = worldGen.Tilemap.WorldToCell(new Vector3(walkX, walkY, 0));
                if(!worldGen.Tilemap.HasTile(xyPos)){
                    worldGen.Tilemap.SetTile(xyPos, worldGen.ruleTile);
                    for (int y = xyPos.y; y > -999; y--)
                    {
                        //Vector3Int xyPos_ = worldGen.Tilemap.WorldToCell(new Vector3(walkX, y, 0));
                        if(!worldGen.Tilemap.HasTile(new Vector3Int(walkX, y-1, 0))){
                            worldGen.Tilemap.SetTile(new Vector3Int(walkX, y-1, 0), worldGen.ruleTile);
                        }
                        else{
                            break;
                        }
                    }
                }
                else{
                    if(itteration > RandMountainSize/2f){
                        break;
                    }
                }
            }
        }
        else{
            itteration = 0;
            for (int i = 0; i < Random.Range(moutainTop.x, moutainTop.y); i++)
            {
                walkX++;
                Vector3Int xyPos_ = worldGen.Tilemap.WorldToCell(new Vector3(walkX, walkY, 0));
                worldGen.Tilemap.SetTile(xyPos_, worldGen.ruleTile);
                for (int y = xyPos_.y; y > -999; y--)
                {
                    //Vector3Int xyPos_ = worldGen.Tilemap.WorldToCell(new Vector3(walkX, y, 0));
                    if(!worldGen.Tilemap.HasTile(new Vector3Int(walkX, y-1, 0))){
                        worldGen.Tilemap.SetTile(new Vector3Int(walkX, y-1, 0), worldGen.ruleTile);
                    }
                    else{
                        break;
                    }
                }
            }
            
            for (int i = 0; i < RandMountainSize; i++)
            {
                itteration++;
                //right half
                int randInt = Random.Range(0,3);
                switch(randInt){
                    case 0:
                        walkX++;
                        break;
                    case 1: 
                        walkY--;
                        break;
                    case 2:
                        walkY--;
                        break;
                }
                Vector3Int xyPos = worldGen.Tilemap.WorldToCell(new Vector3(walkX, walkY, 0));
                if(!worldGen.Tilemap.HasTile(xyPos)){
                    worldGen.Tilemap.SetTile(xyPos, worldGen.ruleTile);
                    for (int y = xyPos.y; y > -999; y--)
                    {
                        //Vector3Int xyPos_ = worldGen.Tilemap.WorldToCell(new Vector3(walkX, y, 0));
                        if(!worldGen.Tilemap.HasTile(new Vector3Int(walkX, y-1, 0))){
                            worldGen.Tilemap.SetTile(new Vector3Int(walkX, y-1, 0), worldGen.ruleTile);
                        }
                        else{
                            break;
                        }
                    }
                }
                else{
                    if(itteration > RandMountainSize/2f){
                        break;
                    }
                }
            }
        }
    }
}
