using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float kickForce = 1f;

    public override void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        Debug.Log(hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            var enemyHealth = hitCollider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(transform.position, damage, kickForce);
            }
        }
    }
}

