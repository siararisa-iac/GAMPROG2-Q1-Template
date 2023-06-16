using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float gravityScale = 1.0f;

    private float gravity = -9.8f;

    private CharacterController characterController;

    //jump variables
    private Vector3 playerVelocity;
    private bool grounded;
    private float jumpHeight = 5.0f;
    private bool jumpPressed = false;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove) + (transform.forward * zMove);
        moveDirection.y += gravity * Time.deltaTime * gravityScale;
        moveDirection *= moveSpeed * Time.deltaTime;

        //jump mechanic
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Jump");
            if (characterController.velocity.y == 0)
            {
                Debug.Log("can Jump");
                jumpPressed = true;
            }
            else
            {
                Debug.Log("cannot Jump");
            }
        }
        Jump();

        //Debug.Log(moveDirection);
        characterController.Move(moveDirection);
    }

    //jump code
    void Jump()
    {
        grounded = characterController.isGrounded;
        if (grounded)
        {
            playerVelocity.y = 0.0f;
        }

        if (jumpPressed && grounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravity);
            jumpPressed = false;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}