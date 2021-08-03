using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float forceMagnitude = 100f;
  [SerializeField] private float maxVelocity = 4f;
  [SerializeField] private float rotationSpeed = 5f;

  private Camera _mainCamera;

  private Rigidbody _rb;

  private Vector3 _movementDirection;

  private void Start()
  {
    _mainCamera = Camera.main;

    _rb = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    ProcessInput();
    KeepPlayerOnScreen();
    RotateToFaceVelocity();
  }

  /// <summary>
  /// for the physics calculations
  /// </summary>
  private void FixedUpdate()
  {
    if (_movementDirection == Vector3.zero) return;

    _rb.AddForce(_movementDirection * forceMagnitude, ForceMode.Force);

    // clamp it otherwise it will increase continuously
    _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocity);

    print(_rb.velocity);
  }

  private void ProcessInput()
  {
    if (Touchscreen.current.press.isPressed)
    {
      var touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
      var worldPos = _mainCamera.ScreenToWorldPoint(touchPos);

      // moving away from the finger pressed
      _movementDirection = transform.position - worldPos;
      _movementDirection.z = 0f;

      // normalise so we dont move faster if we touch away from the player object
      _movementDirection.Normalize();
    }
    else
    {
      _movementDirection = Vector3.zero;
    }
  }

  private void KeepPlayerOnScreen()
  {
    var newPos = transform.position;

    var viewportPos = _mainCamera.WorldToViewportPoint(transform.position);

    if (viewportPos.x > 1)
    {
      // gone off the right of the screen
      newPos.x = -newPos.x + 0.1f;
    }

    if (viewportPos.x < 0)
    {
      // gone off the left of the screen
      newPos.x = -newPos.x - 0.1f;
    }

    if (viewportPos.y > 1)
    {
      // gone off the top of the screen
      newPos.y = -newPos.y + 0.1f;
    }

    if (viewportPos.y < 0)
    {
      // gone off the bottom of the screen
      newPos.y = -newPos.y - 0.1f;
    }

    transform.position = newPos;
  }

  private void RotateToFaceVelocity()
  {
    if (_rb.velocity == Vector3.zero)
    {
      return;
    }
    
    var targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.back);

    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
  }
}