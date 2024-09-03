using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSensitivity = 5f;
    [SerializeField] private float dashSpeed = 20f; // Скорость для рывка
    [SerializeField] private float dashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float dashCooldown = 1f; // Время перезарядки

    private float lastDashTime = -10f;

    [SerializeField] private CharacterController characterController;

    public DualVirtualJoystick virtualJoystick;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
        playerInput.Gameplay.Dash.performed += OnDashPerformed; // Подписываемся на событие
    }

    private void OnDestroy()
    {
        playerInput.Gameplay.Dash.performed -= OnDashPerformed; // Отписываемся от события
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(PerformDash());
            lastDashTime = Time.time;
        }
    }

    private IEnumerator PerformDash()
    {
        float originalSpeed = speed;
        speed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        speed = originalSpeed;
    }

    void Update()
    {
        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        Vector2 movementInput = Vector2.zero;

        movementInput += playerInput.Gameplay.MoveKeyboard.ReadValue<Vector2>();

        if (virtualJoystick != null)
        {
            movementInput += virtualJoystick.leftInputVector;
        }

        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime;
        characterController.Move(movement);
    }

    void RotateTowardsMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSensitivity * Time.deltaTime);
            }
        }
    }
}