using UnityEngine;
using System.Collections;
using YG;

public class Movement : MonoBehaviour, IPhysicsObserver
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSensitivity = 3f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;


    private float lastDashTime = -10f;
    private bool isCanMove = true;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;
    [SerializeField] private GameObject dashButton;


    //private PlayerInput playerInput;
    private AnimationsManager animationsManager;
    private Attack attack;
    private Collider[] ground = new Collider[1];

    private bool isMobile = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animationsManager = GetComponent<AnimationsManager>();

        isMobile = Application.isMobilePlatform || YandexGame.EnvironmentData.isTablet;
        Debug.Log("Устройство: " + YandexGame.EnvironmentData.deviceType);
        Debug.Log("Устройство: " + Application.isMobilePlatform);

        if (isMobile)
        {
            moveJoystick.gameObject.SetActive(true);
            lookJoystick.gameObject.SetActive(true);
            dashButton.SetActive(true);
            attack = GetComponent<Attack>();
        }
        else
        {
            //playerInput = new PlayerInput();
            //playerInput.Gameplay.Enable();
            //playerInput.Gameplay.Dash.performed += OnDashPerformed;
            moveJoystick.gameObject.SetActive(false);
            lookJoystick.gameObject.SetActive(false);
            dashButton.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (isMobile) return;
        //playerInput.Gameplay.Dash.performed -= OnDashPerformed;
    }

    void FixedUpdate()
    {
        Move();
        RotateTowardsMouse();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) OnDashPerformed();
    }

    private void Move()
    {
        Vector2 movementInput = Vector2.zero;

        if (isMobile)
        {
            movementInput += new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
        } 
        else
        {
            //movementInput += playerInput.Gameplay.MoveKeyboard.ReadValue<Vector2>();
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }


        animationsManager.OnMove(movementInput.magnitude);

        if (movementInput.sqrMagnitude < 0.1f || !isCanMove) return;

        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * speed;

        Vector3 targetVelocity = new Vector3(movement.x, 0f, movement.z);

        rb.velocity += targetVelocity;

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
    }

    public void OnDashPerformed()
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

    private IEnumerator EnableMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        isCanMove = true;
    }

    void RotateTowardsMouse()
    {
        if (isMobile)
        {
            Vector3 direction = new Vector3(lookJoystick.Horizontal, 0f, lookJoystick.Vertical);
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSensitivity);
                attack.OnAttack();
            }
        } 
        else
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        
    }
    public void OnHealthChanged(Vector3 attackerPosition, float kickForce)
    {
        isCanMove = false;
        var kickDirection = new Vector3(
            (transform.position.x - attackerPosition.x),
            1f,
            (transform.position.z - attackerPosition.z)
        ).normalized * kickForce;
        rb.AddForce(kickDirection, ForceMode.Impulse);
        StartCoroutine(EnableMove(kickForce / 10f));
    }

    private bool IsGrounded()
    {
        Physics.OverlapSphereNonAlloc(transform.position, 0.01f, ground, LayerMask.GetMask("Ground"));
        Debug.Log(ground[0] != null);
        return ground[0] != null;
    }
}