using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float attackRange = 1.5f;
    public int damage = 10;

    public override void Attack()
    {
        Debug.Log("Perform melee attack");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            //var target = hit.collider.GetComponent<Damageable>();
            //if (target != null)
            //{
            //    target.TakeDamage(damage);
            //}
        }
    }
}

