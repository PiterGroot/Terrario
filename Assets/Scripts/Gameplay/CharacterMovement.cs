using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private bool MoveRight;
    //private bool 

    [Header("Other")]
    [SerializeField]private Rigidbody2D rb2D;
    [SerializeField]public Animator playerAnim;
    [Header("Movement")]
    [SerializeField]public float moveSpeed = 10;
    [SerializeField]private float jumpForce = 5;
    [SerializeField]public float flipScale = 1;
    [SerializeField]private bool isGrounded = false;
    public bool canMove = true;

    public void SetIdle(){
         playerAnim.SetBool("isRunning", false);
    }
    void Update()
    {
        if(canMove){
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-moveSpeed, 0, 0) * Time.deltaTime;
                transform.localScale = new Vector3(flipScale, 1f, 0f);
                playerAnim.SetBool("isRunning", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
                transform.localScale = new Vector3(-flipScale, 1f, 0f);
                playerAnim.SetBool("isRunning", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
                playerAnim.SetBool("isRunning", false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
                playerAnim.SetBool("isRunning", false);
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
                StartCoroutine(Jump());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("TileMap")){
             isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
         if(other.collider.CompareTag("TileMap")){
             isGrounded = false;
        }
    }
    public IEnumerator Jump(){
        rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        yield return new WaitForSeconds(.6f);
        rb2D.AddForce(new Vector2(0, -1f), ForceMode2D.Force);
    }
    public void JumpIEnumerator(){
        if(isGrounded){
            StartCoroutine(Jump());
        }
    }
}
