using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnMove(float move)
    {
        animator.SetFloat("Speed", move);
    }

    public void OnAttack()
    {
        animator.SetTrigger("MeleeAttack");
    }
}
