using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
        playerInput.Gameplay.Attack.performed += OnAttack;
    }

    private void FixedUpdate()
    {
        currentWeapon.DecreaseCooldown();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (currentWeapon != null)
            currentWeapon.Attack();
    }
}
