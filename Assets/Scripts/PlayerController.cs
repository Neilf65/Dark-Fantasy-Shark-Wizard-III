using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.0f;

    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Rigidbody rb;
    
    private Vector2 moveInput;
    private Vector3 velocity;
    private Vector2 camRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            rb.AddForce(Vector3.up * velocity.y, ForceMode.Impulse);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interacting");
    }

    void Update()
    {
        //inputs
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        

    }

    private void FixedUpdate()
    {
        Vector3 move = cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x;
        move.y = 0f;
        rb.AddForce(move.normalized * moveSpeed, ForceMode.VelocityChange);



    }

    private void LateUpdate()
    {
        RotatePlayerToCameraForward();
    }

    private void RotatePlayerToCameraForward()
    {
        
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();


        Vector3 forwardRelativeMovementVector = moveInput.y * cameraForward;
        Vector3 rightRelativeMovementVector = moveInput.x * cameraRight;


        Vector3 cameraRelativeMovement = forwardRelativeMovementVector + rightRelativeMovementVector;
    
        controller.Move(cameraRelativeMovement / 50f);

    }

}