using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationsManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
        //playerInput.Gameplay.Dash.performed += OnDashPerformed; 
        //playerInput.Gameplay.Attack.performed += Attack; 
    }

    private void Update()
    {
        animator.SetFloat("Speed", playerInput.Gameplay.MoveKeyboard.ReadValue<Vector2>().magnitude);
    }

    public void OnAttack()
    {
        animator.SetTrigger("MeleeAttack");
    }
}
