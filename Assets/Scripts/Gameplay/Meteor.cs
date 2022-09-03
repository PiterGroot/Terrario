using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Meteor : MonoBehaviour
{
    bool explode = false;
    [SerializeField]private Tilemap map;
    [SerializeField]private Tilemap destoyedMap;
    [SerializeField]private Tile ExplodedTile;
    [SerializeField]private ParticleSystem explosion;
    [SerializeField]private Sprite[] Sprites;

    private void Awake() {
        destoyedMap = GameObject.FindGameObjectWithTag("Tilemap1").GetComponent<Tilemap>();
        map = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(!explode && other.gameObject.name != "Meteor(Clone)"){
            explode = true;
            //CreateCrater();
            MakeCrater(transform.position, 2f);
        }
    }
    private void MakeCrater(Vector3 Position, float Radius){
        for (int x = -(int)Radius; x < Radius; x++){
            for (int y = -(int)Radius; y < Radius; y++){
                Vector3Int gridPos = map.WorldToCell(Position + new Vector3(x, y, 0));
                if(map.HasTile(gridPos)){
                    Removetile(gridPos);
                }
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void Removetile(Vector3Int Position){
        map.SetTile(Position, null);
        destoyedMap.SetTile(Position, ExplodedTile);
        map.RefreshTile(Position);
    }
}
