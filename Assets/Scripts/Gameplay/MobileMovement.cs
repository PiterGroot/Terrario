using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    private bool holdLeft = false;
    private bool holdRight = false;
    private float moveSpeed_;
    private float flipScale_;
    private Animator playerAnim_;
    
    private void Awake() {
        moveSpeed_ = GetComponent<CharacterMovement>().moveSpeed;
        flipScale_ = GetComponent<CharacterMovement>().flipScale;
        playerAnim_ = GetComponent<CharacterMovement>().playerAnim;
    }
    // Update is called once per frame
    private void Update()
    {
        
        if (holdRight == true){
            transform.position += new Vector3(moveSpeed_, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(-flipScale_, 1f, 0f);
            playerAnim_.SetBool("isRunning", true);
        }
        if (holdLeft == true){
            transform.position += new Vector3(-moveSpeed_, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(flipScale_, 1f, 0f);
            playerAnim_.SetBool("isRunning", true);
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended){
                GetComponent<MobileMovement>().holdRight = false;
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
                playerAnim_.SetBool("isRunning", false);
            }

            if (touch.phase == TouchPhase.Ended){
                GetComponent<MobileMovement>().holdLeft = false;
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
                playerAnim_.SetBool("isRunning", false);
            }

            if (touch.phase == TouchPhase.Began)
            {
                //test for the x position of the touch
                if (touch.position.x > (Screen.width / 2)){
                    GetComponent<MobileMovement>().holdRight = true;
                    //Debug.Log("right");
                }
                else if (touch.position.x < (Screen.width / 2)){
                    GetComponent<MobileMovement>().holdLeft = true;
                    //Debug.Log("left");
                }
            }     
            if (touch.phase == TouchPhase.Began)
            {
               GetComponent<CharacterMovement>().JumpIEnumerator(); 
            }    
        }
    }
}