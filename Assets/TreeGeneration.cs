using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour
{
    private WorldGeneration worldGeneration;
    private void Awake()
    {
        worldGeneration = FindObjectOfType<WorldGeneration>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("TileMap"))
        {
            GenerateTree(5);
        }
    }

    private void GenerateTree(int size)
    {
        int posX = Mathf.RoundToInt(transform.position.x);
        int posY = Mathf.RoundToInt(transform.position.y - 1);
        for (int i = 0; i < size; i++)
        {
            Vector3Int xyPos = worldGeneration.Tilemap.WorldToCell(new Vector3(posX, posY + i, 0));
            worldGeneration.Tilemap.SetTile(xyPos, worldGeneration.ruleTile);
        }
        Destroy(gameObject);
    }
}
