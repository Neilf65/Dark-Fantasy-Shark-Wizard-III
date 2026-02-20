using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Variables  
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private Vector2 _moveInput;

    void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void Update()
    {
        Vector2 move = _playerInput.Land.MoveInput.ReadValue<Vector2>();
        Debug.Log(move);
        _playerInput.Land.Jump.ReadValue<float>();
        if (_playerInput.Land.Jump.ReadValue<float>() == 1);
    }
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
