using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    [SerializeField]private WorldGeneration worldGeneration;
    private void Awake() {
        if(worldGeneration == null){
            worldGeneration = FindObjectOfType<WorldGeneration>();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("TileMap")){
            worldGeneration.GenerateCave(transform.position);
            Destroy(gameObject);
        }
    }
}
