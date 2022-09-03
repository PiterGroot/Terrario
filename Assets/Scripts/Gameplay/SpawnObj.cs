using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public bool canSpam;
    private Vector3 mouseWorldPos;
    [SerializeField]private GameObject PrimaryObj;
    [SerializeField]private GameObject SecondaryObj;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            if(canSpam){
                if(Input.GetKey(KeyCode.Mouse0)){
                    Instantiate(PrimaryObj, GetMousePos(), Quaternion.identity);
                }
                else if(Input.GetKey(KeyCode.Mouse1)){
                    Instantiate(SecondaryObj, GetMousePos(), Quaternion.identity);
                }
            }
            else{
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    Instantiate(PrimaryObj, GetMousePos(), Quaternion.identity);
                }
                else if(Input.GetKeyDown(KeyCode.Mouse1)){
                    Instantiate(SecondaryObj, GetMousePos(), Quaternion.identity);
                }
            }
        }  
    }
    private Vector3 GetMousePos(){
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        return mouseWorldPos;
    }
}
