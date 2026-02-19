using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Variables  
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rb;
    private Vector2 _moveInput;
 
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = 
            _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0f;
      _rb.AddForce(move.normalized * moveSpeed, ForceMode.VelocityChange);  
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
