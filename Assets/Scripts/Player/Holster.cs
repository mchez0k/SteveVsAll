using UnityEngine;
using UnityEngine.InputSystem;

public class Holster : MonoBehaviour
{
    public Weapon currentWeapon;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable(); // Активируем GamePlay карту

        playerInput.Gameplay.Attack.performed += OnAttack; // Подписываемся на событие атаки
    }

    private void OnDestroy()
    {
        playerInput.Gameplay.Attack.performed -= OnAttack; // Отписываемся от события при уничтожении
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        currentWeapon?.Attack(); // Вызываем атаку текущего оружия
    }

    // Метод для экипировки нового оружия
    public void EquipWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        currentWeapon = newWeapon;
        currentWeapon.gameObject.SetActive(true);
    }
}