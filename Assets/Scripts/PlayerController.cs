using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private AttemptManager attemptManager;
    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.0f;
    private float keysCollected = 0;
    
    public LayerMask interactableLayerMask;
    public FootstepManager footstepManager;
    public float footstepInterval = 6f;

    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Rigidbody rb;
    
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private Vector2 camRotation;
    private bool ShouldFaceMoveDirection;
    private float footstepTimer = 6f;
    private string floorTag;

    RaycastHit[] hits = new RaycastHit[4];
    Ray ray;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        attemptManager = FindObjectOfType<AttemptManager>();

        if (attemptManager == null)
        {
            Debug.LogError("AttemptManager not found in scene!");
        }
    }

    // Read movement inputs
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    // Jump if player can jump
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jumping {context.performed} - Is Grounded: {controller.isGrounded}");
        if (context.performed && controller.isGrounded)
        {
            Debug.Log("We are supposed to jump");
            velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        }    
    }


    void Update()
    {
        //inputs
        Vector3 forwardRelativeMovementVector = cameraTransform.forward;
        Vector3 rightRelativeMovementVector = cameraTransform.right;

        // Normalize vectors
        forwardRelativeMovementVector.y = 0f;
        rightRelativeMovementVector.y = 0f;
        forwardRelativeMovementVector.Normalize();
        rightRelativeMovementVector.Normalize();

        // Calculate movement direction
        Vector3 moveDirection = forwardRelativeMovementVector * moveInput.y + rightRelativeMovementVector * moveInput.x;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        
        // Rotate player to face movement direction
        if (ShouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // Play footsteps while the player's moving
        if ((Math.Abs(moveInput.y) > 0f) || (Math.Abs(moveInput.x) > 0f))
        {
            if (controller.isGrounded)
            {
                if (footstepTimer >= footstepInterval / 2)
                {
                    footstepTimer = 0f;
                    footstepManager.Footstep();
                }
                footstepTimer += 1f * Time.deltaTime;
                
            }
        }
        else
        {
            footstepManager.FootstepStop();
            footstepTimer = footstepInterval;
        }

        // Reset footsteps when floor changes
        if (floorTag != footstepManager.FloorTag())
        {
            floorTag = footstepManager.FloorTag();
            footstepManager.FootstepStop();
            footstepTimer = footstepInterval;
            print("Reset footsteps");
        }

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckForColliders()
    {
        int numHits = Physics.RaycastNonAlloc(ray, hits);

        if (numHits > 0)
        {
            Debug.Log("Collided with Enemy");
            LoseManager.manager.Lose();
            attemptManager?.IncrementAttempts();

        }
    }

    public void CollectKey(GameObject Key)
    {
        keysCollected += 1;
        Debug.Log($"Collected a key! Total keys: {keysCollected}");
        Destroy(Key);
    }

    public void CollectStunItem(GameObject StunItem)
    {
        Debug.Log("Collected a stun item!");
        Destroy(StunItem);
    }
    private bool isDead = false;
    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;
        if (isDead || LoseManager.isGameOver) return;

        if (other.CompareTag("Enemy"))
            if (other.CompareTag("Enemy") || other.CompareTag("Trap"))
            {
                isDead = true;

                LoseManager.manager.Lose();

                Debug.Log("Collided with Enemy");
                EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();

                attemptManager?.IncrementAttempts();
            }
        if (other.CompareTag("Trap"))
        {
            isDead = true;

            LoseManager.manager.Lose();

            Debug.Log("Collided with Trap");

            attemptManager?.IncrementAttempts();

            Debug.Log("Player died");
        }

    }
}