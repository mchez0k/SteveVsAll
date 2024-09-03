using UnityEngine;
using UnityEngine.InputSystem;

public class Holster : MonoBehaviour
{
    public Weapon currentWeapon;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable(); // ���������� GamePlay �����

        playerInput.Gameplay.Attack.performed += OnAttack; // ������������� �� ������� �����
    }

    private void OnDestroy()
    {
        playerInput.Gameplay.Attack.performed -= OnAttack; // ������������ �� ������� ��� �����������
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        currentWeapon?.Attack(); // �������� ����� �������� ������
    }

    // ����� ��� ���������� ������ ������
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