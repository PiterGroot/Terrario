using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDevice : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer){
            gameObject.GetComponent<CharacterMovement>().canMove = true;
            FindObjectOfType<MobileMovement>().enabled = false;
            print("Playing with Windows");
        }

        if (Application.platform == RuntimePlatform.Android){
            FindObjectOfType<MobileMovement>().enabled = true;
            FindObjectOfType<CharacterMovement>().canMove = false;   
            print("Playing with Android");
        }
    }
}
