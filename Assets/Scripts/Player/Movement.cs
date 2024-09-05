using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, IPhysicsObserver
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSensitivity = 3f;
    [SerializeField] private float dashSpeed = 20f; // Скорость для рывка
    [SerializeField] private float dashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float dashCooldown = 1f; // Время перезарядки


    private float lastDashTime = -10f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private DualVirtualJoystick virtualJoystick;

    private PlayerInput playerInput;
    private Collider[] ground = new Collider[1];

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    if (movementInput.sqrMagnitude < 0.1f) return;

    Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * speed;

    // Плавно изменяем скорость.
    Vector3 currentVelocity = rb.velocity;
    Vector3 targetVelocity = new Vector3(movement.x, currentVelocity.y, movement.z);

    // Используем функцию Lerp для плавного перемещения к целевой скорости.
    rb.velocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.fixedDeltaTime * 10f);

    // Ограничиваем максимальную горизонтальную скорость.
    rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed + 5f);
}

    public void OnHealthChanged(Transform attacker, float kickForce)
    {
        Debug.Log("OnHealthChanged - Movement");
        var kickDirection = new Vector3(
            (transform.position.x - attacker.position.x),
            1f,
            (transform.position.z - attacker.position.z)
        ).normalized * kickForce;
        rb.AddForce(kickDirection, ForceMode.Impulse);
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSensitivity);
            }
        }
    }
    private bool IsGrounded()
    {
        Physics.OverlapSphereNonAlloc(transform.position, 0.01f, ground, LayerMask.GetMask("Ground"));
        Debug.Log(ground[0] != null);
        return ground[0] != null;
    }
}