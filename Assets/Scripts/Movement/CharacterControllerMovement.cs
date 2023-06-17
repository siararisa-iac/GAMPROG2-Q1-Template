using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float gravityScale = 1.0f;
    [SerializeField]
    private float jumpHeight = 3.0f;

    private float gravity = -9.8f;
    private float groundedVelocity = -0.5f;
    private float yVelocity;

    private CharacterController characterController;

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
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime;
        moveDirection += GetGravityAndJump();
        characterController.Move(moveDirection);
    }

    private Vector3 GetGravityAndJump()
    {
        //Ensure that the character is kept grounded
        if (characterController.isGrounded && yVelocity < 0f)
        {
            yVelocity = groundedVelocity;
        }
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {
            // Calculate the velocity given the height
            yVelocity = Mathf.Sqrt(jumpHeight * 2.0f * Mathf.Abs(gravity));
        }
        // Keep applying gravitational force
        yVelocity += gravity * gravityScale * Time.deltaTime;
        return Vector3.up * yVelocity * Time.deltaTime;
    }
}
