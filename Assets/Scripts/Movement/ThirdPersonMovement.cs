using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 5.0f;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [SerializeField] private float speedChangeRate = 10.0f;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedVelocity = -0.5f;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private float groundedRadius;
    public bool IsGrounded;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 3.0f;
    //time required before entering the fall state
    [SerializeField] private float fallTimeout = 0.15f;


    [Header("Audio")]
    [SerializeField] private AudioClip stepSFX;
    [SerializeField] private AudioClip landSFX;

    //Animation
    private Animator animator;
    private float movementBlend;
    private float currentSpeed;
    private int speedParam;
    private int groundedParam;
    private int jumpParam;
    private int freeFallParam;

    //Movement variables
    private CharacterController characterController;
    private float currentAngle;
    private float currentAngleVelocity;
    private float gravity = -9.8f;
    private bool isSprinting = false;
    private Vector3 targetDirection;

    //Audio variables
    private AudioSource audioSource;
    private float yVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();   
        if(camera == null)
        {
            camera = Camera.main;
        }

        //match the radius of the characterController
        //groundedRadius = characterController.radius;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SetupAnimationParameters();
    }

    private void SetupAnimationParameters()
    {
        //Comparing integers is more optimized than comparing strings
        speedParam = Animator.StringToHash("speed");
        groundedParam = Animator.StringToHash("grounded");
        jumpParam = Animator.StringToHash("jump");
        freeFallParam = Animator.StringToHash("freeFall");
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        //Move the player with input
        Vector3 movementInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")).normalized;

        //Get input when sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;
        // Make sure that the targetSpeed is 0 when no input is done
        if(movementInput == Vector3.zero) targetSpeed = 0;

        //Get the magnitude of our character controller's velocity
        float horizontalSpeed = new Vector3(characterController.velocity.x,
            0, characterController.velocity.z).magnitude;

        //To achieve a smooth transition, we will lerp our speed
        float speedOffset = 0.1f;
        if(horizontalSpeed < targetSpeed - speedOffset ||
            horizontalSpeed > targetSpeed + speedOffset) 
        {
            currentSpeed = Mathf.Lerp(horizontalSpeed,
                targetSpeed * movementInput.magnitude, 
                Time.deltaTime * speedChangeRate);
        }
        else
        {
            currentSpeed = targetSpeed;
        }

        //Lerp our animation blend
        movementBlend = Mathf.Lerp(movementBlend, currentSpeed,
            Time.deltaTime * speedChangeRate);
        if(movementBlend < 0.01f) movementBlend = 0f;

        // Make the character move
        if (movementInput.magnitude >= 0.1f)
        {
            //Rotate the character based on input
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z)
                * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle,
                targetAngle,
                ref currentAngleVelocity,
                rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            targetDirection = Quaternion.Euler(0, targetAngle, 0)
                * Vector3.forward;
        }

        animator.SetFloat(speedParam, movementBlend);
        characterController.Move(targetDirection *
               (currentSpeed * Time.deltaTime) + GetVelocityY());
    }

    private float fallTimeoutDelta;
   

    private Vector3 GetVelocityY()
    {
        if(IsGrounded)
        {
            //Reset the fall timer
            fallTimeoutDelta = fallTimeout;
            //Ensure that the character is kept on ground
            if (yVelocity < 0.0f)
            {
                yVelocity = groundedVelocity;
            }

            animator.SetBool(jumpParam, false);
            animator.SetBool(freeFallParam, false);

            //Check if we press jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Calculate the velocity given the height
                //Get the velocity needed to reach the desired height sqrt H * -2 * gravity
                yVelocity = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                animator.SetBool(jumpParam, true);
            }
        }
        else
        {
            // Check if we can free fall
            if(fallTimeoutDelta >= 0.0f)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                animator.SetBool(freeFallParam, true);
            }
        }

        //Always apply gravitational force
        yVelocity += gravity * gravityScale * Time.deltaTime;
        return Vector3.up* yVelocity * Time.deltaTime;
    }

    private void CheckGrounded()
    {
        //set up spherePosition for the physics
        //Vector3 spherePosition = new Vector3(groundPosition.transform.position.x,
        //    groundPosition.transform.position.y);
        IsGrounded = Physics.CheckSphere(groundPosition.transform.position,
            groundedRadius, groundLayers);

        animator.SetBool(groundedParam, IsGrounded);
    }


    public void OnFootstep()
    {
        audioSource.clip = stepSFX; 
        audioSource.Play();
    }

    public void OnLand()
    {
        audioSource.clip = landSFX;
        audioSource.Play();
    }
}
