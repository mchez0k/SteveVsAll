using UnityEngine;
using UnityEngine.InputSystem;

public class DualVirtualJoystick : MonoBehaviour
{
    public RectTransform leftJoystickHandle;
    public RectTransform rightJoystickHandle;
    public float handleLimit = 50f;

    private PlayerInput playerInputActions;
    internal Vector2 leftInputVector = Vector2.zero;
    internal Vector2 rightInputVector = Vector2.zero;

    private void Awake()
    {
        playerInputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Update()
    {
        // Чтение ввода от обоих джойстиков
        leftInputVector = playerInputActions.Gameplay.MoveLeft.ReadValue<Vector2>();
        rightInputVector = playerInputActions.Gameplay.MoveRight.ReadValue<Vector2>();

        UpdateLeftJoystickUI(leftInputVector);
        UpdateRightJoystickUI(rightInputVector);
    }

    private void UpdateLeftJoystickUI(Vector2 input)
    {
        leftJoystickHandle.anchoredPosition = input * handleLimit;
    }

    private void UpdateRightJoystickUI(Vector2 input)
    {
        rightJoystickHandle.anchoredPosition = input * handleLimit;
    }
}