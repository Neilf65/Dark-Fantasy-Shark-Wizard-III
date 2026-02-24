using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.0f;
    
    private float interactRange = 2;
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


    void Awake()
    {
        // RotatePlayerToCameraForward();
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // ray = new Ray(cameraTransform.position, cameraTransform.forward);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jumping {context.performed} - Is Grounded: {controller.isGrounded}");
        if (context.performed && controller.isGrounded)
        {
            Debug.Log("We are supposed to jump");
            velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        }    
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //Physics.Raycast(ray, out)
    }

    void Update()
    {
        //inputs
        Vector3 forwardRelativeMovementVector = cameraTransform.forward;
        Vector3 rightRelativeMovementVector = cameraTransform.right;

        forwardRelativeMovementVector.y = 0f;
        rightRelativeMovementVector.y = 0f;
        forwardRelativeMovementVector.Normalize();
        rightRelativeMovementVector.Normalize();

        Vector3 moveDirection = forwardRelativeMovementVector * moveInput.y + rightRelativeMovementVector * moveInput.x;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (ShouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

    void LateUpdate()
    {

    }
    // private void RotatePlayerToCameraForward()
    //  {
    //     Vector3 forwardRelativeMovementVector = cameraTransform.forward;
    //     Vector3 rightRelativeMovementVector = cameraTransform.right;

    //     forwardRelativeMovementVector.y = 0f;
    //     rightRelativeMovementVector.y = 0f;
    //     forwardRelativeMovementVector.Normalize();
    //     rightRelativeMovementVector.Normalize();

    //     Vector3 forwardCam = moveInput.y * forwardRelativeMovementVector;
    //     Vector3 rightCam = moveInput.x * rightRelativeMovementVector;

    //     Vector3 cameraRelativeMovement = forwardCam + rightCam;

    //     transform.Translate(cameraRelativeMovement / Space.World);

    // }

    private void CheckForColliders()
    {
        int numHits = Physics.RaycastNonAlloc(ray, hits);

        if (numHits > 0)
        {
            Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

            for (int i = 0; i < numHits;i++)
            {
                Debug.Log(hits[i].collider.gameObject.name + " was hit!");
            }
        }
    }
}