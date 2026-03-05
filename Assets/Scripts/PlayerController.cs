using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.0f;
    private float keysCollected = 0;
    
    public LayerMask interactableLayerMask;

    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Rigidbody rb;
    
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private Vector2 camRotation;
    private bool ShouldFaceMoveDirection;

    RaycastHit[] hits = new RaycastHit[4];
    Ray ray;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;


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
            EnemyMovement enemy = gameObject.GetComponent<EnemyMovement>();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}