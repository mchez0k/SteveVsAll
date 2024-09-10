using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    public float currentCooldown;

    private PlayerInput playerInput;
    private AnimationsManager animationsManager;
    private SoundManager soundManager;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
        playerInput.Gameplay.Attack.performed += OnAttack;

        animationsManager = GetComponent<AnimationsManager>();
        SetWeapon();
        soundManager = GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {
        currentCooldown -= Time.deltaTime;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (currentWeapon != null && currentCooldown < 0)
        {
            animationsManager.OnAttack();
            soundManager.OnAttack();
            currentWeapon.Attack();
            currentCooldown = currentWeapon.WeaponData.coolDown;
        }
    }

    public void SetWeapon()
    {
        var weapon = PlayerProgress.GetWeapon();
        currentWeapon.WeaponData = weapon;
        currentCooldown = weapon.coolDown;
    }

}
