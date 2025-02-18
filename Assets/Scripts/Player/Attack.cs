using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    public float currentCooldown;

    //private PlayerInput playerInput;
    private AnimationsManager animationsManager;
    private SoundManager soundManager;


    private void Awake()
    {
        //playerInput = new PlayerInput();
        //playerInput.Gameplay.Enable();
        //playerInput.Gameplay.Attack.performed += OnAttack;

        animationsManager = GetComponent<AnimationsManager>();
        SetWeapon();
        soundManager = GetComponent<SoundManager>();
    }

    private void OnDestroy()
    {
        //playerInput.Gameplay.Attack.performed -= OnAttack;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnAttack();
    }

    private void FixedUpdate()
    {
        currentCooldown -= Time.deltaTime;
    }

    public void OnAttack()
    {
        if (currentWeapon != null && currentCooldown < 0)
        {
            animationsManager.OnAttack();
            soundManager.OnAttack();
            currentWeapon.Attack();
            currentCooldown = currentWeapon.WeaponData.coolDown;
        }
    }

    //private void OnAttack(InputAction.CallbackContext context)
    //{
    //    if (currentWeapon != null && currentCooldown < 0)
    //    {
    //        animationsManager.OnAttack();
    //        soundManager.OnAttack();
    //        currentWeapon.Attack();
    //        currentCooldown = currentWeapon.WeaponData.coolDown;
    //    }
    //}

    public void SetWeapon()
    {
        var weapon = PlayerProgress.GetWeapon();
        currentWeapon.WeaponData = weapon;
        currentCooldown = weapon.coolDown;
    }

}
