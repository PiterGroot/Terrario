using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    [SerializeField]bool LeftRight;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(LeftRight){
                FindObjectOfType<WorldGeneration>().GenerateChunkRight(transform.position);
                Destroy(gameObject);
            }
            else{
                FindObjectOfType<WorldGeneration>().GenerateChunkLeft(transform.position);
                Destroy(gameObject);
            }
        }
    }
}
