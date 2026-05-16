using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera References")]
    public Camera playerCam;
    public Transform cameraRoot; // The object that holds the camera for rotation

    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpPower = 5f;
    public float gravity = 20f;

    [Header("Look Settings")]
    public float lookSpeed = 2f;
    public float lookXLimit = 75f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip[] woodFootstepSounds;
    public Transform footstepAudioPosition;

    // Internal State
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove = true; // IMPORTANT: Controlled by ToggleControl()
    private bool isWalking = false;
    private bool isFootstepCoroutineRunning = false;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock and hide cursor on start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleFootsteps();
    }

    private void HandleMovement()
    {
        // We calculate forward/right even if canMove is false to keep moveDirection updated
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Logic: If canMove is false, speed is 0
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jumping logic
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply Gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Apply Movement
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        // IMPORTANT: Only rotate camera if the player is NOT interacting with a padlock
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    // --- FUNCTION CALLED BY PADLOCK / INTERACTABLES ---
    public void ToggleControl(bool state)
    {
        canMove = state;

        if (!state)
        {
            // Reset movement so player doesn't "slide" while looking at lock
            moveDirection = Vector3.zero;
            isWalking = false;
        }
    }

    private void HandleFootsteps()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (canMove && (horizontal != 0 || vertical != 0) && characterController.isGrounded)
        {
            if (!isFootstepCoroutineRunning)
            {
                isWalking = true;
                bool isRunning = Input.GetKey(KeyCode.LeftShift);
                StartCoroutine(PlayFootstepSounds(0.5f / (isRunning ? 1.5f : 1f)));
            }
        }
        else
        {
            isWalking = false;
        }
    }

    IEnumerator PlayFootstepSounds(float delay)
    {
        isFootstepCoroutineRunning = true;
        while (isWalking)
        {
            if (woodFootstepSounds.Length > 0)
            {
                audioSource.PlayOneShot(woodFootstepSounds[Random.Range(0, woodFootstepSounds.Length)]);
            }
            yield return new WaitForSeconds(delay);
        }
        isFootstepCoroutineRunning = false;
    }
}