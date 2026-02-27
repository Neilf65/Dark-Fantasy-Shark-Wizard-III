using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIPlayerController : MonoBehaviour
{
    // ✅ Attempt manager reference
    private AttemptManager attemptManager;

    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.0f;

    private float interactRange = 2;
    public LayerMask interactableLayerMask;

    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector3 velocity;
    private bool ShouldFaceMoveDirection;

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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        }
    }

    void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private bool isDead = false;

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return; // 🔒 block duplicates

        if (other.CompareTag("Enemy"))
        {
            isDead = true; // 🔒 lock immediately

            Debug.Log("Collided with Enemy");

            attemptManager?.IncrementAttempts();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
